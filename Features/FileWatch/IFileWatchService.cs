using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatch
{
  public interface IFileWatchService
  {
    public void LoadConfigurations();
    public void StartFileWatcher();
  }
}
