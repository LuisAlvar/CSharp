
//using var source = new CancellationTokenSource();
//CancellationToken token = source.Token;

//try
//{
//  await DependentTaskManager.RunDependentTasksAsync(token);
//}
//catch (Exception ex)
//{
//  Console.WriteLine($"Exception occurred: {ex.Message}");
//}


//try
//{
//  await ContinuousTaskManager.RunContinuousTasksAsync();

//  // Simulate running the tasks for a period of time
//  await Task.Delay(10000);

//  // Stop the tasks
//  ContinuousTaskManager.Stop();
//}
//catch (Exception ex)
//{
//  Console.WriteLine($"Exception occurred: {ex.Message}");
//}




//try
//{
//  var realTimeTaskManager = RealTimeTaskManager.RunRealTimeTasksAsync();

//  // Simulate running the tasks for a period of time
//  await Task.Delay(10000);

//  // Stop the tasks
//  RealTimeTaskManager.Stop();

//  // Await the task manager to ensure all tasks are complete
//  await realTimeTaskManager;
//}
//catch (Exception ex)
//{
//  Console.WriteLine($"Exception occurred: {ex.Message}");
//}




try
{
  await RealTimeThreadManager.RunRealTimeTasksAsync();
}
catch (Exception ex)
{
  RealTimeThreadManager.Stop();
  Console.WriteLine($"Exception occurred: {ex.Message}");
}
