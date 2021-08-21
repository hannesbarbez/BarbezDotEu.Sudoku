// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;

namespace BarbezDotEu.Sudoku.Generator
{
    /// <summary>
    /// Implements a game of Sudoku, including its solution.
    /// </summary>
    public class SudokuGame
    {
        /// <summary>
        /// Constructs a <see cref="SudokuGame"/>.
        /// </summary>
        public SudokuGame()
        {
            this.Solution = new SudokuSolutionGenerator().Generate();
        }

        /// <summary>
        /// Gets the complete solution of this game.
        /// </summary>
        public IEnumerable<byte[]> Solution { get; }
    }
}
