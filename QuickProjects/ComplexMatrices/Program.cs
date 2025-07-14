// See https://aka.ms/new-console-template for more information
using ComplexVectors.Entity;

ComplexVector V = new ComplexVector();
V.Add(new ComplexNumber(6, -4));
V.Add(new ComplexNumber(7,3));
V.Add(new ComplexNumber(4.2, -8.1));
V.Add(new ComplexNumber(0,-3));

Console.WriteLine(V.ToString());


ComplexVector W = new ComplexVector();
W.Add(new ComplexNumber(16, 2.3));
W.Add(new ComplexNumber(0, -7));
W.Add(new ComplexNumber(6, 0));
W.Add(new ComplexNumber(0, -4));

Console.WriteLine(W.ToString());

ComplexVector result = V +  W;

Console.WriteLine(result.ToString());
Console.WriteLine(V.Inverse().ToString());


ComplexNumber C1 = new ComplexNumber(3,2);
V = new ComplexVector();
V.Add(new ComplexNumber(6,3));
V.Add(new ComplexNumber(0,0));
V.Add(new ComplexNumber(5,1));
V.Add(new ComplexNumber(4, 0));

result = C1 * V;
Console.WriteLine(result.ToString());