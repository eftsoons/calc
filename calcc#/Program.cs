using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

string[][] buttons = [["(", ")", "<", "C"], ["1", "2", "3", "+"], ["4", "5", "6", "-"], ["7", "8", "9", "*"], [".", "0", "=", "/"]];
int[] coord = [0, 0];

double calc = 0;
string calcresponse = "";

string buttonconsole(string calcresponse, double calc)
{
    string response = $"Решенение примера: {calc}\nПример: {calcresponse}";
    for (int y = 0; y < buttons.Length; y++)
    {
        response += "\n|";
        for (int x = 0; x < buttons[y].Length; x++)
        {
            response += coord[0] == x && coord[1] == y ? $"* {buttons[y][x]} *|" : $"  {buttons[y][x]}  |";
        }
    }

    return response;
}

double Eval(string input)
{
    try
    {
        calcresponse = "";
        return Convert.ToDouble(new DataTable().Compute(input, null));
    }
    catch
    {
        return 0;
    }
}

void reactionkey(ConsoleKeyInfo click)
{
    if (ConsoleKey.DownArrow == click.Key || ConsoleKey.S == click.Key)
    {
        if (coord[1] < buttons.Length - 1)
        {
            coord[1] += 1;
        }
    }
    else if (ConsoleKey.UpArrow == click.Key || ConsoleKey.W == click.Key)
    {
        if (coord[1] > 0)
        {
            coord[1] -= 1;
        }
    }
    else if (ConsoleKey.RightArrow == click.Key || ConsoleKey.D == click.Key)
    {
        if (coord[0] < 3)
        {
            coord[0] += 1;
        }
    }
    else if (ConsoleKey.LeftArrow == click.Key || ConsoleKey.A == click.Key)
    {
        if (coord[0] > 0)
        {
            coord[0] -= 1;
        }
    }
    else if (ConsoleKey.Enter == click.Key)
    {
        if (buttons[coord[1]][coord[0]] == "=")
        {
            calc = Eval(calcresponse);
        }
        else if (buttons[coord[1]][coord[0]] == "C")
        {
            calc = 0;
            calcresponse = "";
        }
        else if (buttons[coord[1]][coord[0]] == "<")
        {
            if (calcresponse.Length > 0)
            {
                calcresponse = calcresponse.Remove(calcresponse.Length - 1);
            }
        }
        else
        {
            calcresponse += buttons[coord[1]][coord[0]];
        }
    }
}

while (true)
{
    Console.Clear();

    Console.WriteLine(buttonconsole(calcresponse, calc));

    ConsoleKeyInfo click = Console.ReadKey();

    reactionkey(click);
}