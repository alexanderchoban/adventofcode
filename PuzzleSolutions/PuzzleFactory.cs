using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class PuzzleFactory
    {
        public static IAocPuzzle GetPuzzleForDay(int day)
        {
            if (day > 25 || day < 1)
            {
                throw new ArgumentOutOfRangeException("Day must be 1-25 for advent.");
            }

            switch(day)
            {
                case 1:
                    return new Day1();
                case 2:
                    return new Day2();
                case 3:
                    return new Day3();
                case 4:
                    return new Day4();
                case 5:
                    return new Day5();
                case 6:
                    return new Day6();
                case 7:
                    return new Day7();
                case 8:
                    return new Day8();
                default:
                    throw new Exception("Day not supported yet.");
            }

        }
    }
}