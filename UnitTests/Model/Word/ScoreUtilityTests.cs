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
    }
}
