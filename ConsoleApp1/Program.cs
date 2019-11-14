using System;

using Model;
using System.Drawing;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] tab = new string[3, 3]{
                {"1", "2", "3" },
                {"4", "5", "6" },
                {"7", "8", "9" }
            };
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    Console.WriteLine($"X: {i}, Y: {j}, value: {tab[i, j]}");
                }
            }
        }
    }
}
