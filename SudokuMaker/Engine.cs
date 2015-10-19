using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuMaker
{
    public class Engine
    {
        private Random Rand { get; set; }

        public Engine(int seed = -1)
        {
            if (seed == -1)
            {
                seed = DateTime.Now.Millisecond;
            }
            this.Rand = new Random(seed);
        }

        public Sudoku Generate()
        {
            var sudoku = new Sudoku();

            int c = 0;
            var s = this.Rand.Next(1, 10);
            sudoku.SetValue(c, s);

            while (c < 9 * 9)
            {
                var goBack = false;
                while (!sudoku.IsOk)
                {
                    goBack = !sudoku.NextValue(c);
                    if (goBack)
                    {
                        break;
                    }
                }

                if (goBack)
                {
                    while (goBack)
                    {
                        sudoku.ClearValue(c);
                        c--;
                        goBack = !sudoku.NextValue(c);
                    }
                    continue;
                }

                c++;
                if (c < 9 * 9)
                {
                    s = this.Rand.Next(1, 10);
                    sudoku.SetValue(c, s);
                }
            }


            return sudoku;
        }
    }
}
