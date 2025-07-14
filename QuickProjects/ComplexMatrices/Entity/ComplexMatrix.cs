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

  public ComplexMatrix(int rows, int colums) 
  {
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
    int findMaxLength = 0;

    for (int i = 0; i < DataStore.Count; i++)
    {
      DataStore[i].
    }
     
  }
}

