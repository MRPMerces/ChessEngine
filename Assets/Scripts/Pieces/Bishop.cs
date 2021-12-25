using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public Bishop(Tile tile, Color color) {
        this.tile = tile;
        this.color = color;

        pieceType = PieceType.BISHOP;
    }

    public override Tile[] getMovableTiles(bool OnlyTakes = false) {
        List<Tile> tiles = new List<Tile>(8);

        tiles.AddRange(processTiles(-1, 1));
        tiles.AddRange(processTiles(-1, -1));
        tiles.AddRange(processTiles(1, 1));
        tiles.AddRange(processTiles(1, -1));

        return tiles.ToArray();
    }

    List<Tile> processTiles(int x, int y) {
        int X = tile.X;
        int Y = tile.Y;
        bool run = true;
        List<Tile> tiles = new List<Tile>(8);
        while (run) {
            X += x;
            Y += y;
            switch (World.world.isValidMove(X, Y, color)) {
                case State.VALID:
                    tiles.Add(World.world.getTileAt(X, Y));
                    break;

                case State.INVALID:
                    run = false;
                    break;

                case State.TAKES:
                    tiles.Add(World.world.getTileAt(X, Y));
                    run = false;
                    break;

                default:
                    Debug.LogError("Unknown State");
                    break;
            }
        }

        return tiles;
    }
}
