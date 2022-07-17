using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOF.Behavioral
{
  /* Use when you need different strategies for each context, extensible behavior
   * and isolation (each context has it own behavior). */

  /// <summary>
  /// Context interface containing different strategies for each context.
  /// </summary>
  public interface IStrategyContext
  {
    public IList<IStrategy> Strategies { get; }
  }

  #region different contexts (each one has a different strategy)
  public class ContextA : IStrategyContext
  {
    public ContextA(IList<IStrategy> strategies)
    {
      Strategies = strategies;
    }

    public IList<IStrategy> Strategies { get; }
  }
  public class ContextB : IStrategyContext
  {
    public ContextB(IList<IStrategy> strategies)
    {
      Strategies = strategies;
    }

    public IList<IStrategy> Strategies { get; }
  }
  #endregion

  /// <summary>
  /// Defines a common processing method between the strategies.
  /// </summary>
  public interface IStrategy
  {
    public void Run(float x, float y);
  }
  #region different strategies
  public class Sum : IStrategy
  {
    public void Run(float x, float y)
    {
      Console.WriteLine(x + " + " + y + " = " + (x + y));
    }
  }
  public class Mult : IStrategy
  {
    public void Run(float x, float y)
    {
      Console.WriteLine(x + " * " + y + " = " + (x * y));
    }
  }
  public class Div : IStrategy
  {
    public void Run(float x, float y)
    {
      if (y > 0)
      {
        Console.WriteLine(x + " / " + y + " = " + (x / y));
      }
    }
  }
  #endregion

  public class StrategyClient
  {
    public static void Run()
    {
      /* creates the strategies by context then execute all of them */

      var actionsByContext =
        new List<IStrategyContext>() {
          new ContextA(new List<IStrategy>() { new Sum() }),
          new ContextB(new List<IStrategy>() { new Mult(), new Div() })
        };

      for(int i = 2; i > 0; i--)
      {
        Console.WriteLine("Round " + i);
        for (int c = 0; c < actionsByContext.Count; c++)
        {
          Console.WriteLine("Context " + c);
          actionsByContext[c].Strategies.ToList().ForEach(y => y.Run(i, i - 1));
        }
        Console.WriteLine("");
      }
    }
  }
}
