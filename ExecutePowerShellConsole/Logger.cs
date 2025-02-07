using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace ExecutePowerShellConsole
{
  public class Logger
  {
    private const string OutputFileDir = @"C:/Program Files/LeafSoft/HealthCheckApp";
    private const string OutputFileName = "Install_Logs.txt";

    private static List<string> lsLogContainer = new List<string>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="type"></param>
    public static void AddMessage(string message, LoggerMessageType type = LoggerMessageType.INFO)
    {
      lsLogContainer.Add($"[{type.ToString()}: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")}]: {message}");
    }

    /// <summary>
    /// 
    /// </summary>
    public static void Flush()
    {
      int retryCount = 5;
      int delay = 1000;

      if (lsLogContainer.Count == 0) return;
      if (!File.Exists(Path.Combine(OutputFileDir, OutputFileName)))
      {
        EnsureAndTakeOwnership(OutputFileDir);
        FileStream fs = File.Create(Path.Combine(OutputFileDir, OutputFileName));
        fs.Close();
      }
      while (retryCount > 0)
      {
        try
        {
          using (StreamWriter write = new StreamWriter(Path.Combine(OutputFileDir, OutputFileName)))
          {
            foreach (var item in lsLogContainer)
            {
              write.WriteLine(item);
            }
          }
          Console.WriteLine("File accessed and written successfully.");
          lock (lsLogContainer)
          {
            lsLogContainer = new List<string>();
          }
          break;
        }
        catch (IOException ex) when (IsFileLocked(ex))
        {
          Console.WriteLine($"File is being used by another process. Retry in {delay/1000} seconds ...");
          System.Threading.Thread.Sleep(delay);
        }
        --retryCount;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    private static bool IsFileLocked(IOException ex)
    {
      int errorCode = System.Runtime.InteropServices.Marshal.GetHRForException(ex) & ((1 << 16) - 1);
      return errorCode == 32 | errorCode == 33; // ERROR_SHARING_VIOLATION or ERROR_LOCK_VIOLATION
    }

    private static void EnsureAndTakeOwnership(string folderPath)
    {
      // Create the folder if it doesnt exist
      if (!Directory.Exists(folderPath))
      {
        Directory.CreateDirectory($"{folderPath}");
      }

      // Get the current user
      var currentUser = WindowsIdentity.GetCurrent();
      var ntAccount = new NTAccount(currentUser.Name);

      // Get the security descriptor of the folder
      DirectoryInfo di = new DirectoryInfo(folderPath);
      DirectorySecurity ds = di.GetAccessControl();

      // Set the owner to the current user
      ds.SetOwner(ntAccount);

      // Apply the changes
      di.SetAccessControl(ds);

      Console.WriteLine($"Ownership of folder '{folderPath}' has been take by {currentUser.Name}");
    }
  }

  public enum LoggerMessageType
  {
    INFO,
    WARN,
    ERROR,
  }
}
