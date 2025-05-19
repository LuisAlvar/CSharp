using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService_Host
{
  class Program
  {
    static void Main(string[] args)
    {
      Log.Logger = new LoggerConfiguration()
       .WriteTo.Console()
       .WriteTo.File("C:\\Program Files\\ArcSoftwareGroup\\CalculatorService_Host\\logs\\CalculatorServiceHost.log", rollingInterval: RollingInterval.Day)
       .CreateLogger();

      Log.Information("Starting CalculatorServiceHost as a Windows Service");

      ServiceBase[] ServicesToRunWithinThisConsole;
      ServicesToRunWithinThisConsole = new ServiceBase[]
      {
          new CalculatorServiceHost()
      };
      ServiceBase.Run(ServicesToRunWithinThisConsole);
      Log.Information("CalculatorServiceHost has been initialized.");

    }
  }
}
