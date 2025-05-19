using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

[RunInstaller(true)]
public class ProjectInstaller : Installer
{
  private ServiceProcessInstaller _processInstaller;
  private ServiceInstaller _serviceInstaller;

  public ProjectInstaller()
  {
    // Setup the process installer (run service under LocalSystem)
    _processInstaller = new ServiceProcessInstaller
    {
      Account = ServiceAccount.LocalSystem
    };

    // Setup the service installer
    _serviceInstaller = new ServiceInstaller
    {
      ServiceName = "CalculatorServiceHost",
      DisplayName = "Calculator WCF Service",
      Description = "Self-hosted WCF service running as a Windows Service.",
      StartType = ServiceStartMode.Automatic
    };

    // Add installers to the Installer collection
    Installers.Add(_processInstaller);
    Installers.Add(_serviceInstaller);
  }

  public override void Install(IDictionary stateSaver)
  {
    base.Install(stateSaver);

    try
    {
      using (ServiceController sc = new ServiceController("CalculatorServiceHost"))
      {
        sc.Start();
        Console.WriteLine("Service started successfully.");
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error starting service: {ex.Message}");
      throw;
    }
  }

  public override void Uninstall(IDictionary savedState)
  {
    try
    {
      using (ServiceController sc = new ServiceController("CalculatorServiceHost"))
      {
        if (sc.Status != ServiceControllerStatus.Stopped)
        {
          sc.Stop();
          Console.WriteLine("Service stopped successfully.");
        }
      }

      // Delete the service from Windows
      RemoveWindowsService("CalculatorServiceHost");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error stopping/deleting service: {ex.Message}");
    }

    base.Uninstall(savedState);
  }

  private void RemoveWindowsService(string serviceName)
  {
    try
    {
      System.Diagnostics.Process.Start("sc.exe", $"delete {serviceName}");
      Console.WriteLine($"Service '{serviceName}' deleted successfully.");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error deleting service: {ex.Message}");
    }
  }
}