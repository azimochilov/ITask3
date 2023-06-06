namespace Task3;

public class WinnerChecker
{
    public int CheckWinner(int player1Move, int player2Move, string[] choices)
    {
        int halfChoicesCount = choices.Length / 2;

        if (player1Move == player2Move)
            return 0;
        else if (player2Move > player1Move && player2Move - player1Move <= halfChoicesCount
            || player2Move < player1Move && player2Move <= player1Move - halfChoicesCount - 1)
            return 1;
        else
            return 2;
    }
}