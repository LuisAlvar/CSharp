using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScanner.Services
{
  public class FileProcessor : IFileProcessor, IHostedService
  {
    private readonly ILogger<FileProcessor> _logger;

    public FileProcessor(ILogger<FileProcessor> logger) 
    {
      _logger = logger;
    }

    public void ProcessFilename(string fileName)
    {
      throw new NotImplementedException();
    }

    Task IHostedService.StartAsync(CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    Task IHostedService.StopAsync(CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
