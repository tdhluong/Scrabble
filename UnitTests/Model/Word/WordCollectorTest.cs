using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Scrabble2018.Model;
using Scrabble2018.Model.Word;

namespace UnitTests.Model.Word
{
    [TestFixture]
    public class WordCollectorTest
    {
        [Test]
        public static void Locate_ReturnOne_GivenValidWordNotAppeared()
        {
            // Arrange
            string word = "TEST";
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { word });

            // Act
            int result = WordCollector.Locate(word);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public static void Locate_ReturnNegativeOne_GivenInvalidWord()
        {
            // Arrange
            string word = "NONEXISTENT";
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "EXISTENT" });

            // Act
            int result = WordCollector.Locate(word);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [Test]
        public static void VCollect_ReturnPositiveScore_GivenValidVerticalWord()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string>(),
                WordsAppearedInValidation = new List<string>(),
                CorrectWords = new Dictionary<string, int>(),
                BoardChar = new char[15, 15]
            };
            gs.BoardChar[0, 0] = 'T';
            gs.BoardChar[1, 0] = 'E';
            gs.BoardChar[2, 0] = 'S';
            gs.BoardChar[3, 0] = 'T';
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "TEST" });

            // Act
            int result = WordCollector.VCollect(0, 0, gs.BoardChar, gs);

            // Assert
            Assert.IsTrue(result > 0);
        }

        [Test]
        public static void VCollect_ReturnNegativeOne_GivenInvalidVerticalWord()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string>(),
                WordsAppearedInValidation = new List<string>(),
                CorrectWords = new Dictionary<string, int>(),
                BoardChar = new char[15, 15]
            };
            gs.BoardChar[0, 0] = 'T';
            gs.BoardChar[1, 0] = 'E';
            gs.BoardChar[2, 0] = 'S';
            gs.BoardChar[3, 0] = 'X'; // Invalid word
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "TEST" });

            // Act
            int result = WordCollector.VCollect(0, 0, gs.BoardChar, gs);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [Test]
        public static void HCollect_ReturnPositiveScore_GivenValidHorizontalWord()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string>(),
                WordsAppearedInValidation = new List<string>(),
                CorrectWords = new Dictionary<string, int>(),
                BoardChar = new char[15, 15]
            };
            gs.BoardChar[0, 0] = 'T';
            gs.BoardChar[0, 1] = 'E';
            gs.BoardChar[0, 2] = 'S';
            gs.BoardChar[0, 3] = 'T';
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "TEST" });

            // Act
            int result = WordCollector.HCollect(0, 0, gs.BoardChar, gs);

            // Assert
            Assert.IsTrue(result > 0);
        }

        [Test]
        public static void HCollect_ReturnNegativeOne_GivenInvalidHorizontalWord()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string>(),
                WordsAppearedInValidation = new List<string>(),
                CorrectWords = new Dictionary<string, int>(),
                BoardChar = new char[15, 15]
            };
            gs.BoardChar[0, 0] = 'T';
            gs.BoardChar[0, 1] = 'E';
            gs.BoardChar[0, 2] = 'S';
            gs.BoardChar[0, 3] = 'X'; // Invalid word
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "TEST" });

            // Act
            int result = WordCollector.HCollect(0, 0, gs.BoardChar, gs);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [Test]
        public static void Collect_ReturnCombinedScore_GivenValidHorizontalAndVerticalWords()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string>(),
                WordsAppearedInValidation = new List<string>(),
                CorrectWords = new Dictionary<string, int>(),
                BoardChar = new char[15, 15]
            };
            gs.BoardChar[0, 0] = 'T';
            gs.BoardChar[0, 1] = 'E';
            gs.BoardChar[0, 2] = 'S';
            gs.BoardChar[0, 3] = 'T';
            gs.BoardChar[1, 0] = 'E';
            gs.BoardChar[2, 0] = 'S';
            gs.BoardChar[3, 0] = 'T';
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "TEST", "TEES" });

            // Act
            int result = WordCollector.Collect(0, 0, gs.BoardChar, gs);

            // Assert
            Assert.IsTrue(result > 0);
        }

    }
}