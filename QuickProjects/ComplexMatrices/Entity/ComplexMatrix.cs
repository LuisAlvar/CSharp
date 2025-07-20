using ComplexVectors.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
      DataStore[i]= new ComplexNumber(0,0);
    }
  }

  public void Add(ComplexNumber number) => DataStore.Add(number); 

  public static ComplexMatrix operator *(double scalar, ComplexMatrix matrix)
  {
    throw new NotImplementedException();
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

