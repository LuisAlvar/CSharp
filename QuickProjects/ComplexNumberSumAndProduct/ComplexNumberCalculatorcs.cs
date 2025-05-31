using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexNumberSumAndProduct;

public class ComplexNumberCalculatorcs : IComplexNumberCalculator
{
  public string Multiply(string operandA, string operandB)
  {
    throw new NotImplementedException();
  }

  public string Sum(string operandA, string operandB)
  {
    //Stack<string> stkOprandA = new Stack<string>();
    //Stack<string> stkOprandB = new Stack<string>();

    //bool bNextValueIsNegative = false;
    //foreach (char item in operandA.ToArray().Reverse())
    //{
    //  if (IsNegativeSign(item))
    //  {
    //    bNextValueIsNegative = !bNextValueIsNegative;
    //    continue;
    //  }
    //  if (bNextValueIsNegative)
    //  {

    //  }
    //  stkOprandA.Push();
    //}

    throw new NotImplementedException();
  }

  private bool IsNegativeSign(char item)
  {
    return item == '-';
  }
}
