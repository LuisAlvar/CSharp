using ComplexVectors.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexVectors.Entity;

public class ComplexVector
{

  public List<ComplexNumber> DataStore { get; private set; }

  public ComplexVector() { }

  public ComplexVector(int size)
  {
    DataStore = new List<ComplexNumber>(size);
    for (int i = 0; i < size; i++)
    {
      DataStore.Add(new ComplexNumber(0, 0));
    }
  }

  public void Add(ComplexNumber number)
  {
    if (DataStore == null) DataStore = new List<ComplexNumber>();
    DataStore.Add(number);
  }
  /// <summary>
  /// Elementwise Addition
  /// </summary>
  /// <param name="a"></param>
  /// <param name="b"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentException"></exception>
  public static ComplexVector operator +(ComplexVector a, ComplexVector b)
  {
    if (a == null) throw new ArgumentNullException("left side operand is null");
    if (b == null) throw new ArgumentNullException("right side operand is null");
    if (a.DataStore.Count != b.DataStore.Count) throw new ArgumentException("operands dont have the same number of elements");
    ComplexVector result = new ComplexVector(a.DataStore.Count);
    Parallel.For(0, a.DataStore.Count, i => {
      result.DataStore[i] = a.DataStore[i] + b.DataStore[i];
    });
    return result;
  }

  /// <summary>
  /// Scalar Multiplication
  /// </summary>
  /// <param name="scalar"></param>
  /// <param name="a"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ComplexVector operator *(ComplexNumber scalar, ComplexVector a)
  {
    if (a == null) throw new ArgumentNullException("right side operand is null");
    ComplexVector result = new ComplexVector(a.DataStore.Count);
    Parallel.For(0, a.DataStore.Count, i => {
      result.DataStore[i] = scalar*a.DataStore[i];
    });
    return result;
  }

  /// <summary>
  /// Inverse of this Complex Vector
  /// </summary>
  /// <returns></returns>
  public ComplexVector Inverse()
  {
    ComplexVector result = new ComplexVector(DataStore.Count);
    Parallel.For(0, DataStore.Count, i => {
      result.DataStore[i] =  -1 * DataStore[i];
    });
    return result;
  }

  public override string ToString()
  {
    string _ = string.Empty;
    for (int i = 0; i < DataStore.Count; i++)
    {
      _ += DataStore[i].ToString() + "\n";
    }
    return _;
  }

}
