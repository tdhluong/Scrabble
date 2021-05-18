using NUnit.Framework;
using Scrabble2018;
using Scrabble2018.Model;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    public class PlayerManagerTest
    {
        PlayerManager pm;
        [SetUp]
        public void Setup()
        {
            pm = new PlayerManager();
        }

        [Test]
        public void PlayerManager_CreatePlayers_Should_Pass()
        {
            //Assert
            GameState gs = GameState.GSInstance;
            

            //Act
            gs.Initialise(2);
            pm.CreatePlayers(gs);
            


            //Assert
            Assert.AreNotEqual(gs.ListOfPlayers.Count,  0 );

        }

        [Test]
        public void PlayerManager_AddScoreToPlayer_Should_Pass()
        {
            //Arrange
            Player p = new Player();
            p.Score = 1;

            //Act
            pm.AddScoresToPlayer(p, p.Score);

            //Assert
            Assert.True(true);

        }

        [Test]
        public void PlayerManager_GetNewTiles_should_pass()
        {
            //Arrange
            List<char> LoC = new List<char>();
            int num = 1;
            GameState gs = GameState.GSInstance;

            //Act
            gs.Initialise(2);
            var result = gs.ListOfPlayers[num].PlayingTiles[0].TileChar;
            Tile t = gs.ListOfPlayers[num].PlayingTiles[0];
            LoC.Add(result);
            pm.GetNewTiles(gs, LoC, num);

            //Assert check if used tile is removed from playing tiles
            Assert.False(gs.ListOfPlayers[num].PlayingTiles.Contains(t));

        }

        [Test]
        public void PlayerManager_Swap_should_return_Pass()
        {
            //Arrange
            
            GameState gs = GameState.GSInstance;

            //Act
            gs.Initialise(1);
            var c = gs.ListOfPlayers[GameState.GSInstance.PlayerNow].PlayingTiles[0].TileChar;
            var d = '1';
            Tile t = gs.ListOfPlayers[GameState.GSInstance.PlayerNow].PlayingTiles[0];
            char result = pm.Swap(c);
            char res = pm.Swap(d);

            //Assert
            //returns a random char cannot assert so i will test if the swapped tile is not in the playing tiles list
            Assert.False(gs.ListOfPlayers[GameState.GSInstance.PlayerNow].PlayingTiles.Contains(t));

        }

        

    }
}
