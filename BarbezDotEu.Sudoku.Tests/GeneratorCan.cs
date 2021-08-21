using System.Linq;
using BarbezDotEu.Sudoku.Generator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbezDotEu.Sudoku.Tests
{
    [TestClass]
    public class GeneratorCan
    {
        [TestMethod]
        public void CreateASudoku()
        {
            // Arrange + act
             var SudokuGame = new SudokuGame();

            // Assert something's there
            Assert.IsNotNull(SudokuGame?.Solution);

            // Assert there are 9 rows in a game
            Assert.AreEqual(9, SudokuGame.Solution.Count());

            // Asert there are 9 columns in a game
            Assert.AreEqual(9, SudokuGame.Solution.First().Length);

        }
    }
}
