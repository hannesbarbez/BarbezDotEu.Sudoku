// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Linq;
using BarbezDotEu.Generic;

namespace BarbezDotEu.Sudoku.Generator
{
    /// <summary>
    /// Generates Sudoku solutions.
    /// </summary>
    public class SudokuSolutionGenerator
    {
        private static readonly byte[] possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private static readonly int items = possibilities.Length;
        private readonly List<byte[]> playingField = new List<byte[]>();
        private const int LocalFieldsPerRow = 3;

        /// <summary>
        /// Generates and returns a random Sudoku solution.
        /// </summary>
        /// <returns>A solved Sudoku.</returns>
        public IEnumerable<byte[]> Generate()
        {
            // Note: no attempt has been made (yet) to optimize this algorithm.
            this.playingField.Clear();
            for (int i = default; i < items; i++)
            {
                bool noGoodRowFound = true;
                var row = Array.Empty<byte>();
                while (noGoodRowFound)
                {
                    row = possibilities.PickRandom(items).ToArray();
                    if (!playingField.Any() || (DoesNotViolateVerticalUniqueness(row) && DoesNotViolateLocalPlayingFieldUniqueness(row)))
                    {
                        noGoodRowFound = false;
                    }
                }

                playingField.Add(row);
            }

            return playingField;
        }

        /// <summary>
        /// Checks if a candidate row can be added to the current playing field, by checking the existing rows at all row indexes for occurences of the values inside the candidate row.
        /// If no duplicates are present, the candidate row is deemed not to violate the vertical uniqueness rule of the game.
        /// </summary>
        /// <param name="candidateRow">The row to validate for fitness given the existing playing field.</param>
        /// <returns>True if the candidate row does not violate the vertical uniqueness rule of the game. False, otherwise.</returns>
        private bool DoesNotViolateVerticalUniqueness(byte[] candidateRow)
        {
            for (int i = 0; i < items; i++)
            {
                foreach (var existingRow in playingField)
                {
                    if (candidateRow[i] == existingRow[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if a candidate row can be added to the current playing field, by verifying whether a candidate row would result in unique values inside all 9x9 local playing fields.
        /// If no duplicates are present, the candidate row is deemed not to violate the 9x9 local playing field uniqueness rule of the Sudoku game.
        /// </summary>
        /// <param name="candidateRow">The row to validate for fitness given the existing playing field.</param>
        /// <returns>True if the candidate row does not violate the 9x9 local playing field uniqueness rule of the Sudoku game. False, otherwise.</returns>
        private bool DoesNotViolateLocalPlayingFieldUniqueness(byte[] candidateRow)
        {
            var currentFieldRows = this.playingField.Count;
            if (currentFieldRows == 0)
            {
                return true;
            }

            var maxRowsLookback = 2;
            var rowsToCheck = currentFieldRows < maxRowsLookback ? currentFieldRows : maxRowsLookback;
            var localFields = new List<List<byte>>();
            for (int i = default; i < LocalFieldsPerRow; i++)
            {
                var localField = new List<byte>();
                for (int j = currentFieldRows - rowsToCheck; j < currentFieldRows; j++)
                {
                    localField.AddRange(this.playingField[j].Skip(i * LocalFieldsPerRow).Take(LocalFieldsPerRow));
                }

                localFields.Add(localField);
            }

            for (int i = default; i < localFields.Count; i++)
            {
                var numbersThatShouldntOccur = candidateRow.Skip(i * LocalFieldsPerRow).Take(LocalFieldsPerRow);
                var duplicates = localFields[i].Intersect(numbersThatShouldntOccur);
                if (duplicates.Any())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
