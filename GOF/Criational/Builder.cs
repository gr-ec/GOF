using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOF.Criational
{
  /* Use to build complex objects and to avoid giant class constructors. */

  /// <summary>
  /// Define the steps to build a toy to be called by client or director.
  /// </summary>
  public interface IToyBuilder
  {
    public void SetShape(string svg, int heightCm);
    public void SetRecommendedAge(int minAge);
    public void SetSound(string decodedSound);
    public void SetEngine(int manufacturer, int model);
    public void SetBatteries(int wattsNeeded);
  }
  /// <summary>
  /// Implement interface steps and define methods to get the doll and to reset the doll (to build the next).
  /// </summary>
  public class DollBuilder : IToyBuilder
  {
    private Doll Doll = new Doll();

    public DollBuilder()
    {
      this.Reset();
    }

    #region interface implementation
    public void SetShape(string svg, int heightCm)
    {
      Doll.Shape = new Shape(svg, heightCm);
    }
    public void SetRecommendedAge(int minAge)
    {
      Doll.MinAge = minAge;
    }
    public void SetSound(string decodedSound)
    {
      Doll.Sound = new Sound(decodedSound);
    }
    public void SetEngine(int manufacturer, int model)
    {
      Doll.Engine = new Engine(manufacturer, model);
    }
    public void SetBatteries(int wattsNeeded)
    {
      var batteries = new List<Battery>();
      var batteryType = "AA";
      var wattsPerBattery = batteryType == "AA" ? 10.0 : 15.0;
      var batteriesNeeded = (int)Math.Ceiling(wattsNeeded / wattsPerBattery);
      for (var i = 0; i < batteriesNeeded; i++)
      {
        batteries.Add(new Battery((int)wattsPerBattery, batteryType));
      }

      Doll.Batteries = batteries;
    }
    #endregion

    public void Reset()
    {
      this.Doll = new Doll();
    }

    public Doll GetDoll()
    {
      var doll = this.Doll;

      this.Reset();

      return doll;
    }
  }

  #region not important (just model)
  public class Battery
  {
    public Battery(int watts, string type)
    {
      Watts = watts;
      Type = type;
    }

    public int Watts { get; set; }
    public string Type { get; set; }
  }
  public class Sound
  {
    public Sound(string decodedSound)
    {
      DecodedSound = decodedSound;
    }

    public string DecodedSound { get; set; }
  }
  public class Engine
  {
    public Engine()
    {
    }
    public Engine(int manufacturer, int model)
    {
      Manufacturer = manufacturer;
      Model = model;
    }

    public int Manufacturer { get; set; }
    public int Model { get; set; }
  }
  public class Shape
  {
    public Shape()
    {
      Svg = "";
    }
    public Shape(string svg, int heightCm)
    {
      Svg = svg;
      HeightCm = heightCm;
    }

    public string Svg { get; set; }
    public int HeightCm { get; set; }
  }
  public class Doll
  {
    public Doll()
    {
      Shape = new Shape();
      MinAge = 0;
      Sound = new Sound("");
      Batteries = new List<Battery>();
      Engine = new Engine();
    }

    public Shape Shape { get; set; }
    public int MinAge { get; set; }
    public Sound Sound { get; set; }
    public Engine Engine { get; set; }
    public IList<Battery> Batteries { get; set; }

    public void Print()
    {
      Console.WriteLine("Svg: " + Shape.Svg + ", height (cm): " + Shape.HeightCm);
      Console.WriteLine("Min. age: " + MinAge);
      Console.WriteLine("Sound: " + Sound.DecodedSound);
      Console.WriteLine("Manufacturer: " + Engine.Manufacturer + ", model (cm): " + Engine.Model);
      var strBatt = string.Join(", ", Batteries.Select(x => x.Watts + "W " + x.Type));
      Console.WriteLine("Bateries: " + strBatt);
    }
  }
  #endregion

  /// <summary>
  /// Optional, but is good to centralize the build of different kinds of objects. Each method contains
  /// the steps to build on kind of object (clients just call one of them - it's reusable).
  /// </summary>
  public class Director
  {
    readonly IToyBuilder Builder;

    public Director(IToyBuilder builder)
    {
      Builder = builder;
    }

    public void BuildBuzzLightYear()
    {
      Builder.SetShape("BuzzSVG", 10);
      Builder.SetRecommendedAge(6);
      Builder.SetSound("BUZZZ!!");
      Builder.SetEngine(712, 256);
      Builder.SetBatteries(42);
    }
    public void BuildBarbie()
    {
      Builder.SetShape("BarbieSVG", 8);
      Builder.SetRecommendedAge(5);
    }
  }

  public class BuilderClient
  {
    public static void Run()
    {
      /* To use this pattern we create the builder, the director (passing the builder)
       * and then we build, get and process the different objects that we are building, one by one.
       */

      var builder = new DollBuilder();
      var director = new Director(builder);

      //build, get and process buzz
      director.BuildBuzzLightYear();
      var buzz = builder.GetDoll();
      buzz.Print();

      //build, get and process barbie
      director.BuildBarbie();
      var barbie = builder.GetDoll();
      barbie.Print();
    }
  }
}
