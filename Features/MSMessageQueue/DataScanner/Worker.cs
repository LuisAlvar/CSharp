using DataScanner.Services;

namespace DataScanner
{
  public class Worker : BackgroundService
  {
    private readonly ILogger<Worker> _logger;
    private readonly IFileWatchService _fileWatchService;
    public Worker(ILogger<Worker> logger, IFileWatchService fileWatchService)
    {
      _logger = logger;
      _fileWatchService = fileWatchService;
      _fileWatchService.FileDetected += OnFileDetected;
    }

    /// <summary>
    /// Event Subscription: Subscribed to the event in the worker service and handled the detected file
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnFileDetected(object sender, FileDetectedEventArgs e)
    {
      _logger.LogInformation($"File detected: { e.FilePath }");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
      _fileWatchService.StartFileWatcher();

      while (!stoppingToken.IsCancellationRequested)
      {
        await Task.Delay(1000, stoppingToken);
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
      }
    }
  }
}
