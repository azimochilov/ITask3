using System;
using System.Security.Cryptography;

public class RockPaperScissorsGame
{
    public static void Main(string[] args)
    {
        if (args.Length < 3 || args.Length % 2 == 0)
        {
            Console.WriteLine("Incorrect number of arguments. Please provide an odd number of non-repeating strings.");
            Console.WriteLine("Example: dotnet run rock paper scissors lizard Spock");
            return;
        }

        Console.WriteLine("HMAC key: " + GenerateKey());
        Console.WriteLine("Available moves:");
        for (int i = 0; i < args.Length; i++)
        {
            Console.WriteLine((i + 1) + " - " + args[i]);
        }
        Console.WriteLine("0 - exit");
        Console.WriteLine("? - help");

        int userMove = GetUserMove(args.Length);
        if (userMove == 0)
        {
            Console.WriteLine("Exiting the game.");
            return;
        }

        string userMoveString = args[userMove - 1];
        string computerMove = GenerateComputerMove(args.Length);
        Console.WriteLine("Your move: " + userMoveString);
        Console.WriteLine("Computer move: " + computerMove);

        int result = DetermineWinner(userMove, int.Parse(computerMove), args.Length);
        if (result == 0)
        {
            Console.WriteLine("It's a draw!");
        }
        else if (result > 0)
        {
            Console.WriteLine("You win!");
        }
        else
        {
            Console.WriteLine("Computer wins!");
        }

        Console.WriteLine("HMAC key: " + GenerateKey());
    }

    private static string GenerateKey()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] keyBytes = new byte[32];
            rng.GetBytes(keyBytes);
            return BitConverter.ToString(keyBytes).Replace("-", "");
        }
    }

    private static int GetUserMove(int numMoves)
    {
        int userMove;
        do
        {
            Console.Write("Enter your move: ");
            string input = Console.ReadLine();
            if (input == "?")
            {
                DisplayHelpTable(numMoves);
                continue;
            }
            if (int.TryParse(input, out userMove) && userMove >= 0 && userMove <= numMoves)
            {
                return userMove;
            }
            Console.WriteLine("Invalid input. Please try again.");
        } while (true);
    }

    private static void DisplayHelpTable(int numMoves)
    {
        Console.WriteLine("╔═══════════════════════════════════╗");
        Console.WriteLine("║           Help - Moves             ║");
        Console.WriteLine("╠═══════════════════════════════════╣");
        Console.WriteLine("║   Move   ║   Win    ║   Lose   ║Draw║");
        Console.WriteLine("╠══════════╬═════════╬═════════╬════╣");

        // Define an array of colors
        ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Magenta };

        for (int i = 1; i <= numMoves; i++)
        {
            string move = GetMoveName(i);
            string win = GetMoveName((i % numMoves) + 1);
            string lose = GetMoveName((i + 1) % numMoves + 1);

            // Set the color based on the move index
            ConsoleColor color = colors[i - 1];
            Console.ForegroundColor = color;

            Console.WriteLine($"║ {move,-8} ║ {win,-8} ║ {lose,-8} ║Draw║");

            Console.ResetColor();
        }

        Console.WriteLine("╚══════════╩═════════╩═════════╩════╝");
    }

    private static string GetMoveName(int moveIndex)
    {
        return moveIndex.ToString();
    }

    private static string GenerateComputerMove(int numMoves)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] randomNumber = new byte[1];
            rng.GetBytes(randomNumber);
            return (randomNumber[0] % numMoves + 1).ToString();
        }
    }

    private static int DetermineWinner(int userMove, int computerMove, int numMoves)
    {
        int halfMoves = numMoves / 2;
        if (userMove == computerMove)
        {
            return 0; // Draw
        }
        else if ((userMove > computerMove && userMove - computerMove <= halfMoves) || (userMove < computerMove && computerMove - userMove > halfMoves))
        {
            return 1; // User wins
        }
        else
        {
            return -1; // Computer wins
        }
    }
}
