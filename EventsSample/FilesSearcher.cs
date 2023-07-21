using System.Diagnostics;

namespace EventsSample
{
    public class FilesSearcher : IDisposable
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
                FilesSearch(directoryPath, CancellationToken.None);
                return;
            }

            RecursiveSearch(directoryPath, CancellationToken.None, maxDepth, 1);
        }

        /// <summary>
        /// Поиск файлов в указанном каталоге
        /// </summary>
        /// <param name="directoryPath">Путь к дериктории</param>
        /// <param name="isRecursive">Рекурсивный поиск</param>
        /// <param name="maxDepth">Глубина поиска</param>
        /// <param name="cancellationToken">Токен отмены</param>
        public void Search(string directoryPath, CancellationToken cancellationToken, bool isRecursive = false, int maxDepth = 1)
        {
            try
            {
                if (!isRecursive)
                {
                    FilesSearch(directoryPath, cancellationToken);
                    return;
                }

                RecursiveSearch(directoryPath, cancellationToken, maxDepth, 1);
            }
            catch (OperationCanceledException e)
            {
                Debug.WriteLine(e.Message);
                return;
            }
            
        }

        private void RecursiveSearch(string directoryPath, CancellationToken cancellationToken, int maxDepth, int currentDepth)
        {
            FilesSearch(directoryPath, cancellationToken);

            if (maxDepth < currentDepth)
            {
                return;
            }

            IsExceptionHandler(() =>
            {
                var allDirectories = Directory.GetDirectories(directoryPath);
                foreach (var directory in allDirectories)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        throw new OperationCanceledException();
                    }

                    RecursiveSearch(directory, cancellationToken, maxDepth, currentDepth + 1);
                }
            }, directoryPath);
        }

        private void FilesSearch(string directoryPath, CancellationToken cancellationToken)
        {
            IsExceptionHandler(() => 
            {
                var allFiles = Directory.GetFiles(directoryPath);
                foreach (string file in allFiles)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        throw new OperationCanceledException();
                    }

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
                    return;
                }

                if (e is FileNotFoundException || e is DirectoryNotFoundException)
                {
                    Debug.WriteLine($"Путь к файлу или директории {directoryPath} не найден");
                    return;
                }

                throw e;
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