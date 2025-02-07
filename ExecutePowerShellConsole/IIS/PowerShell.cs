
using System.Diagnostics;
using Newtonsoft;
using Newtonsoft.Json;

namespace ExecutePowerShellConsole.IIS
{
  public class PowerShell
  {
    private static RunSettings _settings = new RunSettings();

    public static void Settings(string[] args)
    {
      try
      {
        if (args.Length == 0) throw new ArgumentException("Missing Valuable Parameters to Exe via Setup Installer");
        var data = JsonConvert.SerializeObject(args);
        Logger.AddMessage("Arguments - " + data);
        _settings.InstallationStage = args[0];
        _settings.InstallationType = args[1];
        Logger.AddMessage("RunSettings - " + JsonConvert.SerializeObject(_settings));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void Execute()
    {
      try
      {
        Logger.AddMessage("Executing Powershell script ....");


        if (_settings.InstallationStage.ToLower().Contains("commit"))
        {
          Commit();
        }
        else if (_settings.InstallationStage.ToLower().Contains("uninstall"))
        {
          Uninstall();
        }

        Logger.AddMessage("At the end of executing Powershell script ....");
        Logger.Flush();
      }
      catch (Exception ex)
      {
        Logger.AddMessage($"Failed to execute {ex}", LoggerMessageType.ERROR);
        Logger.Flush();
      }
    }

    private static void Commit()
    {
      Logger.AddMessage("Executing Commit steps....");
      // Define your PowerShell script as a string
      ProcessStartInfo scriptInfo = new ProcessStartInfo("powershell.exe", string.Join(" ", "-NoProfile", "-C", IIS.Scripts.AddNewWebsiteToIIS))
      {
        UseShellExecute = false,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        CreateNoWindow = true
      };

      using (Process process = new Process())
      {
        process.StartInfo = scriptInfo;
        process.Start();

        // Read the output
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        //Display the script
        Logger.AddMessage("Script:");
        Logger.AddMessage(IIS.Scripts.AddNewWebsiteToIIS);

        // Display the output and error (if any)
        Logger.AddMessage("Output:");
        Logger.AddMessage(output);

        if (!string.IsNullOrEmpty(error))
        {
          Logger.AddMessage("Error:");
          Logger.AddMessage(error);
        }
      }
      Logger.AddMessage("Executing Commit steps .... completed");

    }


    private static void Uninstall()
    {

    }

  }
}
