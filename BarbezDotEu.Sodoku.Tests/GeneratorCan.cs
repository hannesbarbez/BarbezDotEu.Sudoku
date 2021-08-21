using System.Linq;
using BarbezDotEu.Sodoku.Generator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbezDotEu.Sodoku.Tests
{
    [TestClass]
    public class GeneratorCan
    {
        [TestMethod]
        public void CreateASodoku()
        {
            // Arrange + act
             var sodokuGame = new SodokuGame();

            // Assert something's there
            Assert.IsNotNull(sodokuGame?.Solution);

            // Assert there are 9 rows in a game
            Assert.AreEqual(9, sodokuGame.Solution.Count());

            // Asert there are 9 columns in a game
            Assert.AreEqual(9, sodokuGame.Solution.First().Length);

        }
    }
}
