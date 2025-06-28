// See https://aka.ms/new-console-template for more information
using ComplexNumbers;
using ComplexNumbers.Entity;

List<string> lstComplextNums = ComplexNumberParser.GetComplexNumbers(args);
Console.WriteLine($"These are the complex numbers: {string.Join(",", lstComplextNums)}");

List<ComplexNumber> lstComplexNums = ComplexNumberParser.TransformToComplexObjects(lstComplextNums);
Console.WriteLine($"Sum Result: {(lstComplexNums[0] + lstComplexNums[1])}");
Console.WriteLine($"Substract Result: {(lstComplexNums[0] - lstComplexNums[1])}");
Console.WriteLine($"Product Result: {(lstComplexNums[0] * lstComplexNums[1])}");
Console.WriteLine($"Divide Result: {(lstComplexNums[0] / lstComplexNums[1])}");

ComplexNumber c = new ComplexNumber(1,1);

Console.WriteLine(c.Cartesian());
Console.WriteLine(c.Polar());