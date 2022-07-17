using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOF.Criacionais
{
  /* Use to create objects without specifying its concrete classes (it uses factories instead) and to have a commom operation
   * between them. Benefits include changing concrete objects without breaking the code and combining them with "strategy" pattern
   * to process multiple behaviors at once. */

  /// <summary>
  /// Declares a creator factory method (overriden by subclasses) and a method to do the job.
  /// The factory method is different from the abstract factory because the latter would only create the factory, not the
  /// object. After, each factory would create the object and process it.
  /// </summary>
  public abstract class AbstractMathOpCreator
  {
    /// <summary>
    /// Object factory method that creates the object and it's overriden by subclasses.
    /// </summary>
    public abstract IMathOperation FactoryMethod();
    /// <summary>
    /// Calls factory method (to create the object) then process the information.
    /// </summary>
    public double Operate(double x, double y)
    {
      var op = this.FactoryMethod();

      return op.Calc(x, y);
    }
  }

  #region concrete creators (only responsible for object creation) used by clients
  public class SumCreator : AbstractMathOpCreator
  {
    public override IMathOperation FactoryMethod()
    {
      return new Sum();
    }
  }
  public class MultCreator : AbstractMathOpCreator
  {
    public override IMathOperation FactoryMethod()
    {
      return new Mult();
    }
  }
  #endregion

  /// <summary>
  /// Object interface containing implementors commom method to do the job.
  /// </summary>
  public interface IMathOperation
  {
    double Calc(double x, double y);
  }
  #region objects containing interface method implementation
  public class Sum : IMathOperation
  {
    public double Calc(double x, double y)
    {
      return x + y;
    }
  }
  public class Mult : IMathOperation
  {
    public double Calc(double x, double y)
    {
      return x * y;
    }
  }
  #endregion

  public class FactoryMethodClient
  {
    public static void Run()
    {
      /* To use this pattern first we create the concrete creators then we call its operations. */

      var sum = new SumCreator();
      var mult = new MultCreator();

      int x = 1, y = 2;
      Console.WriteLine(x+" + "+y+" = " + sum.Operate(x, y));
      Console.WriteLine(x+" * "+y+" = " + mult.Operate(x, y));
    }
  }
}
