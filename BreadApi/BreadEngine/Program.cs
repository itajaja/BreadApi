using System;

namespace Hylasoft.BreadEngine
{
  class Program
  {
    static void Main()
    {
      var b = new BreadLoader(@"C:\Program Files (x86)\Siemens\SIT\MES\BIN\OEEBread.dll");
      Console.WriteLine("found " + b.Breads.Count + " Breads inside " + b.Name);
      foreach (var bread in b.Breads)
      {
      }
    }
  }
}
