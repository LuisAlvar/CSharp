using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexNumberSumAndProduct;

public static class ComplexNumberParser
{
  public static List<string> GetComplexNumbers(string[] args)
  {
    List<string> result = new List<string>();
    bool bHoldNext = false;
    foreach (string arg in args)
    {
      if (arg.Contains("oprand"))
      {
        bHoldNext = true;
        continue;
      }
      if (bHoldNext)
      {
        result.Add(arg);
        bHoldNext = false;
      }
    }
    return result;
  }

  public static List<int> Transform(List<string> ComplexNumbers)
  {
    List<int> result = new List<int>();
    bool bNextValueNeg;
    char prevChar = '\0';
    foreach (string complexNum in ComplexNumbers)
    {
      bNextValueNeg = false;
      foreach (char character in complexNum.ToCharArray())
      {
        if (character == '-')
        {
          bNextValueNeg = true;
        }
        else if (char.IsDigit(character))
        {
          int number = int.Parse(character.ToString());
          if (bNextValueNeg) number = number * -1;
          result.Add(number);
          bNextValueNeg = false;
        }
        else if (char.IsLetter(character) && character == 'i' && !char.IsDigit(prevChar))
        {
          int number = 1;
          if (bNextValueNeg) number = number * -1;
          result.Add(number);
          bNextValueNeg = false;
        }
        prevChar = character;
      }
    }
    return result;
  }
}

