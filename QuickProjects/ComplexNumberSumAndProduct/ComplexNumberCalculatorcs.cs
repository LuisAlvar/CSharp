using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexNumberSumAndProduct;

public class ComplexNumberCalculator : IComplexNumberCalculator
{

  public ComplexNumberCalculator() { }

  public string Multiply(string operandA, string operandB)
  {
    throw new NotImplementedException();
  }

  public string Sum(List<int> ComplexNumberComponents)
  {
    int realNumberSum = 0;
    int imaginaryNumberSum = 0;
    string result = string.Empty;
    for (int i = 0; i < ComplexNumberComponents.Count; i = 2 * i)
    {
      realNumberSum += ComplexNumberComponents[i];
      imaginaryNumberSum += ComplexNumberComponents[++i];
    }
    if (imaginaryNumberSum < 0)
    {
      result = realNumberSum.ToString() + "-" + Math.Abs(imaginaryNumberSum).ToString() + "i";
    }
    else
    {
      result = realNumberSum.ToString() + "+" + Math.Abs(imaginaryNumberSum).ToString() + "i";
    }
    return result;
  }

  public string Multiply(List<int> ComplexNumberComponents)
  {
    List<int> stack = new List<int>();
    for (int op1 = 0; op1 < ComplexNumberComponents.Count / 2; ++op1)
    {
      for (int op2 = ComplexNumberComponents.Count / 2; op2 < ComplexNumberComponents.Count; ++op2)
      {
        stack.Add(ComplexNumberComponents[op1] * ComplexNumberComponents[op2]);
      }
    }
    stack[ComplexNumberComponents.Count - 1] = stack[ComplexNumberComponents.Count - 1] * -1;
    int temp = stack[ComplexNumberComponents.Count - 2];
    stack[ComplexNumberComponents.Count - 2] = stack[ComplexNumberComponents.Count - 1];
    stack[ComplexNumberComponents.Count - 1] = temp;
    return Sum(stack);
  }

}
