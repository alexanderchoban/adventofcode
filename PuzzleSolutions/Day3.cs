using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day3 : IAocPuzzle
    {
        public string Solve(string input, AocPuzzlePart part)
        {
            int intNumber = Convert.ToInt32(input);
            if (part == AocPuzzlePart.Part1)
            {
                return SolvePart1(intNumber).ToString();
            }
            else
            {
                return SolvePart2(intNumber).ToString();
            }
        }

        private int SolvePart1(int intNumber)
        {
            if (intNumber == 1) return 0;

            // use doubles so we do get int math
            double number = Convert.ToDouble(intNumber);
            double level = Findlevel(number);
            double sideLength = 2 * level - 1;
            double levelMax = Math.Pow(sideLength, 2);

            // 0 = bottom, 1 = left, 2 == top, 3 = right
            double side = GetSide(number, levelMax, sideLength);


            double middleOfSide = levelMax - ((sideLength - 1) * side) - (sideLength - 1) / 2;

            double dist = Math.Abs(number - middleOfSide) + level - 1;
            return Convert.ToInt32(dist);
        }

        private int SolvePart2(int intNumber)
        {
            Dictionary<string, int> grid = new Dictionary<string, int>();
            grid[c(0, 0)] = 1;
            int x = 1;
            int y = 0;

            for (int level = 2; true; level++)
            {
                double sideLength = 2 * level - 1;

                // first row
                for (int i = 0; i < sideLength - 2; i++)
                {
                    grid[c(x, y)] = CalcPartTwoValue(grid, x, y);
                    if (grid[c(x, y)] > intNumber) return grid[c(x, y)];
                    y++;
                }

                // second row
                for (int i = 0; i < sideLength - 1; i++)
                {
                    grid[c(x, y)] = CalcPartTwoValue(grid, x, y);
                    if (grid[c(x, y)] > intNumber) return grid[c(x, y)];
                    x--;
                }

                // third row
                for (int i = 0; i < sideLength - 1; i++)
                {
                    grid[c(x, y)] = CalcPartTwoValue(grid, x, y);
                    if (grid[c(x, y)] > intNumber) return grid[c(x, y)];
                    y--;
                }

                // fourth row
                for (int i = 0; i < sideLength; i++)
                {
                    grid[c(x, y)] = CalcPartTwoValue(grid, x, y);
                    if (grid[c(x, y)] > intNumber) return grid[c(x, y)];
                    x++;
                }
            }
        }

        private static int CalcPartTwoValue(Dictionary<string, int> grid, int x, int y)
        {
            int sum = 0;

            if (grid.ContainsKey(c(x + 1, y))) sum += grid[c(x + 1, y)];
            if (grid.ContainsKey(c(x + 1, y - 1))) sum += grid[c(x + 1, y - 1)];
            if (grid.ContainsKey(c(x + 1, y + 1))) sum += grid[c(x + 1, y + 1)];
            if (grid.ContainsKey(c(x - 1, y))) sum += grid[c(x - 1, y)];
            if (grid.ContainsKey(c(x - 1, y + 1))) sum += grid[c(x - 1, y + 1)];
            if (grid.ContainsKey(c(x - 1, y - 1))) sum += grid[c(x - 1, y - 1)];
            if (grid.ContainsKey(c(x, y - 1))) sum += grid[c(x, y - 1)];
            if (grid.ContainsKey(c(x, y + 1))) sum += grid[c(x, y + 1)];

            return sum;
        }

        private static string c(int x, int y)
        {
            return x + "," + y;
        }

        private static double GetSide(double number, double levelMax, double sideLength)
        {
            // 0 = bottom, 1 = left, 2 == top, 3 = right
            sideLength--;
            if (number > levelMax - sideLength)
            {
                return 0.0;
            }
            else if (number > levelMax - sideLength * 2)
            {
                return 1.0;
            }
            else if (number > levelMax - sideLength * 3)
            {
                return 2.0;
            }
            else if (number > levelMax - sideLength * 4)
            {
                return 3.0;
            }

            throw new Exception("Bad side");
        }

        private static double Findlevel(double number)
        {

            double level = 1;

            while (Math.Pow(2 * level - 1, 2) < number)
                level++;

            return level;
        }
    }
}