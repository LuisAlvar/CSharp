
// --oprand -3+i --oprand 2-4i
using ComplexNumberSumAndProduct;

List<string> lstComplextNums = ComplexNumberParser.GetComplexNumbers(args);
Console.WriteLine($"These are the complex numbers: {string.Join(",", lstComplextNums)}");

List<int> lstNums = ComplexNumberParser.Transform(lstComplextNums);
Console.WriteLine($"These real numbers within these complex numbers: {string.Join(",", lstNums)}");

string sum = new ComplexNumberCalculator().Sum(lstNums);
Console.WriteLine($"Sum Result: {sum}");

string product = new ComplexNumberCalculator().Multiply(lstNums);
Console.WriteLine($"Product Result: {product}");

sum = new ComplexNumberCalculator().Sum(lstComplextNums[0], lstComplextNums[1]);
Console.WriteLine($"Sum Result: {sum}");