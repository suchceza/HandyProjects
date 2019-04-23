
namespace GAkademiVideoDownloader
{
    internal class Lesson
    {
        //################################################################################
        #region Constructor

        public Lesson(string name, int startIndex, int endIndex)
        {
            Name = name;
            StartIndex = startIndex;
            EndIndex = endIndex;
        }

        #endregion

        //################################################################################
        #region Properties

        public string Name
        {
            get;
        }

        public int StartIndex
        {
            get;
        }

        public int EndIndex
        {
            get;
        }

        #endregion
    }
}
