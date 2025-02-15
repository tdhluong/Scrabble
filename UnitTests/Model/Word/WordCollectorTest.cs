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
        // Test cases using LLM
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

        [Test]
        public static void VCollect_ReturnZero_GivenWordLengthOne()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string>(),
                WordsAppearedInValidation = new List<string>(),
                CorrectWords = new Dictionary<string, int>(),
                BoardChar = new char[15, 15]
            };
            gs.BoardChar[0, 0] = 'A';

            // Act
            int result = WordCollector.VCollect(0, 0, gs.BoardChar, gs);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public static void HCollect_ReturnZero_GivenWordLengthOne()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string>(),
                WordsAppearedInValidation = new List<string>(),
                CorrectWords = new Dictionary<string, int>(),
                BoardChar = new char[15, 15]
            };
            gs.BoardChar[0, 0] = 'A'; // Single character word

            // Act
            int result = WordCollector.HCollect(0, 0, gs.BoardChar, gs);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public static void VCollect_ReturnZero_GivenValidVerticalWordAlreadyAppeared()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string> { "TEST" },
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
            Assert.AreEqual(0, result);
        }

        [Test]
        public static void HCollect_ReturnZero_GivenValidHorizontalWordAlreadyAppeared()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string> { "TEST" },
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
            Assert.AreEqual(0, result);
        }


        [Test]
        public static void Collect_ReturnZero_GivenNoValidWords()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string>(),
                WordsAppearedInValidation = new List<string>(),
                CorrectWords = new Dictionary<string, int>(),
                BoardChar = new char[15, 15]
            };
            gs.BoardChar[0, 0] = 'A'; // Single character, no valid word
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "TEST" });

            // Act
            int result = WordCollector.Collect(0, 0, gs.BoardChar, gs);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public static void Collect_ReturnValidScore_GivenOnlyHorizontalWord()
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
            gs.BoardChar[0, 1] = 'O'; // Valid horizontal word
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "TO" });

            // Act
            int result = WordCollector.Collect(0, 0, gs.BoardChar, gs);

            // Assert
            Assert.IsTrue(result > 0);
        }


        // Additional tests manually to cover all statements and branches    
        [Test]
        public static void VCollect_ReturnPositiveScore_GivenValidVerticalWordAtLastRow()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string>(),
                BoardChar = new char[15, 15]
            };
            int lastRow = gs.BoardChar.GetLength(0) - 1;
            gs.BoardChar[lastRow - 3, 0] = 'T';
            gs.BoardChar[lastRow - 2, 0] = 'E';
            gs.BoardChar[lastRow - 1, 0] = 'S';
            gs.BoardChar[lastRow, 0] = 'T';
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "TEST" });

            // Act
            int result = WordCollector.VCollect(lastRow - 3, 0, gs.BoardChar, gs);

            // Assert
            Assert.IsTrue(result > 0);
        }

        [Test]
        public static void HCollect_ReturnPositiveScore_GivenValidHorizontalWordAtLastColumn()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string>(),
                BoardChar = new char[15, 15]
            };
            int lastCol = gs.BoardChar.GetLength(1) - 4; // Leave space for a 4-letter word
            gs.BoardChar[0, lastCol] = 'T';
            gs.BoardChar[0, lastCol + 1] = 'E';
            gs.BoardChar[0, lastCol + 2] = 'S';
            gs.BoardChar[0, lastCol + 3] = 'T';
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "TEST" });

            // Act
            int result = WordCollector.HCollect(0, lastCol, gs.BoardChar, gs);

            // Assert
            Assert.IsTrue(result > 0);
        }

        [Test]
        public static void VCollect__ReturnNegativeOne_GivenInvalidWord()
        {
            GameState gs = new GameState();
            char[,] bc = gs.BoardChar;
            int i = 13, j = 6;
            bc[i, j] = 'T';
            bc[i + 1, j] = 'E';

            int result = WordCollector.VCollect(i, j, bc, gs);
            Assert.AreEqual(-1, result);
        }

        [Test]
        public static void HCollect__ReturnNegativeOne_GivenInvalidWord()
        {
            GameState gs = new GameState();
            char[,] bc = gs.BoardChar;
            int j = 13, i = 6;
            bc[i, j] = 'T';
            bc[i, j + 1] = 'E';

            int result = WordCollector.HCollect(i, j, bc, gs);
            Assert.AreEqual(-1, result);
        }

        [Test]
        public static void Collect_ReturnZero_GivenInvalidVerticalAndHorizontalWord()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string>(),
                BoardChar = new char[15, 15]
            };
            gs.BoardChar[0, 0] = 'T';
            gs.BoardChar[0, 1] = 'X'; // Invalid horizontal word
            gs.BoardChar[1, 0] = 'X'; // Invalid vertical word
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "TEST" }); // "TX" and "TX" are not in the word list

            // Act
            int result = WordCollector.Collect(0, 0, gs.BoardChar, gs);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public static void VCollect_ReturnZero_GivenValidWordAlreadyAppearedAtEndOfBoard()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string> { "TO" }, // Word already appeared
                BoardChar = new char[15, 15]
            };
            int lastRow = gs.BoardChar.GetLength(0) - 1;
            gs.BoardChar[lastRow - 1, 0] = 'T';
            gs.BoardChar[lastRow, 0] = 'O'; // Valid word "TO" at the end of the board
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "TO" });

            // Act
            int result = WordCollector.VCollect(lastRow - 1, 0, gs.BoardChar, gs);

            // Assert
            Assert.AreEqual(0, result); // Should return 0 because the word has already appeared
        }

        [Test]
        public static void HCollect_ReturnZero_GivenValidWordAlreadyAppearedAtEndOfBoard()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string> { "TO" }, // Word already appeared
                BoardChar = new char[15, 15]
            };
            int lastCol = gs.BoardChar.GetLength(1) - 2; // Leave space for a 2-letter word
            gs.BoardChar[0, lastCol] = 'T';
            gs.BoardChar[0, lastCol + 1] = 'O'; // Valid word "TO" at the end of the board
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "TO" });

            // Act
            int result = WordCollector.HCollect(0, lastCol, gs.BoardChar, gs);

            // Assert
            Assert.AreEqual(0, result); // Should return 0 because the word has already appeared
        }

        [Test]
        public static void HCollect_ReturnZero_GivenSingleCharacterWord()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string>(),
                BoardChar = new char[15, 15]
            };
            gs.BoardChar[0, 0] = 'A'; // Single character word
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "A" });

            // Act
            int result = WordCollector.HCollect(0, 0, gs.BoardChar, gs);

            // Assert
            Assert.AreEqual(0, result); // Should return 0 because word length is 1
        }

        [Test]
        public static void Collect_BreakLoop_GivenValidWordAlreadyAppeared()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string> { "TO" }, // Word already appeared
                BoardChar = new char[15, 15]
            };
            gs.BoardChar[0, 0] = 'T';
            gs.BoardChar[0, 1] = 'O'; // Valid word "TO"
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "TO" });

            // Act
            int result = WordCollector.Collect(0, 0, gs.BoardChar, gs);

            // Assert
            Assert.AreEqual(0, result); // Loop should break immediately
        }

        [Test]
        public static void HCollect_LoopTerminates_GivenEndOfBoard()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string>(),
                BoardChar = new char[15, 15]
            };
            int lastCol = gs.BoardChar.GetLength(1) - 1;
            gs.BoardChar[0, lastCol] = 'A'; // Single character at the end of the board
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "A" });

            // Act
            int result = WordCollector.HCollect(0, lastCol, gs.BoardChar, gs);

            // Assert
            Assert.AreEqual(0, result); // Loop should terminate immediately
        }

        [Test]
        public static void VCollect_LoopTerminates_GivenEndOfBoard()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string>(),
                BoardChar = new char[15, 15]
            };
            int lastRow = gs.BoardChar.GetLength(0) - 1;
            gs.BoardChar[lastRow, 0] = 'A'; // Single character at the end of the board
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "A" });

            // Act
            int result = WordCollector.VCollect(lastRow, 0, gs.BoardChar, gs);

            // Assert
            Assert.AreEqual(0, result); // Should return 0 because word length is 1
        }

        // This test case is supposed to cover "return -1;" statement "if( !VFound && !HFound )"
        // but since there is a "bool VFound = true;" and there is no vertical direction check after that to reset VFound
        // therefore VFound is always true and we can't get into that condition to cover "return -1;" statement.

        [Test]
        public static void Collect_ReturnZero_GivenInvalidVerticalAndValidHorizontalWord()
        {
            // Arrange
            GameState gs = new GameState
            {
                WordsAppeared = new List<string>(),
                BoardChar = new char[15, 15]
            };
            gs.BoardChar[0, 0] = 'T';
            gs.BoardChar[0, 1] = 'X'; // Invalid horizontal word
            gs.BoardChar[1, 0] = 'N'; // Invalid vertical word
            File.WriteAllLines(@"Model\Word\wordlist.txt", new[] { "T0", "ON" }); // "TX" and "TX" are not in the word list

            // Act
            int result = WordCollector.Collect(1, 3, gs.BoardChar, gs);

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}