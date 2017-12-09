using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public interface IAocPuzzle
    {
        string Solve(string input, AocPuzzlePart part);
    }

    public enum AocPuzzlePart
    {
        Part1 = 1,
        Part2 = 2
    }
}