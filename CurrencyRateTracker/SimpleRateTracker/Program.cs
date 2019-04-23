using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using SimpleRateTracker.CurrencySource;

namespace SimpleRateTracker
{
    class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int x,
            int y,
            int cx,
            int cy,
            int uFlags);

        private const int HWND_TOPMOST = -1;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOSIZE = 0x0001;

        static void Main(string[] args)
        {
            SetWindowsAlwaysOnTop();

            var fetcher = new YahooService();
            double actualRate = fetcher.GetLatestRate();
            double previousRate = actualRate;
            double highestRate = 0;

            while (true)
            {
                try
                {
                    actualRate = fetcher.GetLatestRate();
                    if (actualRate > highestRate)
                    {
                        highestRate = actualRate;
                    }

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Highest: {highestRate}");
                    Console.ForegroundColor = actualRate > previousRate ? ConsoleColor.Green : ConsoleColor.Red;
                    ConsoleHelper.SetConsoleFont(28);
                    Console.Write($"Current: {actualRate}");

                    previousRate = actualRate;
                    Thread.Sleep(100);
                }
                catch (Exception)
                {
                    //do nothing, retry on next loop
                }
            }
        }

        private static void SetWindowsAlwaysOnTop()
        {
            IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;

            SetWindowPos
            (
                hWnd,
                new IntPtr(HWND_TOPMOST),
                0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE
            );
        }
    }
}
