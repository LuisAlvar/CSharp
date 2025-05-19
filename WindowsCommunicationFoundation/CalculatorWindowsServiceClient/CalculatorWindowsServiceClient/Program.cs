using CalculatorService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorWindowsServiceClient
{
  internal class Program
  {
    static void Main(string[] args)
    {
      using (var service = new CalculatorServiceSvcClient("BasicHttpBinding_ICalculatorServiceSvc"))
      {
        Console.WriteLine(service.HeartBeat());

        double value1 = 100.00D;
        double value2 = 15.99D;
        double result = service.Add(value1, value2);
        Console.WriteLine("Add({0},{1}) = {2}", value1, value2, result);

        result = service.Subtract(145.00D, 76.54D);
        Console.WriteLine("Subtract({0},{1}) = {2}", 145.00D, 76.54D, result);

        result = service.Multiply(9.00D, 81.25D);
        Console.WriteLine("Multiply({0},{1}) = {2}", 9.00D, 81.25D, result);

        result = service.Divide(22.00D, 7.00D);
        Console.WriteLine("Divide({0},{1}) = {2}", 22.00D, 7.00D, result);
      }


      var factory = new ChannelFactory<ICalculatorServiceSvc>(
          new NetTcpBinding(SecurityMode.None),  // Matches server settings
          new EndpointAddress("net.tcp://localhost:8988/CalculatorServiceSvc"));

      var proxy = factory.CreateChannel();
      Console.WriteLine(proxy.Add(5, 3)); // Example call

      CalculatorServiceSvcClient tcpClient = new CalculatorServiceSvcClient("NetTcpBinding_ICalculatorServiceSvc");
      Console.WriteLine(tcpClient.Add(7, 2)); // Test call


      Console.ReadKey();
    }
  }
}
