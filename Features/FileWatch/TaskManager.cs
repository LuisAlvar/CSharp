using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatch
{
  public class TaskManager
  {
    private static CancellationTokenSource _cancellationTokenSource;
    private static TimeSpan MaxTimeout = TimeSpan.FromSeconds(10); // Maximum timeout for Task1

    public static async Task RunTask(Func<CancellationToken, Task> taskFunc)
    {
      _cancellationTokenSource = new CancellationTokenSource();
      var cancellationToken = _cancellationTokenSource.Token;
      Console.CancelKeyPress += (sender, eventArgs) =>
      {
        eventArgs.Cancel = true;
        _cancellationTokenSource.Cancel();
      };

      try
      {
        await taskFunc(_cancellationTokenSource.Token);
      }
      catch (OperationCanceledException)
      {
        Console.WriteLine("The operation was canceled.");
      }
    }
  }
}
