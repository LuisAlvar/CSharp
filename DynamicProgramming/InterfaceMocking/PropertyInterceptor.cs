using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceMocking
{
  public class PropertyInterceptor
  {
    private readonly Dictionary<string, object> _propertyValues = new Dictionary<string, object>();

    public void SetupGet(string propertyName, object returnValue)
    {
      _propertyValues[propertyName] = returnValue;
    }

    public void Intercept(IInvocation invocation)
    {
      var methodName = invocation.Method.Name;
      if (methodName.StartsWith("get_"))
      {
        var propertyName = methodName.Substring(4);
        if (_propertyValues.TryGetValue(propertyName, out var value))
        {
          invocation.ReturnValue = value;
          return;
        }
      }
      // Procesd momally if property not set up 
      invocation.ReturnValue = null;
    }

  }
}
