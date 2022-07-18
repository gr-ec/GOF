using GOF.Behavioral;
using GOF.Criational;
using GOF.Structural;

Console.WriteLine("GOF patterns");
Console.WriteLine("");
Console.WriteLine("1. Builder");
Console.WriteLine("2. Factory method");
Console.WriteLine("3. Abstract factory");
Console.WriteLine("4. Strategy");
Console.WriteLine("5. Observer");
Console.WriteLine("6. Facade");
Console.WriteLine("");
Console.Write("Run: ");

var key = Console.ReadKey();

Console.WriteLine("");
Console.WriteLine("");

switch (key.KeyChar.ToString())
{
  case "1": BuilderClient.Run(); break;
  case "2": FactoryMethodClient.Run(); break;
  case "3": AbstractFactoryClient.Run(); break;
  case "4": StrategyClient.Run(); break;
  case "5": ObserverClient.Run(); break;
  case "6": FacadeClient.Run(); break;
}