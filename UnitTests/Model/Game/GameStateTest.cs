using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Scrabble2018;
using Scrabble2018.Model;


namespace UnitTests.Model.Game
{
    public class GameStateTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GameState_NextPlayer_ShouldReturn_0()
        {
            GameState gs = new GameState();

            gs.PlayerNow = 3;
            gs.NumOfPlayers = 4;

            var result = gs.NextPlayer();
            Assert.AreEqual(0, result);
        }

        [Test]
        public void GameState_NextPlayer_ShouldReturn_PlayerNow()
        {
            GameState gs = new GameState();

            gs.PlayerNow = 2;
            gs.NumOfPlayers = 4;

            var result = gs.NextPlayer();
            Assert.AreEqual(3, result);
        }





    }
}