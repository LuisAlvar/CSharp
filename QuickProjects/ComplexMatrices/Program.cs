// See https://aka.ms/new-console-template for more information
using ComplexMatrices.Entity;
using ComplexVectors.Entity;

ComplexMatrix matrices = new ComplexMatrix(2, 2);
matrices.Add(new ComplexNumber(1, -1));
matrices.Add(new ComplexNumber(3, 0));
matrices.Add(new ComplexNumber(2, 2));
matrices.Add(new ComplexNumber(4, 1));
Console.WriteLine(matrices.ToString());

ComplexNumber c1 = new ComplexNumber(0, 2);
ComplexNumber c2 = new ComplexNumber(1, 2);

ComplexMatrix result = c1 * (c2 * matrices);
Console.WriteLine(result.ToString());


ComplexMatrix result2 = c1 * matrices + c2 * matrices;
Console.WriteLine(result2.ToString());