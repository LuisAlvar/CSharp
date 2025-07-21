// See https://aka.ms/new-console-template for more information
using ComplexMatrices.Entity;
using ComplexVectors.Entity;

ComplexMatrix matrices = new ComplexMatrix(2, 2);
//matrices.Add(new ComplexNumber(1, -1));
//matrices.Add(new ComplexNumber(3, 0));
//matrices.Add(new ComplexNumber(2, 2));
//matrices.Add(new ComplexNumber(4, 1));
//Console.WriteLine(matrices.ToString());

//ComplexNumber c1 = new ComplexNumber(0, 2);
//ComplexNumber c2 = new ComplexNumber(1, 2);

//ComplexMatrix result = c1 * (c2 * matrices);
//Console.WriteLine(result.ToString());

//ComplexMatrix result2 = c1 * matrices + c2 * matrices;
//Console.WriteLine(result2.ToString());

ComplexMatrix exercise225 = new ComplexMatrix(3, 3);
exercise225.Add(new ComplexNumber(6, -3));
exercise225.Add(new ComplexNumber(2, 12));
exercise225.Add(new ComplexNumber(0, -19));

exercise225.Add(new ComplexNumber(0, 0));
exercise225.Add(new ComplexNumber(5, 2.1));
exercise225.Add(new ComplexNumber(17, 0));

exercise225.Add(new ComplexNumber(1, 0));
exercise225.Add(new ComplexNumber(2, 5));
exercise225.Add(new ComplexNumber(3, -4.5));
Console.WriteLine("Original: \n" + exercise225.ToString());
Console.WriteLine("Transponse: \n" + exercise225.Transpose().ToString());
Console.WriteLine("Conjugate: \n" + exercise225.Conjugate().ToString());
Console.WriteLine("Adjoint: \n" + exercise225.Adjoint().ToString());
