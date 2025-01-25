
namespace DataScanner.Services
{
  public class FileDetectedEventArgs : EventArgs
  {
    public string FilePath { get; }
    public FileDetectedEventArgs(string filePath)
    {
      FilePath = filePath;
    }
  }

  public interface IFileWatchService
  {
    public void StartFileWatcher();

    /// <summary>
    /// Event defined: Added an event (FileDetected) to IFileWatchService
    /// </summary>
    public event EventHandler<FileDetectedEventArgs> FileDetected;
  }
}
