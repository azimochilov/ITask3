namespace Task3;

public class Game
{
    private Table table;
    private Random random;
    private string[] choices;
    private WinnerChecker checker;
    private KeyHMACGenerator keyHMACGenerator;

    public Game(string[] choices)
    {
        this.choices = choices;
        this.table = new Table();
        this.random = new Random();
        this.checker = new WinnerChecker();
        this.keyHMACGenerator = new KeyHMACGenerator();
    }

    public void Run()
    {
        while (true)
        {
            var computerMoveNumber = random.Next(choices.Length) + 1;

            var hmacKeySecretText = "SecretKey";
            var hmacKey = keyHMACGenerator.GenerateKey(hmacKeySecretText);
            var hmac = keyHMACGenerator.GenerateHMAC(choices[computerMoveNumber - 1], hmacKey);
            Console.WriteLine("HMAC: " + hmac);

            Console.WriteLine("Available moves:");
            for (int i = 0; i < choices.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {choices[i]}");
            }

            Console.WriteLine($"0 - exit");
            Console.WriteLine($"? - help");

            Console.WriteLine("Enter your move: ");
            var input = Console.ReadLine();

            if (input == "?")
            {
                table.PrintHelpTable(choices);
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
            }
            else if (input == "0")
            {
                Console.WriteLine("Bye, bye!");
                Environment.Exit(0);
            }
            else if (input.Any(c => !char.IsDigit(c)) || int.Parse(input) < 1 || int.Parse(input) > choices.Length)
            {
                Console.WriteLine("Please, enter valid input!");
            }
            else
            {
                int playerMoveNumber = int.Parse(input);

                Console.WriteLine("Your move: " + choices[playerMoveNumber - 1]);
                Console.WriteLine("Computer's move: " + choices[computerMoveNumber - 1]);
                Console.WriteLine("HMAC key: " + hmacKey);

                int winner = checker.CheckWinner(playerMoveNumber, computerMoveNumber, choices);
                Console.WriteLine(winner == 0 ? "Draw." : winner == 1 ? "You won" : "Computer won");
            }
        }
    }
}