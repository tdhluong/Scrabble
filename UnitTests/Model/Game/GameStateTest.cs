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
        public void GameState_GamePass_PlayerNow_ShouldReturn_4()
        {
            GameState gs = new GameState();

            gs.NumOfPlayers = 4;
            gs.PlayerNow = 2;
            gs.LastAction = "Play";

            gs.GamePass();

            Assert.AreEqual(3,  gs.PlayerNow );
        }



        [Test]
        public void GameState_GamePass_LastAction_ShouldReturn_Pass()
        {
            GameState gs = new GameState();

            gs.NumOfPlayers = 4;
            gs.PlayerNow = 2;
            gs.LastAction = "play";

            gs.GamePass();

            Assert.AreEqual("pass", gs.LastAction);
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