using NUnit.Framework;
using Scrabble2018;
using Scrabble2018.Model;
using System;
using System.Windows.Media;

namespace UnitTests
{
    public class BoardTileTest
    {
        BoardTiles bt;
        [SetUp]
        public void Setup()
        {
            bt = new BoardTiles();
        }

        [Test]
        public void BoardTile_WordMultiplier_WordTriple_Should_Return_3()
        {
            // Arrange

            // Act
            var result = bt.WordMultiplier(0, 0);

            // Reset

            // Assert
            Assert.AreEqual(3, result);
        }

        [Test]
        public void BoardTile_WordMultiplier_WordDouble_Should_Return_2()
        {
            // Arrange

            // Act
            var result = bt.WordMultiplier(1, 1);

            // Reset

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void BoardTile_WordMultiplier_Default_Square_Should_Return_1()
        {
            // Arrange

            // Act
            var result = bt.WordMultiplier(1, 0);

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void BoardTile_LetterMultiplier_TripleLetter_Should_Return_3()
        {
            // Arrange

            // Act
            var result = bt.LetterMultiplier(1, 5);

            // Reset

            // Assert
            Assert.AreEqual(3, result);
        }

        [Test]
        public void BoardTile_LetterMultiplier_DoubleLetter_Should_Return_2()
        {
            // Arrange

            // Act
            var result = bt.LetterMultiplier(0, 3);

            // Reset

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void BoardTile_LetterMultiplier_Default_Square_Should_Return_1()
        {
            // Arrange

            // Act
            var result = bt.LetterMultiplier(0, 1);

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        /*
         * This is an interesting unit test. The method that is being tested
         * (CleanVisited) is void, so we can't verify anything except the 
         * fact that we made it through the method successfully. So, the
         * goal of this unit test is exactly that: If we get through this unit
         * test and it passes, then we know we made it through that method
         * successfully.
         */
        [Test]
        public void BoardTile_CleanVisited_Should_Pass()
        {
            // Arrange

            // Act
            bt.CleanVisited();

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void BoardTile_ApplyVisited_Should_Pass()
        {
            // Arrange

            // Act
            bt.ApplyVisited();

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void BoardTile_ApplyVisited_Has_Visited_Should_Pass()
        {
            // Arrange

            // Act
            bt.LetterMultiplier(0, 7);
            bt.LetterMultiplier(1, 9);
            bt.ApplyVisited();

            // Reset
            bt.CleanVisited();

            // Assert
            Assert.IsTrue(true);
        }


        [Test]
        public void BoardTile_DetermineColor_Should_Return_Correct_Colors()
        {
            // Assert
            Assert.AreEqual(Brushes.OrangeRed, BoardTiles.DetermineColor(0, 0)); // Word Triple
            Assert.AreEqual(Brushes.Coral, BoardTiles.DetermineColor(1, 1)); // Word Double
            Assert.AreEqual(Brushes.LightSkyBlue, BoardTiles.DetermineColor(0, 3)); // Letter Double
            Assert.AreEqual(Brushes.MediumBlue, BoardTiles.DetermineColor(1, 5)); // Letter Triple
            Assert.AreEqual(Brushes.Gold, BoardTiles.DetermineColor(7, 7)); // Start
            Assert.AreEqual(Brushes.Bisque, BoardTiles.DetermineColor(1, 0)); // Default           
        }
    }
}