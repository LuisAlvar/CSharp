using DataScanner;
using DataScanner.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
      services.AddSingleton<IFileWatchService, FileWatchService>();
      services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
