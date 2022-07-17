using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOF.Criational
{
  /* Use to produce families of related objects without needing to specify their concrete classes */

  /// <summary>
  /// To know which factory will be created.
  /// </summary>
  public enum Factories { A, B }

  /// <summary>
  /// Declares common processing among the concrete products.
  /// </summary>
  public interface IProduct
  {
    public void DoSomething();
  }
  #region concrete products implement the interface common processing method
  public class ProductA1 : IProduct
  {
    public void DoSomething()
    {
      Console.WriteLine("ProductA1 is doing something!");
    }
  }
  public class ProductA2 : IProduct
  {
    public void DoSomething()
    {
      Console.WriteLine("ProductA2 is doing something!");
    }
  }
  public class ProductB1 : IProduct
  {
    public void DoSomething()
    {
      Console.WriteLine("ProductB1 is doing something!");
    }
  }
  public class ProductB2 : IProduct
  {
    public void DoSomething()
    {
      Console.WriteLine("ProductB2 is doing something!");
    }
  }
  #endregion

  /// <summary>
  /// Has a method to create and return the factories, another one to create
  /// the concrete product and to call the common product method and also declares
  /// an abstract method to be overridden by subclasses for concrete product creation.
  /// 
  /// The abstract factory is different from the factory method because the latter just
  /// creates one family of products while the former can work with families of products.
  /// </summary>
  public abstract class AbstractFactory
  {
    public abstract IProduct CreateProduct1();
    public abstract IProduct CreateProduct2();

    public void Run()
    {
      var products = new List<IProduct>() { this.CreateProduct1(), this.CreateProduct2() };

      products.ForEach(x => x.DoSomething());
    }

    public static AbstractFactory CreateConcreteFactory(Factories f)
    {
      return f == Factories.A ? new ConcreteFactoryA() : new ConcreteFactoryB();
    }
  }
  #region concrete factories used to override the creation of the concretes products
  public class ConcreteFactoryA : AbstractFactory
  {
    public override IProduct CreateProduct1()
    {
      return new ProductA1();
    }
    public override IProduct CreateProduct2()
    {
      return new ProductA2();
    }
  }
  public class ConcreteFactoryB : AbstractFactory
  {
    public override IProduct CreateProduct1()
    {
      return new ProductB1();
    }
    public override IProduct CreateProduct2()
    {
      return new ProductB2();
    }
  }
  #endregion

  public class AbstractFactoryClient
  {
    /* Firstly create factories then call the common processing */

    public static void Run()
    {
      var factories = new List<Factories>() { Factories.A, Factories.B };

      factories.ForEach(x => AbstractFactory.CreateConcreteFactory(x).Run());
    }
  }
}
