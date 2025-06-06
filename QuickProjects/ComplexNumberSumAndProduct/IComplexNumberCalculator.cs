using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexNumberSumAndProduct
{
  public interface IComplexNumberCalculator
  {
    public string Sum(List<int> ComplexNumberComponents);

    public string Multiply(List<int> ComplexNumberComponents);

    public string Sum(string operandA, string operandB);


    public string Multiply(string operandA, string operandB);
  }
}
