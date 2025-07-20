// See https://aka.ms/new-console-template for more information
using ComplexMatrices.Entity;
using ComplexVectors.Entity;

ComplexNumber C1 = new ComplexNumber(1,-1);
ComplexNumber C2 = new ComplexNumber(2, 2);

ComplexNumber C3 = new ComplexNumber(3, 0);
ComplexNumber C4 = new ComplexNumber(4, 1);


//Console.WriteLine(C1.ToString());
//Console.WriteLine(C2.ToString());
//Console.WriteLine(C3.ToString());
//Console.WriteLine(C4.ToString());

ComplexMatrix matrices = new ComplexMatrix(2, 2);
matrices.Add(C1);
matrices.Add(C3);
matrices.Add(C2);
matrices.Add(C4);
Console.WriteLine(matrices.ToString());