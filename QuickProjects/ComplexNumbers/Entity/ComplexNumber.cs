
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScottPlot;
using ScottPlot.WinForms;

namespace ComplexNumbers.Entity;

public class ComplexNumber
{
  public double real { get; private set; }
  public double imaginary { get; private set; }
  public double x_coordinate { get; private set; }
  public double y_coordinate { get; private set; }
  public double theta { get; private set; }
  public double row { get; private set; }

  public ComplexNumber(double Real, double Imaginary)
  {
    real = Real;
    imaginary = Imaginary;
    row = this.Modulus();
    theta = Math.Atan(imaginary / real);
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
    return $"({row.ToString("F2")},{theta.ToString("F2")})";
  }

  public async Task Draw()
  {
    Console.WriteLine("Launching WinForms window...");

    /*
     * TaskCompletionSource acts like a manual signal.
     * When the form is closed, the FormClosed event triggers tcs.SetResult().
     * The await tcs.Task line in Main pauses until the form is closed. 
     * Once closed, the main thread resums cleanly.
     */
    var tcs = new TaskCompletionSource();

    Thread uiThread = new Thread(() =>
    {
      var form = new VectorForm(0,0,this.real, this.imaginary, this.theta, this.ToString());
      form.FormClosed += (s, e) => tcs.SetResult(); // Signal when form is closed
      Application.Run(form);
    });

    uiThread.SetApartmentState(ApartmentState.STA);
    uiThread.Start();

    // Wait for the form to close
    await tcs.Task;

    Console.WriteLine("WinForms window closed. Back on main thread.");
  }


}

public class VectorForm : Form
{
  public VectorForm(double baseX, double baseY, double tipX, double tipY, double theta, string name)
  {
    this.Text = "Vector Plot";
    this.Width = 600;
    this.Height = 400;

    var formsPlot = new FormsPlot
    {
      Dock = DockStyle.Fill
    };
    this.Controls.Add(formsPlot);

    // create a line
    Coordinates arrowTip = new(tipX, tipY);
    Coordinates arrowBase = new(baseX, baseY);
    CoordinateLine arrowLine = new(arrowBase, arrowTip);

    // the shape of the arrowhead can be adjusted
    var skinny = formsPlot.Plot.Add.Arrow(arrowLine);
    skinny.ArrowFillColor = Colors.Green;
    skinny.ArrowLineWidth = 0;
    skinny.ArrowWidth = 3;
    skinny.ArrowheadLength = 20;
    skinny.ArrowheadAxisLength = 20;
    skinny.ArrowheadWidth = 7;

    // offset backs the arrow away from the tip coordinate
    formsPlot.Plot.Add.Marker(arrowLine.End);

    // Add a label at the tip of the arrow 
    formsPlot.Plot.Add.Text($"Vector {name}", arrowTip.WithDelta(.5, 0));



    // Define arc parameters
    var center = new ScottPlot.Coordinates(0, 0);
    double radius = 0.5;
    var start = ScottPlot.Angle.FromDegrees(0);
    var sweep = ScottPlot.Angle.FromRadians(theta);

    // Add the arc
    var arc = formsPlot.Plot.Add.Arc(center, radius, start, sweep);
    arc.LineColor = Colors.Black;
    arc.LineWidth = 2;

    // Midpoint angle for label
    double midAngle = sweep.Degrees / 2;
    double labelRadius = radius + 0.2;
    double labelX = labelRadius * Math.Cos(midAngle * Math.PI / 90);
    double labelY = labelRadius * Math.Sin(midAngle * Math.PI / 90);

    formsPlot.Plot.Add.Text($"θ = {(theta*180/Math.PI).ToString("F2")}", labelX+0.2, labelY);


    // fixing the plot 
    formsPlot.Plot.Axes.SetLimits(-1,10,-1,10);
    formsPlot.Plot.Axes.Bottom.Label.Text = "Real Axis";
    formsPlot.Plot.Axes.Left.Label.Text = "Imaginary Axis";
    formsPlot.Plot.Axes.Bottom.Label.FontSize = 12;
    formsPlot.Plot.Axes.Left.Label.FontSize = 12;

    formsPlot.Refresh();
  }
}

