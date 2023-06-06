using System;
namespace Task3;

public static class Task3
{
    public static void Main(string[] args)
    {
        var exampleText = "Example: dotnet run paper rock scissors.";

        if (args.Length < 3)
        {
            Console.WriteLine($"Number of arguments should be more than 2.\nYou entered {args.Length} arguments.\n" + exampleText);
        }
        else if (args.Length % 2 == 0)
        {
            Console.WriteLine($"Number of arguments should be odd number.\nYou entered {args.Length} arguments.\n" + exampleText);
        }
        else if (args.Any(i => args.Count(j => i == j) > 1))
        {
            string dublicate = args.FirstOrDefault(i => args.Count(j => i == j) > 1);
            Console.WriteLine($"Do not dublicate arguments.\nYou entered '{dublicate}' more than one time.\n" + exampleText);
        }
        else
        {
            var game = new Game(args);
            game.Run();
        }
    }
}