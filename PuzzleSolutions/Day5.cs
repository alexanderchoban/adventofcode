using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day5 : IAocPuzzle
    {
        public string Solve(string input, AocPuzzlePart part)
        {
            var maze = input.Split(System.Environment.NewLine).Select(x => Convert.ToInt32(x.ToString())).ToArray();

            bool escaped = false;
            int curLoc = 0;
            int step = 1;

            while (!escaped)
            {
                int lastLoc = curLoc;
                curLoc += maze[curLoc];

                if (part == AocPuzzlePart.Part2 && maze[lastLoc] >= 3)
                {
                    // after each jump, if the offset was three or more, instead decrease it by 1. Otherwise, increase it by 1 as before
                    maze[lastLoc]--;
                }
                else
                {
                    maze[lastLoc]++;
                }

                if (curLoc >= maze.Count())
                {
                    escaped = true;
                }
                else
                {
                    step++;
                }
            }

            return step.ToString();
        }
    }
}