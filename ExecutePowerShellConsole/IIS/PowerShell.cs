
using System.Diagnostics;

namespace ExecutePowerShellConsole.IIS
{
  public class PowerShell
  {
    public static void Execute()
    {
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
        Console.WriteLine("Script:");
        Console.WriteLine(IIS.Scripts.AddNewWebsiteToIIS);

        // Display the output and error (if any)
        Console.WriteLine("Output:");
        Console.WriteLine(output);

        if (!string.IsNullOrEmpty(error))
        {
          Console.WriteLine("Error:");
          Console.WriteLine(error);
        }
      }
    }
  }
}
