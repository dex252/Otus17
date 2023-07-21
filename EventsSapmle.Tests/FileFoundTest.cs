using EventsSample;
using System.Diagnostics;
using Xunit;
using Timer = System.Timers.Timer;

namespace EventsSapmle.Tests
{
    public class FileFoundTest
    {
        /// <summary>
        /// Время срабатывания токена отмены
        /// </summary>
        const int CANCEL_TIME = 3000;

        [Theory]
        [InlineData(@"C:\")]
        [InlineData(@"C:\", true)]
        [InlineData(@"C:\", true, 2)]
        [InlineData(@"D:\")]
        [InlineData(@"D:\", true)]
        [InlineData(@"D:\", true, 3)]
        public void SearchTest(string directoryPath, bool isRecursive = false, int maxDepth = 1)
        {
            if (!Directory.Exists(directoryPath))
            {
                Debug.WriteLine($"ѕуть {directoryPath} не существует");
                return;
            }

            using (var fs = new FilesSearcher())
            {
                fs.Search(directoryPath, isRecursive, maxDepth);
            }
            
        }

        [Theory]
        [InlineData(@"C:\", true, 3)]
        [InlineData(@"C:\", true, 4)]
        [InlineData(@"C:\", true, 5)]
        public void SearchWithCancelationTokenTest(string directoryPath, bool isRecursive = false, int maxDepth = 1)
        {
            if (!Directory.Exists(directoryPath))
            {
                Debug.WriteLine($"Путь {directoryPath} не существует");
                return;
            }

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            using (var timer = new Timer(CANCEL_TIME))
            using (var fs = new FilesSearcher())
            {
                timer.Elapsed += (_, args) => {
                    tokenSource.Cancel();
                };

                timer.Start();

                fs.Search(directoryPath, token, isRecursive, maxDepth);
            }

        }

    }
}