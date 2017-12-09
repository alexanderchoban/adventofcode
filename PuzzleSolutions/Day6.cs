using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day6 : IAocPuzzle
    {
        public string Solve(string input, AocPuzzlePart part)
        {
            var curList = input.Split("\t").Select(x => Convert.ToInt32(x)).ToList();
            IList<List<int>> seenDataList = new List<List<int>>();
            int totalRedists = 0;
            int haveSeenCount = 0;

            while ((part == AocPuzzlePart.Part1 && haveSeenCount < 1) || (part == AocPuzzlePart.Part2 && haveSeenCount < 2))
            {
                totalRedists++;
                seenDataList.Add(curList.Select(x => x).ToList());
                var redistributeVal = curList.Max();
                var index = curList.FindIndex(a => a == redistributeVal);
                curList[index] = 0;

                while (redistributeVal > 0)
                {
                    index++;
                    if (index == curList.Count)
                    {
                        index = 0;
                    }

                    curList[index]++;
                    redistributeVal--;
                }

                for (int i = 0; i < seenDataList.Count; i++)
                {
                    if (AreListsTheSame(seenDataList[i], curList))
                    {
                        // part two needs a second loop
                        if (part == AocPuzzlePart.Part2 && haveSeenCount == 0)
                        {
                            totalRedists = 0;
                            seenDataList.Clear();
                        }

                        haveSeenCount++;
                        break;
                    }
                }
            }

            return totalRedists.ToString();
        }

        private bool AreListsTheSame(List<int> a, List<int> b)
        {
            if (a.Count != b.Count)
            {
                return false;
            }

            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}