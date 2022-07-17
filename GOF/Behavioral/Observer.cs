using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOF.Behavioral
{
  /* Use when there are events that can trigger multiple actions that can be added/removed at runtime */

  /// <summary>
  /// Observers must implement a method to deal with the event (the method can be parameterless)
  /// </summary>
  public interface IObserver
  {
    public void Act(string data);
  }
  #region concrete observers
  public class SendEmail : IObserver
  {
    public void Act(string data)
    {
      Console.WriteLine("E-mail sent due to " + data);
    }
  }
  public class SendNotification : IObserver
  {
    public void Act(string data)
    {
      Console.WriteLine("Notification sent due to " + data);
    }
  }
  public class SendMoney : IObserver
  {
    public void Act(string data)
    {
      Console.WriteLine("Money sent due to " + data);
    }
  }
  #endregion

  /// <summary>
  /// The subject knows the observers and can notify them. Also, it can subscribe and unsubscribe observers.
  /// </summary>
  public interface ISubject
  {
    public IList<IObserver> Observers { get; set; }

    public void Notify(string data);

    public void Subscribe(IObserver observer);
    public void Unsubscribe(IObserver observer);
  }
  public class Subject : ISubject
  {
    public Subject(IList<IObserver> observers)
    {
      Observers = observers;
    }

    public IList<IObserver> Observers { get; set; }

    public void Notify(string data)
    {
      foreach(var o in this.Observers)
      {
        o.Act(data);
      }
    }

    public void Subscribe(IObserver observer)
    {
      if (!Observers.Contains(observer))
      {
        Observers.Add(observer);
      }
    }

    public void Unsubscribe(IObserver observer)
    {
      if (Observers.Contains(observer))
      {
        Observers.Remove(observer);
      }
    }
  }

  public class ObserverClient
  {
    /* Create the "subject" passing "observers" then notify them */

    public static void Run()
    {
      var subject = new Subject(new List<IObserver>() { new SendEmail(), new SendNotification() });

      var evt = "\"sign in\"";
      Console.WriteLine(evt + " event");
      subject.Notify(evt);
      Console.WriteLine("");

      while (subject.Observers.Any())
      {
        subject.Unsubscribe(subject.Observers.First());
      }

      evt = "\"logout\"";
      Console.WriteLine(evt + " event");
      subject.Notify(evt);
      Console.WriteLine("");

      subject.Subscribe(new SendMoney());

      evt = "\"anniversary\"";
      Console.WriteLine(evt + " event");
      subject.Notify(evt);
      Console.WriteLine("");
    }
  }
}
