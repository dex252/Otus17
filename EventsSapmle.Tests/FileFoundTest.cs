using EventsSample;
using System.Diagnostics;
using Xunit;

namespace EventsSapmle.Tests
{
    public class FileFoundTest
    {
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
                Debug.WriteLine($"Путь {directoryPath} не существует");
                return;
            }
            
            using (var fs = new FilesSearcher())
            {
                fs.Search(directoryPath, isRecursive, maxDepth);
            }
            
        }

    }
}