// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarbezDotEu.Sodoku.Generator;

namespace BarbezDotEu.Sodoku.Cli
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            Program program = new();
            Run();
        }

        private static void Run()
        {
            Parallel.For(default, 10, i =>
            {
                var field = new SodokuGenerator().Generate();
                DisplayField(field);
                Console.WriteLine();
            });
        }

        private static void DisplayField(IEnumerable<byte[]> field)
        {
            foreach (var row in field)
            {
                Console.WriteLine(string.Join(',', row));
            }
        }
    }
}
