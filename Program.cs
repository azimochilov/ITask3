public class RockPaperScissorsGame
{
    private static readonly Dictionary<int, string> moves = new Dictionary<int, string>
    {
        { 1, "rock" },
        { 2, "paper" },
        { 3, "scissors" }
    };

    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Rock Paper Scissors!");
        Console.WriteLine("Available moves:");
        foreach (var move in moves)
        {
            Console.WriteLine($"{move.Key} - {move.Value}");
        }
        Console.WriteLine("0 - exit");

        while (true)
        {
            int userMove = GetUserMove();
            if (userMove == 0)
            {
                Console.WriteLine("Exiting the game.");
                break;
            }

            string userMoveString = moves[userMove];
            string computerMove = GenerateComputerMove();
            Console.WriteLine("Your move: " + userMoveString);
            Console.WriteLine("Computer move: " + computerMove);

            int result = DetermineWinner(userMove, computerMove);
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
        }
    }

    private static int GetUserMove()
    {
        while (true)
        {
            Console.Write("Enter your move: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int userMove) && (userMove >= 0 && userMove <= 3))
            {
                return userMove;
            }
            Console.WriteLine("Invalid input. Please try again.");
        }
    }

    private static string GenerateComputerMove()
    {
        Random random = new Random();
        int randomMove = random.Next(1, 4);
        return moves[randomMove];
    }

    private static int DetermineWinner(int userMove, string computerMove)
    {
        if (userMove == GetMoveNumber(computerMove))
        {
            return 0; // Draw
        }
        else if ((userMove == 1 && GetMoveNumber(computerMove) == 3) ||
                 (userMove == 2 && GetMoveNumber(computerMove) == 1) ||
                 (userMove == 3 && GetMoveNumber(computerMove) == 2))
        {
            return 1; // User wins
        }
        else
        {
            return -1; // Computer wins
        }
    }

    private static int GetMoveNumber(string move)
    {
        return moves.FirstOrDefault(x => x.Value == move).Key;
    }
}
