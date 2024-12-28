using System;
using System.Threading;
using System.Threading.Tasks;

public class DependentTaskManager
{
  private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

  public static async Task RunDependentTasksAsync(CancellationToken cancellationToken)
  {
    var tcs1 = new TaskCompletionSource<int>();
    var tcs2 = new TaskCompletionSource<int>();

    var task1 = Task1Async(tcs1, tcs2, cancellationToken);
    var task2 = Task2Async(tcs1, tcs2, cancellationToken);

    await Task.WhenAll(task1, task2);

    // Proceed with the synchronous task after both tasks complete
    Console.WriteLine("Data from task2: " + tcs2.Task.Result);
    ExecuteSynchronousTask();
  }

  private static async Task Task1Async(TaskCompletionSource<int> tcs1, TaskCompletionSource<int> tcs2, CancellationToken cancellationToken)
  {
    // Simulate data production and dependency on task2
    Console.WriteLine("iam Task 1| working....");
    await Task.Delay(4000, cancellationToken); // Simulating work
    Console.WriteLine("iam Task 1| Setting data: 42");
    tcs1.SetResult(42); // Setting data

    var resultFromTask2 = await tcs2.Task; // Waiting for data from task2
    Console.WriteLine($"iam Task 1| received data from Task 2: {resultFromTask2}");
  }

  private static async Task Task2Async(TaskCompletionSource<int> tcs1, TaskCompletionSource<int> tcs2, CancellationToken cancellationToken)
  {
    // Wait for data from task1
    var resultFromTask1 = await tcs1.Task; // Waiting for data from task1
    Console.WriteLine($"iam Task 2| received data: {resultFromTask1} - from Task 1");

    // Simulate data production
    Console.WriteLine("iam Task 2| working on data just received from task 1.");
    await Task.Delay(8000, cancellationToken); // Simulating work
    tcs2.SetResult(resultFromTask1 * 2); // Setting data
    Console.WriteLine($"iam Task 2| done working; setting data {resultFromTask1 * 2}");
  }

  private static void ExecuteSynchronousTask()
  {
    // Simulate a synchronous task
    Console.WriteLine("Executing synchronous task.");
  }
}
