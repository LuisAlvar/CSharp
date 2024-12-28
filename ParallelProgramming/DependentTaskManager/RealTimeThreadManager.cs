using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class RealTimeThreadManager
{
  private static readonly BlockingCollection<int> _dataQueue = new BlockingCollection<int>();
  private static CancellationTokenSource _cancellationTokenSource;
  private static TimeSpan MaxTimeout = TimeSpan.FromSeconds(10); // Maximum timeout for Task1

  public static async Task RunRealTimeTasksAsync()
  {
    _cancellationTokenSource = new CancellationTokenSource();
    CancellationToken token = _cancellationTokenSource.Token;

    var task1 = Task1Async(token);
    var task2 = Task2Async(token);

    Console.WriteLine("Waiting...for the tasks");
    await Task.WhenAll(task1, task2);
    Console.WriteLine("Done waiting for the tasks");

    // Proceed with the synchronous task after both tasks complete
    ExecuteSynchronousTask();
  }

  private static async Task Task1Async(CancellationToken cancellationToken)
  {
    try
    {
      var timeoutCts = new CancellationTokenSource(MaxTimeout);
      using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token))
      {
        while (!linkedCts.Token.IsCancellationRequested)
        {
          // Simulate data production
          int data = new Random().Next(100);
          _dataQueue.Add(data);
          Console.WriteLine($"Task 1 produced data: {data}");

          await Task.Delay(1000, linkedCts.Token); // Simulating work
        }
      }
    }
    catch (OperationCanceledException)
    {
      Console.WriteLine("Task 1 stopped producing data due to timeout or cancellation.");
    }
    finally
    {
      Console.WriteLine("Task 1 signal _dataQueue as CompleteAdding");
      _dataQueue.CompleteAdding(); // Signal Task2 that no more data will be added
    }
  }

  private static async Task Task2Async(CancellationToken cancellationToken)
  {
    var processingTasks = new List<Task>();

    try
    {
      while (!cancellationToken.IsCancellationRequested && !_dataQueue.IsCompleted)
      {
 

        // Wait for data from task1
        if (_dataQueue.TryTake(out int data, Timeout.Infinite, cancellationToken))
        {
          Console.WriteLine($"Task 2 received data: {data}");

          // Spawn a new task for each piece of data received
          var processingTask = ProcessDataAsync(data, cancellationToken);
          processingTasks.Add(processingTask);

        }

      }

      // Ensure all processing tasks are completed before exiting
      Console.WriteLine($"iam Task 2 | Waiting on tasks");
      await Task.WhenAll(processingTasks);

    }
    catch (OperationCanceledException)
    {
      Console.WriteLine("Task 2 canceled.");
    }

 
  }

  private static async Task ProcessDataAsync(int data, CancellationToken cancellationToken)
  {
    try
    {
      // Simulate data processing
      await Task.Delay(2000, cancellationToken); // Simulating work
      Console.WriteLine($"Processed data: {data}");
    }
    catch (OperationCanceledException)
    {
      Console.WriteLine($"Processing of data {data} canceled.");
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
