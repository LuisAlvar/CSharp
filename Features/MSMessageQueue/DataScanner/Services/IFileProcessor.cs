using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScanner.Services
{
  public interface IFileProcessor
  {
    public void ProcessFilename(string fileName);
  }
}
