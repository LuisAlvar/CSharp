using System.Text;
using System.Threading.Tasks.Sources;
using DataScanner.Entities;

namespace DataScanner.Services
{
  public class FileWatchService : IFileWatchService
  {
    private readonly IConfiguration _config;
    private readonly ILogger<FileWatchService> _logger;
    private readonly object _lock = new();

    private List<CustomFolderSettings> _lstCustomFoldersSettings = new List<CustomFolderSettings>();
    private List<FileSystemWatcher> _lstFileSystemWatcher = new List<FileSystemWatcher>();
    private bool _isInitialized = false;

    public event EventHandler<FileDetectedEventArgs> FileDetected = null!;

    public FileWatchService(ILogger<FileWatchService> logger, IConfiguration? configuration=null)
    {
      _logger = logger;
      _logger.LogInformation("File System Watcher service injected ...");
      _config = configuration 
        ?? new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();
    }

    /// <summary>
    /// Event Raising: Raised the event in the FileWatcherService when a file is detected
    /// </summary>
    /// <param name="e"></param>
    private void OnFileDetected(FileDetectedEventArgs e)
    {
      FileDetected?.Invoke(this, e);
    }

    private void FileWatcher_Create(object sender, FileSystemEventArgs e, string actionExec, string actionArgs)
    {
      string fileName = e.FullPath;
      string newStr = string.Format(actionArgs, fileName);
      _logger.LogInformation($"---> Detected File ----> ({fileName}) )");
      OnFileDetected(new FileDetectedEventArgs(fileName));
    }

    private void LoadConfigurations()
    {
      try
      {
        _logger.LogInformation("Loading configuration ....");
        _config.GetSection("ListCustomFolderSettings").Bind(_lstCustomFoldersSettings);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error loading configurations");
      }
    }

    private void InitializeFileWatchers()
    {

      foreach (CustomFolderSettings customFolder in _lstCustomFoldersSettings)
      {
        try
        {
          DirectoryInfo directoryInfo = new DirectoryInfo(customFolder.FolderPath);
          if (directoryInfo.Exists && customFolder.FolderEnabled)
          {
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Filter = customFolder.FolderFilter;
            fileSystemWatcher.Path = customFolder.FolderPath;
            StringBuilder actionToExecute = new StringBuilder(customFolder.ExecutableFile);
            StringBuilder actionArguments = new StringBuilder(customFolder.ExecutableArguments);
            fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            fileSystemWatcher.Created += (senderObj, fileSysArgs) => FileWatcher_Create(senderObj, fileSysArgs, actionToExecute.ToString(), actionArguments.ToString());
            fileSystemWatcher.EnableRaisingEvents = true;
            lock (_lock)
            {
              _lstFileSystemWatcher.Add(fileSystemWatcher);
            }
            _logger.LogInformation($"Monitoring files with extension ({fileSystemWatcher.Filter}) in the folder ({fileSystemWatcher.Path})");
          }
        }
        catch (Exception ex)
        {
          _logger.LogError(ex, $"Error setting up FileSystemWatcher for folder: {customFolder.FolderPath}");
        }
      }
    }
  
    public void StartFileWatcher()
    {
      lock(_lock)
      {
        // Used the _isInitialized flag to ensure StartFileWatcher() logic runs only once
        if (!_isInitialized)
        {
          _logger.LogInformation("Starting file watcher ...");
          LoadConfigurations();
          InitializeFileWatchers();
          _isInitialized = true;
        }
      }
    }
  }
}
