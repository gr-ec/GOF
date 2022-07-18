using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOF.Criational
{
  /* Use when you need a class that only has one instance with global access.
   * A better way (testable) to use singletons is to use dependency injection APIs. */

  public class Singleton
  {
    private Singleton()
    {
      Birth = DateTime.Now.ToString();
    }

    private static Singleton? _cache;

    public string Birth { get; }

    public static Singleton Instance()
    {
      var lockObj = new object();

      if(_cache == null)
      {
        lock (lockObj)
        {
          _cache = _cache ?? new Singleton();
        }
      }
      
      return _cache;
    }
  }

  public class SingletonClient
  {
    public static void Run()
    {
      Console.WriteLine(Singleton.Instance().Birth);
      Thread.Sleep(2000);
      Console.WriteLine(Singleton.Instance().Birth);
    }
  }
}
