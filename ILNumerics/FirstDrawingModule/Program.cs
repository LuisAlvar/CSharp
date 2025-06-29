using System;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals;

Array<double> A = zeros<double>(5, 5);  // create a 5x5 matrix of 0's
Array<double> B = arange<double>(1, 5);  // creates a vector, from 1...5
A["0:6:24"] = B;                         // sets the diagonal elements of A

Array<double> RS = ones<double>(5, 1) * rand(1, 3);    // broadcasting operations
Array<double> Inv = linsolve(A, RS);   // solves a linear equation

Console.WriteLine($"A :\r\n{A}");         // nicely formatted output for arrays
Console.WriteLine($"RS :\r\n{RS}");
Console.WriteLine($"Inv :\r\n{Inv}");

Console.WriteLine($"Check: {(bool)allall((multiply(A, Inv) - RS) < eps)}");