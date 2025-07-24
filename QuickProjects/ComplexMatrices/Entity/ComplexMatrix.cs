using ComplexVectors.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace ComplexMatrices.Entity;

public class ComplexMatrix
{
  List<ComplexNumber> DataStore { get; set; } = new List<ComplexNumber>();
  private int _columns {  get; set; } = 0;
  private int _rows { get; set; } = 0;

  public ComplexMatrix(int rows, int colums) 
  {
    _rows = rows;
    _columns = colums;
    DataStore = new List<ComplexNumber>(rows * colums);
    for (int i = 0; i < DataStore.Count; i++)
    {
      DataStore[i] = new ComplexNumber(0,0);
    }
  }

  public void Add(ComplexNumber number) => DataStore.Add(number); 
  
  /// <summary>
  /// Convert rows into columns A[row, column] => A^T[column, row]
  /// <br/>
  /// Time Complexity is still O(n/_columns) ~ O(n)
  /// </summary>
  /// <returns></returns>
  public ComplexMatrix Transpose()
  {
    ComplexMatrix result = new ComplexMatrix(_columns, _rows);
    for (int k = 0; k < _columns; ++k)
    {
      result.Add(DataStore[k]);
      for (int j = 1; j < _rows; j++)
      {
        result.Add(DataStore[_rows * j + k]);
      }
    }
    return result;
  }

  /// <summary>
  /// Within each element of a complex matrix return its conjugation as a new matrix
  /// </summary>
  /// <returns></returns>
  public ComplexMatrix Conjugate()
  {
    ComplexMatrix result = new ComplexMatrix(_rows, _columns);
    for (int i = 0; i < DataStore.Count; i++)
    {
      result.Add(DataStore[i].Conjugation());
    }
    return result;
  }

  /// <summary>
  /// Convert rows into columns of a given complex matrix along with its conjugation of each element.
  /// </summary>
  /// <returns></returns>
  public ComplexMatrix Adjoint()
  {
    ComplexMatrix result = new ComplexMatrix(_columns, _rows);
    for (int k = 0; k < _columns; ++k)
    {
      result.Add(DataStore[k].Conjugation());
      for (int j = 1; j < _rows; j++)
      {
        result.Add(DataStore[_rows * j + k].Conjugation());
      }
    }
    return result;
  }

  public static ComplexMatrix operator *(ComplexNumber scalar, ComplexMatrix matrix)
  {
    ComplexMatrix result = new ComplexMatrix(matrix._rows, matrix._columns);
    for (int i = 0; i < matrix.DataStore.Count; i++)
    {
      result.Add(scalar * matrix.DataStore[i]);
    }
    return result;
  }

  public static ComplexMatrix operator +(ComplexMatrix a, ComplexMatrix b)
  {
    if (a._rows != b._rows || a._columns != b._columns) throw new ArgumentException("Both matrices need to be mxn size.");
    ComplexMatrix result = new ComplexMatrix(a._rows, a._columns);
    for (int i = 0; i < b.DataStore.Count; i++)
    {
      result.Add(a.DataStore[i] + b.DataStore[i]);
    }
    return result;
  }

  public static ComplexMatrix operator *(ComplexMatrix left, ComplexMatrix right)
  {
    if (left._columns != right._rows) throw new ArgumentException("These matrice can not be multipled together");
    ComplexMatrix result = new ComplexMatrix(left._rows, right._columns);

    int left_col_index = 0;
    int left_row_index = 0;
    int current_left_index = 0;
    int right_col_index = 0;
    int right_row_index = 0;
    int current_right_index = 0;

    ComplexNumber sum = new ComplexNumber(0,0);

    while(left_row_index < left._rows)
    {
      left_col_index = 0;
      right_row_index = 0;
      sum = new ComplexNumber(0,0);

      while (right_row_index < right._rows)
      {
        current_left_index = left._columns * left_row_index + left_col_index;
        current_right_index = right._columns * right_row_index + right_col_index;

        //Console.WriteLine($"{current_left_index}-{current_right_index}");

        sum += left.DataStore[current_left_index] * right.DataStore[current_right_index];

        ++left_col_index;
        ++right_row_index;
      }

      result.Add(sum);
      if(right_col_index == right._columns - 1)
      {
        ++left_row_index;
        right_col_index = 0;
      }
      else
      {
        ++right_col_index;
      }
    }

    return result;
  }

  public override string ToString()
  {
    int paddding = 2;
    int maxDataLength = 0;
    int columnIndex = 0;
    int deltaLength = 0;
    string result = string.Empty;
    result = $"{this.GetType().Name}[{_rows},{_columns}]\n";

    for (int i = 0; i < DataStore.Count; i++)
    {
      if (DataStore[i].ToString().Length > maxDataLength) maxDataLength = DataStore[i].ToString().Length;
    }

    for (int i = 0; i < DataStore.Count; i++)
    {
      deltaLength = Math.Abs(DataStore[i].ToString().Length - maxDataLength);
      if (columnIndex != _columns)
      {
        result += DataStore[i].ToString() + WhiteSpacePadding(deltaLength, paddding);
        columnIndex++;
      }
      else
      {
        columnIndex = 0;
        result += '\n';
        result += DataStore[i].ToString() + WhiteSpacePadding(deltaLength, paddding);
        columnIndex++;
      }
    }

    return result;
  }

  private string WhiteSpacePadding(int num, int padding)
  {
    string _ = string.Empty;
    for (int i = 0; i < num + padding; i++)
    {
      _ += " ";
    }
    return _;
  }
}

