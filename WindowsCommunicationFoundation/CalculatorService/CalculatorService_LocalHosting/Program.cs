using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService_LocalHosting
{
  class Program
  {
    static void Main(string[] args)
    {

      using (ServiceHost selfHost = new ServiceHost(typeof(CalculatorService.Services.CalculatorServiceSvc)))
      {
        // Step 5: Start the service.
        selfHost.Open();
        
        Console.WriteLine($"Service running at {selfHost.BaseAddresses[0].ToString()}");
        Console.WriteLine($"Service running at {selfHost.BaseAddresses[1].ToString()}");


        // Close the ServiceHost to stop the service.
        Console.WriteLine("Press <Enter> to terminate the service.");
        Console.WriteLine();
        Console.ReadKey();
        selfHost.Close();
      }
    }
  }
}
