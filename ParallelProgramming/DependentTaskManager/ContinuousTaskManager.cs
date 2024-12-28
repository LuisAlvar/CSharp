using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

public class ContinuousTaskManager
{
  private static readonly BlockingCollection<int> _dataQueue = new BlockingCollection<int>();
  private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
  private static CancellationTokenSource _cancellationTokenSource;

  public static async Task RunContinuousTasksAsync()
  {
    _cancellationTokenSource = new CancellationTokenSource();
    CancellationToken token = _cancellationTokenSource.Token;

    var task1 = Task1Async(token);
    var task2 = Task2Async(token);

    // Wait for a signal to stop
    await Task.WhenAll(task1, task2);

    // Proceed with the synchronous task after both tasks complete
    ExecuteSynchronousTask();
  }

  private static async Task Task1Async(CancellationToken cancellationToken)
  {
    try
    {
      while (!cancellationToken.IsCancellationRequested)
      {
        // Simulate data production
        int data = new Random().Next(100);
        _dataQueue.Add(data);
        Console.WriteLine($"Task 1 produced data: {data}");

        await Task.Delay(4000, cancellationToken); // Simulating work
      }
    }
    catch (OperationCanceledException)
    {
      Console.WriteLine("Task 1 canceled.");
    }
  }

  private static async Task Task2Async(CancellationToken cancellationToken)
  {
    try
    {
      while (!cancellationToken.IsCancellationRequested)
      {
        // Wait for data from task1
        if (_dataQueue.TryTake(out int data, Timeout.Infinite, cancellationToken))
        {
          Console.WriteLine($"Task 2 received data: {data}");

          // Simulate data processing
          await Task.Delay(4000, cancellationToken); // Simulating work
        }
      }
    }
    catch (OperationCanceledException)
    {
      Console.WriteLine("Task 2 canceled.");
    }
  }

  private static void ExecuteSynchronousTask()
  {
    // Simulate a synchronous task
    Console.WriteLine("Executing synchronous task.");
  }

  public static void Stop()
  {
    _cancellationTokenSource.Cancel();
  }
}
