using NUnit.Framework;
using Scrabble2018;
using Scrabble2018.Model.Word;
using Scrabble2018.Model;
using System.Collections.Generic;
using System;

namespace UnitTests
{
    public class ScoreUtilityTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public static void ScoreUtilityTests_ScoreCalc_Return_corretScoreSum()
        {
            GameState gs = new GameState();
            char[,] board = new char[15, 15];
            board[7, 7] = 'H';
            board[7, 8] = 'E';
            board[7, 9] = 'L';
            board[7, 10] = 'L';
            board[7, 11] = 'O';

            int expectedScore = ScoreUtility.ScoreCalc(7, 7, 11, "h", board, gs.boardTiles);
            Assert.AreEqual(expectedScore, ScoreUtility.ScoreCalc(7, 7, 11, "h", board, gs.boardTiles));
        }
        [Test]
        public static void ScoreUtilityTests_ScoreCalc_VerticalDirection()
        {
            GameState gs = new GameState();
            char[,] board = new char[15, 15];
            board[7, 7] = 'H';
            board[8, 7] = 'E';
            board[9, 7] = 'L';
            board[10, 7] = 'L';
            board[11, 7] = 'O';

            int expectedScore = ScoreUtility.ScoreCalc(7, 7, 11, "v", board, gs.boardTiles);
            Assert.AreEqual(expectedScore, ScoreUtility.ScoreCalc(7, 7, 11, "v", board, gs.boardTiles));
        }

        [Test]
        // TestCase for Word Multiplier
        [TestCase(2, 2, ExpectedResult = 18, TestName = "ScoreUtilityTests_ScoreCalc_VerticalDirection_Additional(2,2)")]
        public static int ScoreUtilityTests_ScoreCalc_VerticalDirection_Additional(int i, int j)
        {
            GameState gs = new GameState();
            char[,] bc = gs.BoardChar;
            bc[i, j] = 'H';
            bc[i + 1, j] = 'E';
            bc[i + 2, j] = 'L';
            bc[i + 3, j] = 'P';

            // (2, 2) take all letters "HELP"=18 ((H=4 + E=1 + L=1 + P=3)x2)
            int result = ScoreUtility.ScoreCalc(j, i, i + 3, "v", bc, gs.boardTiles);
            return result;
        }

        [Test]
        // TestCase for Letter Multiplie
        [TestCase(1, 1, ExpectedResult = 22, TestName = "ScoreUtilityTests_ScoreCalc_HorizontalDirection(1,1)")]
        public static int ScoreUtilityTests_ScoreCalc_HorizontalDirection(int i, int j)
        {
            GameState gs = new GameState();
            char[,] bc = gs.BoardChar;
            bc[i, j] = 'Z';
            bc[i, j + 1] = 'E';
            bc[i, j + 2] = 'B';
            bc[i, j + 3] = 'R';
            bc[i, j + 4] = 'A';

            // (1, 1) take 2 letters "ZE"=22 (Z=10x2 + E=1) 
            int result = ScoreUtility.ScoreCalc(j, i, i + 1, "h", bc, gs.boardTiles);
            return result;
        }
    }
}
