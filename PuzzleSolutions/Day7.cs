using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day7 : IAocPuzzle
    {
        public string Solve(string input, AocPuzzlePart part)
        {
            var programs = ParseInput(input);
            var rootNode = FindRootNode(programs);

            if (part == AocPuzzlePart.Part1)
            {
                return rootNode.Name;
            }
            else
            {
                return FindFirstBadWeight(programs).ToString();
            }
        }

        private Day7Prog FindRootNode(List<Day7Prog> progs)
        {
            if (progs.Count < 1)
            {
                throw new ArgumentException("No programs in list");
            }

            Day7Prog curNode = progs[0];
            bool hasParents = true;

            do
            {
                Day7Prog parent = GetParent(curNode.Name, progs);

                if (parent.Name != null && parent.Name != string.Empty)
                {
                    curNode = parent;
                }
                else
                {
                    hasParents = false;
                }
            }
            while (hasParents);


            return curNode;
        }

        private int FindFirstBadWeight(List<Day7Prog> progs)
        {
            Dictionary<string, int> totalWeights = new Dictionary<string, int>();
            var currentLevel = progs.Where(x => x.Children.Count == 0).ToList();

            while (currentLevel.Count > 0)
            {
                List<Day7Prog> newLevel = new List<Day7Prog>();
                foreach (var prog in currentLevel)
                {
                    // skip if all the children are not done
                    if (totalWeights.Where(x => prog.Children.Contains(x.Key)).Count() == prog.Children.Count() && !totalWeights.ContainsKey(prog.Name))
                    {
                        if (prog.Children.Count == 0)
                        {
                            totalWeights.Add(prog.Name, prog.Weight);
                        }
                        else
                        {
                            // check children
                            var childrenWeights = totalWeights.Where(x => prog.Children.Contains(x.Key));

                            if (childrenWeights.Select(y => y.Value).Distinct().Count() > 1)
                            {
                                var uniqueWeights = childrenWeights.GroupBy(x => x.Value)
                                    .Select(group => new
                                    {
                                        weight = group.Key,
                                        Count = group.Count()
                                    })
                                    .OrderBy(x => x.Count);

                                if (uniqueWeights.Count() > 2)
                                {
                                    throw new Exception("Can't balance when more than one is wrong.");
                                }

                                var weightChange = uniqueWeights.Last().weight - uniqueWeights.First().weight;
                                var childToFix = childrenWeights.FirstOrDefault(x => x.Value == uniqueWeights.First().weight).Key;

                                return progs.First(x => x.Name == childToFix).Weight + weightChange;
                            }

                            // calc this total weight
                            totalWeights.Add(prog.Name, totalWeights.Where(x => prog.Children.Contains(x.Key)).Select(y => y.Value).Sum() + prog.Weight);
                        }

                        var parent = GetParent(prog.Name, progs);
                        if (!totalWeights.ContainsKey(parent.Name) && !newLevel.Select(x => x.Name).ToList().Contains(parent.Name))
                        {
                            newLevel.Add(parent);
                        }
                    }
                }
                currentLevel = newLevel;
            }

            throw new Exception("Tree is balanced");
        }

        private Day7Prog GetParent(string progName, List<Day7Prog> progs)
        {
            return progs.FirstOrDefault(x => x.Children.Contains(progName));
        }

        private List<Day7Prog> ParseInput(string input)
        {
            List<Day7Prog> progs = new List<Day7Prog>();
            var lines = input.Split(System.Environment.NewLine);

            foreach (var line in lines)
            {
                if (line.Trim().Length > 0)
                {
                    Day7Prog prog = new Day7Prog();
                    prog.Name = line.Substring(0, line.IndexOf(' ')).Trim();
                    prog.Weight = Convert.ToInt32(line.Substring(line.IndexOf('(') + 1, line.IndexOf(')') - line.IndexOf('(') - 1));

                    if (line.Contains("->"))
                    {
                        prog.Children = line.Split("->")[1].Split(",").Select(x => x.Trim()).ToList();
                    }
                    else
                    {
                        prog.Children = new List<string>();
                    }

                    progs.Add(prog);
                }
            }

            return progs;
        }
    }

    struct Day7Prog
    {
        public string Name;
        public int Weight;
        public List<string> Children;
    }
}