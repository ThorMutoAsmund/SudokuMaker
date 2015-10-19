using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating sudokus. Press 'Q' to exit");
            var engine = new Engine();
            do
            {
                var sudoku = engine.Generate();
                sudoku.Print();
                Console.WriteLine();
                Console.WriteLine("-----------");

                string c = Console.ReadLine();
                if (c == "q" || c == "Q")
                {
                    break;
                }
            }
            while (true);
        }
    }
}
