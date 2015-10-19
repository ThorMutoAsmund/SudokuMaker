using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuMaker
{
    public class Sudoku
    {
        private int[] Orig { get; set; }
        private int[] Numbers { get; set; }
        private Int32 FieldsOk { get; set; }

        public Sudoku()
        {
            this.Numbers = new int[9 * 9];
            this.Orig = new int[9 * 9];
            for (int i = 0; i < 9 * 9; ++i)
            {
                this.Numbers[i] = 0;
            }
            this.FieldsOk = 0;
        }

        public bool IsOk
        {
            get
            {
                return this.FieldsOk == 0;
            }
        }

        private void UpdateColumn(int x)
        {
            int r = 0;
            for (int y = 0; y < 9; ++y)
            {
                var a = this.Numbers[y * 9 + x];
                if (a > 0)
                {
                    a = 1 << (a - 1);
                    if ((r & a) != 0)
                    {
                        this.FieldsOk |= (1 << x);
                        return;
                    }
                    r |= a;
                }
            }
            this.FieldsOk &= ~(1 << x);
        }

        private void UpdateRow(int y)
        {
            int r = 0;
            for (int x = 0; x < 9; ++x)
            {
                var a = this.Numbers[y * 9 + x];
                if (a > 0)
                {
                    a = 1 << (a - 1);
                    if ((r & a) != 0)
                    {
                        this.FieldsOk |= (1 << (y+9));
                        return;
                    }
                    r |= a;
                }
            }
            this.FieldsOk &= ~(1 << (y+9));
        }

        private void UpdateSquare(int i)
        {
            int r = 0;
            for (int y = 3*(i / 3); y < 3 * (i / 3) + 3; ++y)
            {
                for (int x = 3*(i % 3); x < 3 * (i % 3) + 3; ++x)
                {
                    var a = this.Numbers[y * 9 + x];
                    if (a > 0)
                    {
                        a = 1 << (a - 1);
                        if ((r & a) != 0)
                        {
                            this.FieldsOk |= (1 << (i + 18));
                            return;
                        }
                        r |= a;
                    }
                }
            }
            this.FieldsOk &= ~(1 << (i + 18));
        }

        private void Set(int x, int y, int value)
        {
            this.Numbers[y * 9 + x] = value;

            this.UpdateColumn(x);
            this.UpdateRow(y);
            this.UpdateSquare(3 * (y / 3) + (x / 3));
        }

        public void SetValue(int c, int value)
        {
            Set(c % 9, c / 9, value);
            this.Orig[c] = value;
        }

        public bool NextValue(int c)
        {
            var nextValue = (this.Numbers[c] % 9) + 1;
            if (nextValue == this.Orig[c])
            {
                return false;
            }
            Set(c % 9, c / 9, nextValue);

            return true;
        }

        public void ClearValue(int c)
        {
            Set(c % 9, c / 9, 0);
        }

        public void Print()
        {
            for (int y = 0; y < 9; ++y)
            {
                for (int x = 0; x < 9; ++x)
                {
                    Console.Write(this.Numbers[y * 9 + x]);
                    if (x % 3 == 2 && x != 8)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
                if (y % 3 == 2 && y != 8)
                {
                    Console.WriteLine();
                }
            }
        }

        public override string ToString()
        {
            string result = "";
            for (int y = 0; y < 9; ++y)
            {
                for (int x = 0; x < 9; ++x)
                {
                    result += this.Numbers[y * 9 + x];
                    if (x % 3 == 2 && x != 8)
                    {
                        result += " ";
                    }
                }
                result += "//";
                if (y % 3 == 2 && y != 8)
                {
                    result += "//";
                }
            }

            return result;
        }
    }
}
