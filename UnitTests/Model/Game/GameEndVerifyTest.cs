using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Scrabble2018;
using Scrabble2018.Model;


namespace UnitTests.Model.Game
{
    public class GameEndVerifyTest
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void GameEndVerify_TilebagLessThanSeven_Morethan7Tilebags_ShouldPass()
        {
            GameState gs = new GameState();
            AllTiles tiles = new AllTiles();

            gs.TilesBag.MakeTiles();

            bool result = GameEndVerify.TilebagLessThanSeven(gs);
            Assert.IsFalse(result);
        }

        [Test]
        public void GameEndVerify_TilebagLessThanSeven_Lessthan7Tilebags_ShouldPass()
        {
            GameState gs = new GameState();
            AllTiles at = new AllTiles();

            gs.TilesBag.ListTiles.Clear(); //?

            bool result = GameEndVerify.TilebagLessThanSeven(gs);
            Assert.IsTrue(result);
        }


        [Test]
        public void GameEndVerify_ExistsPlayerNoTiles_AllPlayersHaveTiles_ShouldFail()
        {
            GameState gameState = new GameState();
            Player p1 = new Player();
            Tile tile = new Tile('A', 12);
            p1.PlayingTiles.Add(tile);

            Player p2 = new Player();
            tile = new Tile('B', 10);
            p2.PlayingTiles.Add(tile);

            Player p3 = new Player();
            tile = new Tile('A', 12);
            p3.PlayingTiles.Add(tile);

            Player p4 = new Player();
            tile = new Tile('A', 12);
            p4.PlayingTiles.Add(tile);

            gameState.ListOfPlayers.Add(p1);
            gameState.ListOfPlayers.Add(p2);
            gameState.ListOfPlayers.Add(p3);
            gameState.ListOfPlayers.Add(p4);

            bool result = GameEndVerify.ExistsPlayerNoTiles(gameState);
            Assert.IsFalse(result);
        }

        [Test]
        public void GameEndVerify_ExistsPlayerNoTiles_OnePlayersHas0Tiles_ShouldPass()
        {
            GameState gameState = new GameState();
            Player p1 = new Player();
            Tile tile = new Tile('A', 12);
            p1.PlayingTiles.Add(tile);

            Player p2 = new Player();
            tile = new Tile('B', 10);
            p2.PlayingTiles.Add(tile);

            Player p3 = new Player();

            Player p4 = new Player();
            tile = new Tile('A', 12);
            p4.PlayingTiles.Add(tile);

            gameState.ListOfPlayers.Add(p1);
            gameState.ListOfPlayers.Add(p2);
            gameState.ListOfPlayers.Add(p3);
            gameState.ListOfPlayers.Add(p4);

            bool result = GameEndVerify.ExistsPlayerNoTiles(gameState);
            Assert.IsTrue(result);
        }

        [Test]
        public void GameEndVerify_GameEndScoring_ShouldPass()
        {
            GameState gs = new GameState();
            Player p1 = new Player();
            Tile tile = new Tile('A', 12);
            p1.PlayingTiles.Add(tile);

            Player p2 = new Player();
            tile = new Tile('B', 10);
            p2.PlayingTiles.Add(tile);

            gs.ListOfPlayers.Add(p1);
            gs.ListOfPlayers.Add(p2);


            foreach (Player p in gs.ListOfPlayers)
            {

                foreach (Tile t in p.PlayingTiles)
                {
                    p1.Score += AllTiles.ScoreOfLetter(t.TileChar);
                    p2.Score -= AllTiles.ScoreOfLetter(t.TileChar);
                }

            }

            gs.TilesBag.ListTiles.RemoveRange(5, 94);

            bool result = GameEndVerify.GameEndScoring(gs);
            Assert.IsTrue(result);
        }

    }
}