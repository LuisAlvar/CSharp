
using FileWatch;

var fileWatchService = new FileWatchService();
fileWatchService.LoadConfigurations();
fileWatchService.StartFileWatcher();
await TaskManager.RunTask(fileWatchService.Run);