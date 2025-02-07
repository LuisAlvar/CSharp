using ExecutePowerShellConsole;
using ExecutePowerShellConsole.IIS;

try
{
  PowerShell.Settings(args);
  PowerShell.Execute();
}
catch (Exception ex)
{
  Logger.AddMessage(ex.ToString(), LoggerMessageType.ERROR);
  Logger.Flush();
	throw ex;
}