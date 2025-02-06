using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecutePowerShellConsole
{
  public class Logger
  {
    private const string OutputFileDir = @"C:\";
    private const string OutputFileName = "Install_Logs.txt";

    private static List<string> lsLogContainer = new List<string>();

    public static void AddMessage(string message, LoggerMessageType type = LoggerMessageType.INFO)
    {
      lsLogContainer.Add($"{type.ToString()}: {DateTime.UtcNow.ToString()}: {message}");
    }

    public static void Flush()
    {
      if (!File.Exists(Path.Combine(OutputFileDir, OutputFileName)))
      {
        Directory.CreateDirectory(OutputFileDir);
        File.Create(Path.Combine(OutputFileDir, OutputFileName));
      }
      using (StreamWriter write = new StreamWriter(Path.Combine(OutputFileDir, OutputFileName)))
      {
        foreach (var item in lsLogContainer)
        {
          write.WriteLine(item);
        }
      }
    }
  }

  public enum LoggerMessageType
  {
    INFO,
    WARN,
    ERROR,
  }
}
