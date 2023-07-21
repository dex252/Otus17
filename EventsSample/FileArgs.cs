namespace EventsSample
{
    public class FileArgs: EventArgs
    {
        public string Name { get; }
        public FileArgs(string name)
        {
            Name = name;
        }
    }
}
