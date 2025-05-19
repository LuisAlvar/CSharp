using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CalculatorService.Services
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
  // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
  public class CalculatorServiceSvc : ICalculatorServiceSvc
  {
    static CalculatorServiceSvc()
    { 
      
    }

    public double Add(double n1, double n2)
    {
      string logstring = string.Empty;
      logstring = $"{n1} +  {n2}";
      EventLog.WriteEntry("CalculatorServiceSvc", logstring, EventLogEntryType.Information);
      double result = n1 + n2;
      logstring = $"Result: {result}";
      EventLog.WriteEntry("CalculatorServiceSvc", logstring, EventLogEntryType.Information);
      return result;
    }

    public double Divide(double n1, double n2)
    {
      string logstring = string.Empty;
      logstring = $"{n1} /  {n2}";
      EventLog.WriteEntry("CalculatorServiceSvc", logstring, EventLogEntryType.Information);
      double result = n1 / n2;
      logstring = $"Result: {result}";
      EventLog.WriteEntry("CalculatorServiceSvc", logstring, EventLogEntryType.Information);
      return result;
    }

    public double Multiply(double n1, double n2)
    {
      string logstring = string.Empty;
      logstring = $"{n1} * {n2}";
      EventLog.WriteEntry("CalculatorServiceSvc", logstring, EventLogEntryType.Information);
      double result = n1 * n2;
      logstring = $"Result: {result}";
      EventLog.WriteEntry("CalculatorServiceSvc", logstring, EventLogEntryType.Information);
      return result;
    }

    public string HeartBeat()
    {
      return $"Service is working on " + Environment.MachineName;
    }

    public double Subtract(double n1, double n2)
    {
      string logstring = string.Empty;
      logstring = $"{n1} - {n2}";
      EventLog.WriteEntry("CalculatorServiceSvc", logstring, EventLogEntryType.Information);
      double result = n1 - n2;
      logstring = $"Result: {result}";
      EventLog.WriteEntry("CalculatorServiceSvc", logstring, EventLogEntryType.Information);
      return result;
    }
  }
}
