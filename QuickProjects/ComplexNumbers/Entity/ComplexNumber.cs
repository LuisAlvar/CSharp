using ScottPlot;
using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexNumbers.Entity;

public class ComplexNumber
{
  public double real { get; private set; }
  public double imaginary { get; private set; }
  public double x_coordinate { get; private set; } 
  public double y_coordinate { get; private set; }
  public double theta { get; private set; }
  public double row {  get; private set; }

  public ComplexNumber(double Real, double Imaginary)
  {
    real = Real;
    imaginary = Imaginary;
  }

  /// <summary>
  /// Overload the + operator.
  /// Addition between two complex numbers.
  /// </summary>
  /// <param name="a"></param>
  /// <param name="b"></param>
  /// <returns></returns>
  public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
  {
    return new ComplexNumber(a.real + b.real, a.imaginary + b.imaginary);
  }

  /// <summary>
  /// Overload the - operator.
  /// Substration between two complex numbers. 
  /// </summary>
  /// <param name="a"></param>
  /// <param name="b"></param>
  /// <returns></returns>
  public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
  {
    return new ComplexNumber(a.real - b.real, a.imaginary - b.imaginary);
  }

  /// <summary>
  /// Overload the * operator.
  /// Multiplication between two complex numbers
  /// </summary>
  /// <param name="a"></param>
  /// <param name="b"></param>
  /// <returns></returns>
  public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
  {
    double realValue = a.real * b.real - a.imaginary * b.imaginary;
    double ImgValue = a.real * b.imaginary + b.real * a.imaginary;
    return new ComplexNumber(realValue, ImgValue);
  }

  /// <summary>
  /// Overload the / operator. The main reason for doing this way. 
  /// The implmentation is straightforward. 
  /// </summary>
  /// <param name="a"></param>
  /// <param name="b"></param>
  /// <returns></returns>
  public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
  {
    double demon = Math.Pow(a.imaginary, 2) + Math.Pow(b.imaginary, 2);
    double realValue = (a.real * b.real + a.imaginary * b.imaginary) / demon;
    double ImgValue = (b.real * a.imaginary - a.real * b.imaginary) / demon;
    return new ComplexNumber(RoundToSigFigs(realValue, 2), RoundToSigFigs(ImgValue, 2));
  }

  public static double RoundToSigFigs(double number, int sigFigs)
  {
    if (number == 0) return 0;
    double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(number))));
    return Math.Round(number / scale) * scale;
  }

  public override string ToString()
  {
    string _ = $"({real},{imaginary})";
    if (imaginary < 0)
    {
      _ = _ + $" = {real}{imaginary}i";
    }
    else
    {
      _ = _ + $" = {real}+{imaginary}i";
    }
    return _;
  }

  /// <summary>
  /// Return the modulus of this complex number
  /// </summary>
  /// <returns></returns>
  public double Modulus()
  {
    return Math.Sqrt(Math.Pow(real, 2) + Math.Pow(imaginary, 2));
  }

  /// <summary>
  /// Return the conjugate of this complex number
  /// </summary>
  /// <returns></returns>
  public ComplexNumber Conjugation()
  {
    return new ComplexNumber(real, imaginary * -1);
  }

  /// <summary>
  /// The Cartesian representation of the complex number
  /// </summary>
  /// <returns></returns>
  public string Cartesian()
  {
    row = this.Modulus();
    theta = Math.Atan(imaginary/real);
    x_coordinate = row * Math.Cos(theta);
    y_coordinate = row * Math.Sin(theta);
    return $"({x_coordinate.ToString("F2")},{y_coordinate.ToString("F2")})";
  }

  /// <summary>
  /// The Polar representation of the complex number
  /// </summary>
  /// <returns></returns>
  public string Polar()
  {
    row = this.Modulus();
    theta = Math.Atan(imaginary / real);
    return $"({row.ToString("F2")},{theta.ToString("F2")})";
  }

  public void DrawAsVector()
  {
    var form = new Form();
    var plot = new FormsPlot();
    form.Controls.Add(plot);

  }
}

