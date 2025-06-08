using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComplexNumberSumAndProduct;

public class ComplexNumberCalculator : IComplexNumberCalculator
{

  public ComplexNumberCalculator() { }

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


  public string Sum(string operandA, string operandB)
  {
    string result = string.Empty;
    string opParser1 = string.Empty;
    int op1 = 0;
    int indexOnOp1 = 0;
    string opParser2 = string.Empty;
    int op2 = 0;
    int indexOnOp2 = 0;

    operandA = Regex.Replace(operandA, @"(?<=[^\w]|[-+])i", "1");
    operandB = Regex.Replace(operandB, @"(?<=[^\w]|[-+])i", "1");
    operandA = Regex.Replace(operandA, @"(?<=\d)i", "");
    operandB = Regex.Replace(operandB, @"(?<=\d)i", "");

    Console.WriteLine(operandA);
    Console.WriteLine(operandB);

    int validValue = 0;

    while (indexOnOp2 < operandB.Length)
    {
      if (!int.TryParse(opParser1, out op1)) opParser1 += operandA[indexOnOp1++];
      if (!int.TryParse(opParser2, out op2)) opParser2 += operandB[indexOnOp2++];
      if (int.TryParse(opParser1, out op1) && int.TryParse(opParser2, out op2))
      {
        int sum = op1 + op2;
        if (sum != 0)
        {
          result += sum.ToString();
          if (validValue == 2) result += "i";
          opParser1 = string.Empty;
          opParser2 = string.Empty;
          ++validValue;
        }
      }
    }
    return result;
  }

  public string Multiply(string operandA, string operandB)
  {
    string result = string.Empty;
    string opParser1 = string.Empty;
    int op1 = 0;
    int indexOnOp1 = 0;
    string opParser2 = string.Empty;
    int op2 = 0;
    int indexOnOp2 = 0;

    operandA = Regex.Replace(operandA, @"(?<=[^\w]|[-+])i", "1");
    operandB = Regex.Replace(operandB, @"(?<=[^\w]|[-+])i", "1");
    operandA = Regex.Replace(operandA, @"(?<=\d)i", "");
    operandB = Regex.Replace(operandB, @"(?<=\d)i", "");

    Console.WriteLine(operandA);
    Console.WriteLine(operandB);

    List<int> lstOperandA = new List<int>();
    List<int> lstOperandB = new List<int>();

    while (indexOnOp2 < operandB.Length)
    {
      if (!int.TryParse(opParser1, out op1)) opParser1 += operandA[indexOnOp1++];
      if (!int.TryParse(opParser2, out op2)) opParser2 += operandB[indexOnOp2++];
      if (int.TryParse(opParser1, out op1) && int.TryParse(opParser2, out op2))
      {
        if (lstOperandA.Count == 0)
        {
          lstOperandA.Add(op1);
          lstOperandB.Add(op2);
          opParser1 = string.Empty;
          opParser2 = string.Empty;
        }
        else
        {
          lstOperandA.Add(op1);
          lstOperandB.Add(op2);
        }
      }
    }

    Console.WriteLine($"First complex numbers values: {string.Join(",", lstOperandA)}");
    Console.WriteLine($"Second complex numbers values: {string.Join(",", lstOperandB)}");

    List<int> finalCompute = new List<int>();

    for (int i = 0; i < lstOperandB.Count; ++i)
    {
      for (int j = 0; j < lstOperandA.Count; ++j)
      {
        finalCompute.Add(lstOperandB[i] * lstOperandA[j]);
      }
    }

    finalCompute[finalCompute.Count - 1] = finalCompute[finalCompute.Count - 1] * -1;
    finalCompute.Insert(finalCompute.Count - 2, finalCompute[finalCompute.Count - 1]);
    finalCompute.RemoveAt(finalCompute.Count - 1);

    Console.WriteLine($"List of value from distribution: {string.Join("," , finalCompute)}");

    for (int i = 0; i < finalCompute.Count/2; ++i)
    {
      int compute = (finalCompute[i] + finalCompute[(i + 2)]);
      if (string.IsNullOrEmpty(result))
      {
        result += compute.ToString();
      }
      else
      {
        if (compute > 0) result += "+";
        result += compute.ToString();
      }
    }
    result += "i";
    return result;
  }

}