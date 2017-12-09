using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdventOfCode.Models;

namespace AdventOfCode.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new PuzzleInputViewModel());
        }

        [HttpPost]
        public IActionResult Index(PuzzleInputViewModel puzzleViewModel)
        {
            if (ModelState.IsValid)
            {
                var puzzle = PuzzleFactory.GetPuzzleForDay(puzzleViewModel.Day);
                puzzleViewModel.Answer = puzzle.Solve(puzzleViewModel.Input, puzzleViewModel.Part);
                ModelState.Clear();
            }

            return View(puzzleViewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
