// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;

namespace BarbezDotEu.Sodoku.Generator
{
    /// <summary>
    /// Implements a game of sodoku, including its solution.
    /// </summary>
    public class SodokuGame
    {
        /// <summary>
        /// Constructs a <see cref="SodokuGame"/>.
        /// </summary>
        public SodokuGame()
        {
            this.Solution = new SodokuSolutionGenerator().Generate();
        }

        /// <summary>
        /// Gets the complete solution of this game.
        /// </summary>
        public IEnumerable<byte[]> Solution { get; }
    }
}
