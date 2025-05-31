
// --oprand -3+i --oprand 2-4i
using ComplexNumberSumAndProduct;

List<string> lstComplextNums = ComplexNumberParser.GetComplexNumbers(args);
Console.WriteLine($"These are the complex numbers: {string.Join(",", lstComplextNums)}");

List<int> lstNums = ComplexNumberParser.Transform(lstComplextNums);
Console.WriteLine($"These real numbers within these complex numbers: {string.Join(",", lstNums)}");

