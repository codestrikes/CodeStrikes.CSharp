using System;
using CodeCompetition.Sdk;
using CodeCompetition.Sdk.Bots;

namespace CodeCompetition.TestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayerBot playerBot = new PlayerBot();
            Kickboxer kickboxer = new Kickboxer();
            Boxer boxer = new Boxer();


            Console.WriteLine($"Executing fight {playerBot} vs {kickboxer}");
            Fight fight = new Fight(playerBot, kickboxer, new StandardGameLogic());
            var result = fight.Execute();
            Console.WriteLine($"Result: {result}");


            Console.WriteLine($"Executing fight {playerBot} vs {boxer}");
            fight = new Fight(playerBot, boxer, new StandardGameLogic());
            result = fight.Execute();
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
