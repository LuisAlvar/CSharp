using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatch
{
  public class FileWatchService : IFileWatchService
  {
    private readonly IConfiguration _config;
    private List<CustomFolderSettings> _lstCustomFoldersSettings = new List<CustomFolderSettings>();
    private List<FileSystemWatcher> _lstFileSystemWatcher = new List<FileSystemWatcher>();
    private bool _isConfigDataLoaded = false;

    public FileWatchService(IConfiguration? configuration=null)
    {
      Console.WriteLine(AppContext.BaseDirectory);
      _config = configuration 
        ?? new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();
    }

    private void FileWatcher_Create(object sender, FileSystemEventArgs e, string actionExec, string actionArgs)
    {
      string fileName = e.FullPath;
      string newStr = string.Format(actionArgs, fileName);

      System.Console.WriteLine($"---> Detected File ----> ({fileName}) )");
    }

    public void LoadConfigurations()
    {
      Console.WriteLine("LoadConfiguration Method");
      _config.GetSection("ListCustomFolderSettings").Bind(_lstCustomFoldersSettings);
      if (_lstCustomFoldersSettings.Count > 0) _isConfigDataLoaded = true;
    }


    public void StartFileWatcher()
    {
      Console.WriteLine("StartFileWatcher Method");
      if (!_isConfigDataLoaded)
      {
        Console.WriteLine("Need to load configuration");
        return;
      }

      foreach (CustomFolderSettings customFolder in _lstCustomFoldersSettings)
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
          _lstFileSystemWatcher.Add(fileSystemWatcher);
          Console.WriteLine($"Starting to monitor files with extension ({fileSystemWatcher.Filter}) in the folder ({fileSystemWatcher.Path})");
        }
      }
    }

    public async Task Run(CancellationToken cancellationToken)
    {
      while (!cancellationToken.IsCancellationRequested)
      {
        Console.WriteLine("File Watch Service is Active...");
        await Task.Delay(Timeout.Infinite, cancellationToken);
      }
    }

  }

}
