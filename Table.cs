using ConsoleTables;

namespace Task3;

public class Table
{
    public void PrintHelpTable(string[] choices)
    {
        var columns = new List<string>();
        columns.AddRange(new string[] { "Number", "Name" });
        columns.AddRange(choices);

        var table = new ConsoleTable(columns.ToArray());

        var checker = new WinnerChecker();

        for (int i = 0; i < choices.Length; i++)
        {
            var row = new List<string>() { (i + 1).ToString(), choices[i] };
            for (int j = 0; j < choices.Length; j++)
            {
                var winResult = checker.CheckWinner(i + 1, j + 1, choices);

                if (winResult == 1)
                    row.Add("win");
                else if (winResult == 2)
                    row.Add("lose");
                else
                    row.Add("draw");
            }
            table.AddRow(row.ToArray());
        }
        table.Write();
    }
}