// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarbezDotEu.Sudoku.Generator;

namespace BarbezDotEu.Sudoku.Cli
{
    /// <summary>
    /// This class demonstrates the BarbezDotEu.Sudoku.Generator project.
    /// </summary>
    class Program
    {
        private static void Main()
        {
            var games = new HashSet<SudokuGame>();
            Parallel.For(default, 5, i =>
            {
                var game = new SudokuGame();
                games.Add(game);
            });

            Show(games);
        }

        private static void Show(IEnumerable<SudokuGame> games)
        {
            foreach (var game in games)
            {
                foreach (var row in game.Solution)
                {
                    Console.WriteLine(string.Join(',', row));
                }

                Console.WriteLine();
            }
        }
    }
}
