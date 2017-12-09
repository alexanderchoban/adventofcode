using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day4 : IAocPuzzle
    {
        public string Solve(string input, AocPuzzlePart part)
        {
            if (part == AocPuzzlePart.Part1)
            {
                return input.Split(System.Environment.NewLine).Select(x => IsValidPassphrase(x, part)).Where(y => y).Count().ToString();
            }
            else
            {
                return input.Split(System.Environment.NewLine).Select(x => IsValidPassphrase(x, part)).Where(y => y).Count().ToString();
            }
        }

        private bool IsValidPassphrase(string passphrase, AocPuzzlePart part)
        {
            List<string> phraseList;

            if (part == AocPuzzlePart.Part1)
            {
                phraseList = passphrase.Split(' ').ToList();
            }
            else
            {
                phraseList = passphrase.Split(' ').Select(x => String.Concat(x.OrderBy(c => c))).ToList();
            }

            var duplicates = phraseList.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();

            return duplicates.Count == 0;
        }
    }
}