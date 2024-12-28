using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

//public class RealTimeTaskManager
//{
//  private static readonly BlockingCollection<int> _dataQueue = new BlockingCollection<int>();
//  private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
//  private static CancellationTokenSource _cancellationTokenSource;

//  public static async Task RunRealTimeTasksAsync()
//  {
//    _cancellationTokenSource = new CancellationTokenSource();
//    CancellationToken token = _cancellationTokenSource.Token;

//    var task1 = Task1Async(token);
//    var task2 = Task2Async(token);

//    // Wait for a signal to stop
//    await Task.WhenAll(task1, task2);

//    // Proceed with the synchronous task after both tasks complete
//    ExecuteSynchronousTask();
//  }

//  private static async Task Task1Async(CancellationToken cancellationToken)
//  {
//    try
//    {
//      while (!cancellationToken.IsCancellationRequested)
//      {
//        // Simulate data production
//        int data = new Random().Next(100);
//        _dataQueue.Add(data);
//        Console.WriteLine($"Task 1 produced data: {data}");

//        await Task.Delay(1000, cancellationToken); // Simulating work
//      }
//    }
//    catch (OperationCanceledException)
//    {
//      Console.WriteLine("Task 1 canceled.");
//    }
//  }

//  private static async Task Task2Async(CancellationToken cancellationToken)
//  {
//    List<Task> processingTasks = new List<Task>();

//    try
//    {
//      while (!cancellationToken.IsCancellationRequested)
//      {
//        // Wait for data from task1
//        if (_dataQueue.TryTake(out int data, Timeout.Infinite, cancellationToken))
//        {
//          Console.WriteLine($"Task 2 received data: {data}");

//          // Spawn a new task for each piece of data received
//          var processingTask = ProcessDataAsync(data, cancellationToken);
//          processingTasks.Add(processingTask);

//          // Clean up completed tasks
//          processingTasks.RemoveAll(t => t.IsCompleted);
//        }
//      }
//    }
//    catch (OperationCanceledException)
//    {
//      Console.WriteLine("Task 2 canceled.");
//    }

//    // Ensure all processing tasks are completed before exiting
//    await Task.WhenAll(processingTasks);
//  }

//  private static async Task ProcessDataAsync(int data, CancellationToken cancellationToken)
//  {
//    try
//    {
//      // Simulate data processing
//      await Task.Delay(2000, cancellationToken); // Simulating work
//      Console.WriteLine($"Processed data: {data}");
//    }
//    catch (OperationCanceledException)
//    {
//      Console.WriteLine($"Processing of data {data} canceled.");
//    }
//  }

//  private static void ExecuteSynchronousTask()
//  {
//    // Simulate a synchronous task
//    Console.WriteLine("Executing synchronous task.");
//  }

//  public static void Stop()
//  {
//    _cancellationTokenSource.Cancel();
//  }
//}
