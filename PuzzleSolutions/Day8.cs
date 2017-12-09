using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day8 : IAocPuzzle
    {
        private Dictionary<string, int> _curReg;

        public string Solve(string input, AocPuzzlePart part)
        {
            int bigestEverVal = 0;
            _curReg = CreateRegister(input);

            var lines = input.Split(System.Environment.NewLine);
            foreach(var line in lines)
            {
                ProcessLine(line);
                if (_curReg.Max(x => x.Value) > bigestEverVal)
                {
                    bigestEverVal = _curReg.Max(x => x.Value);
                }
            }

            if (part == AocPuzzlePart.Part1)
                return _curReg.Max(x => x.Value).ToString();
            else
                return bigestEverVal.ToString();
        }

        private void ProcessLine(string inputLine)
        {
            var ifs = inputLine.Split("if");

            if (RunLogic(ifs[1]))
            {
                ProcessCommand(ifs[0]);
            }
        }

        private void ProcessCommand(string command)
        {
            if(command.Contains("inc"))
            {
                var vals = command.Split("inc");
                _curReg[vals[0].Trim()] += Convert.ToInt32(vals[1].Trim());
            } 
            else
            {
                var vals = command.Split("dec");
                _curReg[vals[0].Trim()] -= Convert.ToInt32(vals[1].Trim());
            }
        }

        private bool RunLogic(string logic)
        {
            var parts = logic.Trim().Split(" ");

            switch(parts[1])
            {
                case "==":
                    return _curReg[parts[0]] == Convert.ToInt32(parts[2]);
                case "!=":
                    return _curReg[parts[0]] != Convert.ToInt32(parts[2]);
                case "<=":
                    return _curReg[parts[0]] <= Convert.ToInt32(parts[2]);
                case ">=":
                    return _curReg[parts[0]] >= Convert.ToInt32(parts[2]);
                case "<":
                    return _curReg[parts[0]] < Convert.ToInt32(parts[2]);
                case ">":
                    return _curReg[parts[0]] > Convert.ToInt32(parts[2]);
                default:
                    throw new Exception($"Unsupported command: {parts[1]}");
            }
        }

        private Dictionary<string, int> CreateRegister(string input)
        {
            var reg = new Dictionary<string, int>();

            var lines = input.Split(System.Environment.NewLine);

            foreach (var line in lines)
            {
                var key = line.Split(' ')[0].Trim();

                if (!reg.ContainsKey(key))
                {
                    reg.Add(key, 0);
                }
            }

            return reg;
        }
    }
}