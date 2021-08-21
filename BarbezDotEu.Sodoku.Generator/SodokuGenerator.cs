// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Linq;
using BarbezDotEu.Generic;

namespace BarbezDotEu.Sodoku.Generator
{
    public class SodokuGenerator
    {
        private static readonly byte[] possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private static readonly int items = possibilities.Length;
        private readonly List<byte[]> field = new();
        private const int LocalFieldsPerRow = 3;

        public IEnumerable<byte[]> Generate()
        {
            this.field.Clear();
            for (int i = default; i < items; i++)
            {
                bool noGoodRowFound = true;
                var row = Array.Empty<byte>();
                while (noGoodRowFound)
                {
                    row = possibilities.PickRandom(items).ToArray();
                    if (!field.Any() || (DoesntAffectHorizontally(row) && DoesntAffectLocalField(row)))
                    {
                        noGoodRowFound = false;
                    }
                }

                field.Add(row);
            }

            return field;
        }

        private bool DoesntAffectHorizontally(byte[] candidateRow)
        {
            for (int i = 0; i < items; i++)
            {
                foreach (var existingRow in field)
                {
                    if (candidateRow[i] == existingRow[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool DoesntAffectLocalField(byte[] candidateRow)
        {
            var currentFieldRows = this.field.Count;
            if (currentFieldRows == 0)
            {
                return true;
            }

            var maxRowsBack = 2;
            var rowsToCheck = currentFieldRows < maxRowsBack ? currentFieldRows : maxRowsBack;
            List<List<byte>> localFields = new();
            for (int i = default; i < LocalFieldsPerRow; i++)
            {
                List<byte> localField = new();
                for (int j = currentFieldRows - rowsToCheck; j < currentFieldRows; j++)
                {
                    localField.AddRange(this.field[j].Skip(i * LocalFieldsPerRow).Take(LocalFieldsPerRow));
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
