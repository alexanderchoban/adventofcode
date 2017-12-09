using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day2 : IAocPuzzle
    {
        public string Solve(string input, AocPuzzlePart part)
        {
            var spreadsheet = input.Split(System.Environment.NewLine)
                .Select(x => x.Split("\t")
                    .Select(y => Convert.ToInt32(y))
                    .ToArray())
                .ToArray();

            int checksum = 0;
            for (int i = 0; i < spreadsheet.Count(); i++)
            {
                if (part == AocPuzzlePart.Part1) // Sum
                {
                    int max = spreadsheet[i][0];
                    int min = spreadsheet[i][0];

                    for (int j = 1; j < spreadsheet[i].Count(); j++)
                    {
                        if (spreadsheet[i][j] > max)
                        {
                            max = spreadsheet[i][j];
                        }
                        else if (spreadsheet[i][j] < min)
                        {
                            min = spreadsheet[i][j];
                        }
                    }

                    checksum += max - min;
                }
                else // Part 2
                {
                    bool found = false;
                    for (int j = 0; j < spreadsheet[i].Count() && !found; j++)
                    {
                        for (int h = 0; h < spreadsheet[i].Count() && !found; h++)
                        {
                            if (j != h) // dont compare the same index
                            {
                                if (spreadsheet[i][j] % spreadsheet[i][h] == 0)
                                {

                                    checksum += spreadsheet[i][j] / spreadsheet[i][h];
                                    found = true;
                                }
                            }
                        }
                    }
                }
            }

            return checksum.ToString();
        }

        public static int[][] ConvertStringTospreadsheet(string spreadsheet)
        {
            return spreadsheet
                    .Split(System.Environment.NewLine)
                    .Select(x => x.Split('\t')
                        .Select(y => Convert.ToInt32(y))
                        .ToArray())
                    .ToArray();
        }

    }
}