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


            Console.WriteLine($"Executing fight: {playerBot} vs {kickboxer}");
            Fight fight = new Fight(playerBot, kickboxer, new StandardGameLogic());
            var result = fight.Execute();
            // Uncomment to see round results
            // result.RoundResults.ForEach(Console.WriteLine);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine();

            Console.WriteLine($"Executing fight: {playerBot} vs {boxer}");
            fight = new Fight(playerBot, boxer, new StandardGameLogic());
            result = fight.Execute();
            // Uncomment to see round results
            //result.RoundResults.ForEach(Console.WriteLine);
            Console.WriteLine($"Result: {result}");

            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
