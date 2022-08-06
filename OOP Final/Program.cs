using System;
using Travordle;
namespace OOPFinal
{ 
    internal static class Program
    {
        //Very basic console app to show generic functions of travordle game API. Only have routes for a starting city of "Chicago" and "St Paul" so only use them as starting city
        //and guess with ~top 15 pop cities (sorry didn't get menu display implimented or show avalible cities)
        static void Main()
        {

            string input;
            var Game = new TravordleGame();
            int menuState = -1;
            KeyValuePair<int, string> currentMessage = new KeyValuePair<int, string>();
                do
                {
                    Console.WriteLine("Enter Starting city");
                    input = Console.ReadLine();
                    currentMessage = Game.NewGame(input);
                    //Console.WriteLine(currentMessage.Value);
                    //Console.WriteLine("Guess a city and mode in format: <city>,<mode(Bus/Train/Flight)>");
                    while (Game.ActiveGame)
                    {
                        Console.WriteLine(currentMessage.Value);
                        Console.WriteLine("Guess a city and mode in format: <city>,<mode(Bus/Train/Flight)>");
                        input = Console.ReadLine();
                        var section = input.Split(',');
                        currentMessage = Game.RouteGuess(section[0], section[1]);
                        if (currentMessage.Key == -1)
                            Console.WriteLine(currentMessage.Value);
                        if (currentMessage.Key == -2)
                        {
                            Console.WriteLine(currentMessage.Value);
                            menuState = 0;
                        }
                    }

                }
                while (!Game.ActiveGame);

        }
    }
}
