using System.Data.SqlClient;
using System.Diagnostics;

internal static class ThreadLaunchManager 
{
  private static int MaxTimeoutInSeconds = 300;
  private static TimeSpan MaxTimeout => TimeSpan.FromSeconds(MaxTimeoutInSeconds);

  private static readonly object _lock = new object();

  private static Task? _launchTask;

  public static bool IsJobDone { get; private set; } = false;

  public static bool HasMaxTimeoutReached { get; private set; } = false;

  /// <summary>
  /// Manage a single long-running background task, ensuring it doesnt start multiple tasks simultaneously. 
  /// </summary>
  /// <param name="cancellationToken"></param>
  public static void StartCheckForChangeInBackground(CancellationToken cancellationToken)
  {

    /*
     * Ensures that the following block of code is thread-safe, preventing 
     * multiple threads from entering the critical section simultaneously.
     */
    lock(_lock)
    {
      if(_launchTask == null)
      {
        Console.WriteLine("launching task ...");
        _launchTask = UpdateStatus(StartToCheckForChangeInStatus(cancellationToken));

      }
      else {
        Console.WriteLine("launching task already deployed ...");
      }
    }

    /*
     * This nested asynchronous method updates the status and 
     * handles completion or errors
     */
    async Task UpdateStatus(Task launchTask)
    {
      try
      {
        await launchTask; // awaits the completion of the task passed to UpdateStatus. 
        IsJobDone = await IsJobCompleted(cancellationToken);
      }
      catch (System.Exception ex)
      {
        Console.WriteLine("There was error trying to launch the thread :: " + ex.ToString());
      }
      finally
      {
        // Ensures that the lock is re-acquired, logs the task completion, and sets _launchTask to null 
        // to indicate that no task is running. 
        lock(_lock)
        {
          Console.WriteLine("Launch task completed");
          _launchTask = null;
        }
      }
    }
  }

  private static async Task StartToCheckForChangeInStatus(CancellationToken cancellationToken)
  {
    var sw = Stopwatch.StartNew();
    var changeInStatus = false;
    var FirstCheckPrint = false;

    while(!HasMaxTimeoutReached)
    {
      if(!FirstCheckPrint)
      {
        Console.WriteLine($"Checking for a sequences of change in status at {DateTime.Now.ToString()} ....");
        FirstCheckPrint = true;
      }

      changeInStatus = await ChangeInStatus(cancellationToken);
      if (changeInStatus)
      {
        Console.WriteLine("Change in Status detected...");
        break;
      }

      if (cancellationToken.IsCancellationRequested)
      {
        return;
      }

      HasMaxTimeoutReached = sw.Elapsed >= MaxTimeout;
      await Task.Delay(10, cancellationToken);
    }

    if(HasMaxTimeoutReached) Console.WriteLine("Time on waiting max out.");

  }

  public static async Task<bool> IsJobCompleted(CancellationToken cancellationToken)
  {
    using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
    using var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(timeout.Token, cancellationToken);

    try
    {
      var response = await JobCheckResult(cancellationTokenSource.Token);
      Console.WriteLine($"The job [750B1114-3BAC-4EBB-AB68-D6F41A2FEAE4] is {response}");
      return !string.IsNullOrEmpty(response) && response.ToLower().Contains("completed");
    }
    catch (System.Exception ex)
    {
      Console.WriteLine(ex);
      return false;
    }
  }

  private static async Task<bool> ChangeInStatus(CancellationToken cancellationToken)
  {
    //We are assumming that Status is running 
    using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
    using var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(timeout.Token, cancellationToken);

    try
    {
      var response = await JobCheckResult(cancellationTokenSource.Token);
      return !string.IsNullOrEmpty(response) && response != "Running";
    }
    catch (System.Exception)
    {
      return false;
    }

  }

  private static async Task<string> JobCheckResult(CancellationToken cancellationToken)
  {
    string queryString = @"SELECT [Status] FROM dbo.JobBatch WHERE JobId = '750B1114-3BAC-4EBB-AB68-D6F41A2FEAE4'";
    string result = string.Empty;

    using (SqlConnection connection = SqlConnect())
    {
      SqlCommand sqlCommand = new SqlCommand(queryString, connection);
      connection.Open();
      try
      {
        result = await sqlCommand.ExecuteScalarAsync() as string ?? string.Empty;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }

      if(cancellationToken.IsCancellationRequested) return string.Empty;

    }



    return result;
  }

  private static SqlConnection SqlConnect()
  {
    return new SqlConnection(ConnectionString());
  }

  private static string ConnectionString()
  {
    return "Server=localhost,1433;Initial Catalog=Test;Persist Security Info=False;User ID=SA;Password=1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=15;";
  }

}