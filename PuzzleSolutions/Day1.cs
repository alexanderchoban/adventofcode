using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day1 : IAocPuzzle
    {
        public string Solve(string input, AocPuzzlePart part)
        {
            List<int> codeList = input.ToCharArray().ToList().Select(x => Convert.ToInt32(x.ToString())).ToList();
            int compare = 1;

            if (part == AocPuzzlePart.Part2)
            {
                compare = codeList.Count() / 2;
            }

            int sum = 0;
            for (int i = 0; i < codeList.Count(); i++)
            {
                int half = (i - compare);

                if (half < 0)
                    half += codeList.Count();

                if (codeList[i] == codeList[half])
                {
                    sum += codeList[i];
                }
            }

            return sum.ToString();
        }
    }
}
