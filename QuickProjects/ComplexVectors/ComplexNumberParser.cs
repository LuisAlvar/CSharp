﻿using ComplexVectors.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ComplexVectors;

public static class ComplexNumberParser
{
  public static List<string> ParserForComplexNumbers(string[] args)
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

  public static List<ComplexNumber> Transform(List<string> ComplexNumbers)
  {
    List<double> result = new List<double>();
    List<ComplexNumber> complexNums = new List<ComplexNumber>();
    bool bNextValueNeg;
    char prevChar = '\0';

    // 1. Translate the string data to double data
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
          double number = double.Parse(character.ToString());
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
    // 2. Translate double data to complex number object data
    int index = 0;
    while ((index*2)+2 <= result.Count)
    {
      var data = result.Skip(index*2).Take(2).ToList();
      complexNums.Add(new ComplexNumber(data[0], data[1]));
      Console.WriteLine($"Ordered Pair: ({data[0]},{data[1]})");
      ++index;
    }
    return complexNums;
  }
}

