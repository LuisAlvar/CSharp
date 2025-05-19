using CalculatorService.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Threading;
using Serilog;
using System.Runtime.InteropServices;
using Serilog.Core;

namespace CalculatorService_Host
{
  public partial class CalculatorServiceHost : ServiceBase
  {
    private ServiceHost _host = null;

    public CalculatorServiceHost()
    {
      ServiceName = "CalculatorServiceHost";
    }

    protected override void OnStart(string[] args)
    {
      Log.Information("Starting WCF service...");

      try
      {
        _host = new ServiceHost(typeof(CalculatorServiceSvc));
        _host.Open();
        Log.Information("WCF Service started successfully at {Time}", DateTime.Now);
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Error while starting WCF service");
        throw;
      }
    }

    protected override void OnStop()
    {
      Log.Information("Stopping WCF service...");
      if (_host != null)
      {
        _host.Close();
        Log.Information("WCF service stopped at {Time}", DateTime.Now);
      }
    }

  }
}
