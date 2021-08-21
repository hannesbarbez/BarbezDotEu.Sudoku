// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using BarbezDotEu.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarbezDotEu.Sodoku.Cli
{
    internal class Program
    {
        private static int items;
        private readonly List<byte[]> field = new();
        private static readonly byte[] possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        private static void Main(string[] args)
        {
            items = possibilities.Length;
            Program program = new();
            program.Run();
        }

        private void Run()
        {
            this.GenerateField();
            this.DisplayField();
        }

        private void DisplayField()
        {
            foreach (var row in field)
            {
                Console.WriteLine(string.Join(',', row));
            }
        }

        private void GenerateField()
        {
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
            var localFieldsPerRow = 3;
            List<List<byte>> localFields = new();
            for (int i = default; i < localFieldsPerRow; i++)
            {
                List<byte> localField = new();
                for (int j = currentFieldRows - rowsToCheck; j < currentFieldRows; j++)
                {
                    localField.AddRange(this.field[j].Skip(i * localFieldsPerRow).Take(localFieldsPerRow));
                }

                localFields.Add(localField);
            }

            for (int i = default; i < localFields.Count; i++)
            {
                var numbersThatShouldntOccur = candidateRow.Skip(i * localFieldsPerRow).Take(localFieldsPerRow);
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
