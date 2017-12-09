using System;
using System.ComponentModel.DataAnnotations;

namespace AdventOfCode.Models
{
    public class PuzzleInputViewModel
    {
        [Range(1, 25)]
        public int Day { get; set; } = 1;

        public AocPuzzlePart Part { get; set; } = AocPuzzlePart.Part1;

        public string Input { get; set; } = "";

        public string Answer { get; set; } = "";
    }
}