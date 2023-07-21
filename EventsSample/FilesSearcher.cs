using System.Diagnostics;

namespace EventsSample
{
    public class FilesSearcher :IDisposable
    {

        public event EventHandler<FileArgs> FileFound;

        public FilesSearcher()
        {
            FileFound += (_, args) => FileFoundEvent(args);
        }

        private void FileFoundEvent(FileArgs e)
        {
            Debug.WriteLine($"Найден файл с именем {e.Name}");
        }

        /// <summary>
        /// Поиск файлов в указанном каталоге
        /// </summary>
        /// <param name="directoryPath">Путь к дериктории</param>
        /// <param name="isRecursive">Рекурсивный поиск</param>
        /// <param name="maxDepth">Глубина поиска</param>
        public void Search(string directoryPath, bool isRecursive = false, int maxDepth = 1)
        {
            if (!isRecursive)
            {
                FilesSearch(directoryPath);
                return;
            }

            RecursiveSearch(directoryPath, maxDepth, 1);
        }

        private void RecursiveSearch(string directoryPath, int maxDepth, int currentDepth)
        {
            FilesSearch(directoryPath);

            if (maxDepth < currentDepth)
            {
                return;
            }

            IsExceptionHandler(() =>
            {
                var allDirectories = Directory.GetDirectories(directoryPath);
                foreach (var directory in allDirectories)
                {
                    RecursiveSearch(directory, maxDepth, currentDepth + 1);
                }
            }, directoryPath);
        }

        private void FilesSearch(string directoryPath)
        {
            IsExceptionHandler(() => 
            {
                var allFiles = Directory.GetFiles(directoryPath);
                foreach (string file in allFiles)
                {
                    OnFileFound(file);
                }
            }, directoryPath);           
        }

        private void IsExceptionHandler(Action action, string directoryPath)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                if (e is UnauthorizedAccessException)
                {
                    Debug.WriteLine($"Доступ к директории {directoryPath} ограничен");
                }
                return;
            }
        }

        private void OnFileFound(string file)
        {
            if (FileFound != null)
            {
                var args = new FileArgs(file);
                FileFound(this, args);
            }
        }

        public void Dispose()
        {
            UnsubscribeAll();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Отписаться от всех событий
        /// </summary>
        private void UnsubscribeAll()
        {
            if (FileFound != null)
            {
                FileFound = (EventHandler<FileArgs>)Delegate.RemoveAll(FileFound, FileFound);
            }

        }
    }
}