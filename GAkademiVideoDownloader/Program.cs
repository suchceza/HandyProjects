using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace GAkademiVideoDownloader
{
    class Program
    {
        private static string Parameters = "number=1665039552281847&password=123456";
        private static string BaseUrl = "http://www.gakademi.com.tr/login/signin";
        private static string VideoUrl = "http://www.gakademi.com.tr/education/jwplayer/";

        private static readonly IList<Lesson> Lessons = new List<Lesson>
        {
            new Lesson("English", 1385, 1439),
            new Lesson("Arabic", 1468, 1506),
            new Lesson("Russian", 1667, 1702),
            new Lesson("German", 1440, 1467)
        };

        static void Main()
        {
            foreach (var lesson in Lessons)
            {
                int dirIndex = 0;
                for (int i = lesson.StartIndex; i <= lesson.EndIndex; i++)
                {
                    var url = $"{BaseUrl}";

                    //Login Request
                    var loginRequest = CreateRequest(url, Parameters, new CookieContainer());
                    using ((HttpWebResponse)loginRequest.GetResponse())
                    {
                        //Playlist Request
                        var playlistRequest = CreateRequest($"{VideoUrl}{i}", Parameters, loginRequest.CookieContainer);
                        using (HttpWebResponse playlistResponse = (HttpWebResponse)playlistRequest.GetResponse())
                        {
                            using (Stream videoStream = playlistResponse.GetResponseStream())
                            {
                                string videoHeader;
                                var playlistLink = GetPlaylistLinkFromStream(videoStream, out videoHeader);
                                var playlistLinkArray = playlistLink.Split(new[] { "playlist.m3u8" }, StringSplitOptions.None);

                                //Get ChunkLink
                                var chunkRequest = CreateRequest(playlistLink, Parameters, playlistRequest.CookieContainer);
                                using (HttpWebResponse chunkResponse = (HttpWebResponse)chunkRequest.GetResponse())
                                {
                                    using (Stream chunkStream = chunkResponse.GetResponseStream())
                                    {
                                        var chunkLink = GetChunkLinkFromStream(chunkStream);

                                        //Get ChunkList
                                        var chunkListRequest = CreateRequest($"{playlistLinkArray[0]}{chunkLink}", Parameters, chunkRequest.CookieContainer);
                                        bool isChunkListRetry = true;
                                        while (isChunkListRetry)
                                        {
                                            try
                                            {
                                                using (HttpWebResponse chunkListResponse = (HttpWebResponse)chunkListRequest.GetResponse())
                                                {
                                                    isChunkListRetry = false;
                                                    using (Stream chunkListStream = chunkListResponse.GetResponseStream())
                                                    {
                                                        var chunkList = GetChunkListFromStream(chunkListStream);

                                                        var index = 0;
                                                        foreach (var chunkPath in chunkList)
                                                        {
                                                            var ind = $"{index}";

                                                            //check for directory
                                                            var directory = CheckDirectory(playlistLinkArray[0], videoHeader, dirIndex);
                                                            var filename = $@"{directory}\{videoHeader}_{ind.PadLeft(3, '0')}.ts";
                                                            var fileDownloadLink = $"{playlistLinkArray[0]}{chunkPath}";

                                                            //download file
                                                            bool isDownloadRetry = true;
                                                            while (isDownloadRetry)
                                                            {
                                                                try
                                                                {
                                                                    using (var client = new WebClient())
                                                                    {
                                                                        if (CompareFileSize(fileDownloadLink, filename))
                                                                            break;

                                                                        client.DownloadFile(fileDownloadLink, filename);

                                                                        if (CompareFileSize(fileDownloadLink, filename))
                                                                        {
                                                                            Console.WriteLine(filename);
                                                                            isDownloadRetry = false;
                                                                        }

                                                                        //File.AppendAllLines(@"D:\_ga\chunkList.txt", new[] { $"{playlistLinkArray[0]}{chunkPath}" });
                                                                        //File.AppendAllLines(@"D:\_ga\downloadList.txt", new[] { $@"{directory}\{videoHeader}_{ind.PadLeft(3, '0')}" });
                                                                    }
                                                                }
                                                                catch (Exception)
                                                                {
                                                                    isDownloadRetry = true;
                                                                    //Console.WriteLine($"Error: {filename}");
                                                                    //Console.WriteLine($"Exception occured: {e.Message}");
                                                                    //Console.WriteLine(e.StackTrace);
                                                                }
                                                            }

                                                            index++;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception)
                                            {
                                                isChunkListRetry = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    dirIndex++;
                }
            }
        }

        //################################################################################
        #region Private Implementation

        private static bool CompareFileSize(string fileDownloadLink, string filename)
        {
            long fileDiskSize;
            long fileWebSize;

            if (File.Exists(filename))
            {
                FileInfo fileInfo = new FileInfo(filename);
                fileDiskSize = fileInfo.Length;
            }
            else
            {
                return false;
            }

            using (var client = new WebClient())
            {
                var openStream = client.OpenRead(fileDownloadLink);
                fileWebSize = Convert.ToInt64(client.ResponseHeaders["Content-Length"]);
                openStream?.Close();
            }

            return fileWebSize == fileDiskSize;
        }

        private static string CheckDirectory(string rootFolders, string mainFolder, int index)
        {
            var plArray = rootFolders.Split(':');
            var dirArray = plArray[3].Split('/');
            var ind = $"{index}";

            var directoryName = $@"D:\_ga\{dirArray[1]}\{dirArray[2]}\{ind.PadLeft(2, '0')}_{mainFolder}";
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            return directoryName;
        }

        private static HttpWebRequest CreateRequest(string url, string parameters, CookieContainer cookieContainer)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentLength = parameters.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = cookieContainer;

            using (Stream stream = request.GetRequestStream())
            {
                byte[] paramAsBytes = Encoding.Default.GetBytes(parameters);
                stream.Write(paramAsBytes, 0, paramAsBytes.Count());
            }

            return request;
        }

        private static string GetChunkLinkFromStream(Stream chunkStream)
        {
            if (chunkStream != null)
            {
                using (StreamReader reader = new StreamReader(chunkStream))
                {
                    var content = reader.ReadToEnd();
                    var contentArray = content.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    return contentArray[3];
                }
            }

            throw new ArgumentException(nameof(chunkStream));
        }

        private static IList<string> GetChunkListFromStream(Stream chunkListStream)
        {
            if (chunkListStream != null)
            {
                using (StreamReader reader = new StreamReader(chunkListStream))
                {
                    var content = reader.ReadToEnd();
                    var contentArray = content.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                    var chunkList = new List<string>();
                    foreach (var chunk in contentArray)
                    {
                        if (chunk.StartsWith("media_"))
                        {
                            chunkList.Add(chunk);
                        }
                    }

                    return chunkList;
                }
            }

            throw new ArgumentException(nameof(chunkListStream));
        }

        private static string GetPlaylistLinkFromStream(Stream videoStream, out string videoHeader)
        {
            videoHeader = null;
            if (videoStream != null)
            {
                using (StreamReader reader = new StreamReader(videoStream))
                {
                    while (!reader.EndOfStream)
                    {
                        var linkLine = reader.ReadLine()?.Trim();
                        if (linkLine != null && linkLine.StartsWith("file:"))
                        {
                            linkLine = linkLine.Replace("',", "");
                            linkLine = linkLine.Replace("file: '", "");
                            return linkLine;
                        }

                        if (linkLine != null && linkLine.Contains("</h1>"))
                        {
                            linkLine = linkLine.Replace(" </h1>", "");
                            linkLine = linkLine.Replace("/", "");
                            linkLine = linkLine.Replace("  ", "");
                            linkLine = linkLine.Replace("\"", "");
                            linkLine = linkLine.Replace("'", "");
                            var headerArray = linkLine.Split(' ');
                            foreach (var str in headerArray)
                            {
                                var s = str.ToLower();
                                s = s.First().ToString().ToUpper() + s.Substring(1);
                                s = s.Replace(',', '_');
                                videoHeader += s;
                            }
                        }
                    }
                }
            }

            throw new ArgumentException(nameof(videoStream));
        }

        #endregion
    }
}
