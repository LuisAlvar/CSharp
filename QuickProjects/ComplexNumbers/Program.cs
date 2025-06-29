// See https://aka.ms/new-console-template for more information
using ComplexNumbers;
using ComplexNumbers.Entity;
using System;
using System.Collections.Generic;

List<string> lstComplextNums = ComplexNumberParser.GetComplexNumbers(args);
Console.WriteLine($"These are the complex numbers: {string.Join(",", lstComplextNums)}");

List<ComplexNumber> lstComplexNums = ComplexNumberParser.TransformToComplexObjects(lstComplextNums);
Console.WriteLine($"Sum Result: {(lstComplexNums[0] + lstComplexNums[1])}");
Console.WriteLine($"Substract Result: {(lstComplexNums[0] - lstComplexNums[1])}");
Console.WriteLine($"Product Result: {(lstComplexNums[0] * lstComplexNums[1])}");
Console.WriteLine($"Divide Result: {(lstComplexNums[0] / lstComplexNums[1])}");

await lstComplexNums[0].Draw();

ComplexNumber c = new ComplexNumber(2,5);
Console.WriteLine(c.Cartesian());
Console.WriteLine(c.Polar());
await c.Draw();