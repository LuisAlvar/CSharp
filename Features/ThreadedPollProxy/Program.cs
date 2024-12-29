/*
 * Starts a new task asynchronously.
 * Inside this task, it calls an asynchronous method StartIfNotRunning
 * The StartIfNotRunning method checks if a specific job is completed using ThreadLaunchManager.IsJobCompleted(token).
 * If the job is not completed, it calls ThreadLaunchManager.StartCheckForChangeInBackground(token) to start checking for changes in the background.
 * Any exceptions during this process are caught and printed to the console.
 */

CancellationTokenSource source = new CancellationTokenSource();
CancellationToken token = source.Token;

Task task1 = Task.Factory.StartNew(async () =>
{
  await StartIfNotRunning();

  async Task StartIfNotRunning()
  {
    try
    {
      if (!await ThreadLaunchManager.IsJobCompleted(token))
      {
        ThreadLaunchManager.StartCheckForChangeInBackground(token);
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
    }
  }
});

try
{
  while (!(ThreadLaunchManager.IsJobDone || ThreadLaunchManager.HasMaxTimeoutReached))
  {
    await Task.Delay(3000); // Replacing Thread.Sleep with Task.Delay for better async behavior
  }
}
catch (Exception ex)
{
  Console.WriteLine(ex);
}

await task1; // Await the task directly instead of using Task.WaitAll
