using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public Pawn(Tile tile, PieceType pieceType, Color color) {
        this.tile = tile;
        this.pieceType = pieceType;
        this.color = color;
    }
    public override Tile[] getMovableTiles(bool OnlyTakes = false) {
        List<Tile> tiles = new List<Tile>();

        switch (color) {
            case Color.BLACK:
                if (!OnlyTakes && World.world.isValidMove(tile.X, tile.Y - 1, color) == State.VALID) {
                    // South tile
                    tiles.Add(World.world.getTileAt(tile.X, tile.Y - 1));
                }

                if (!OnlyTakes && tile.Y == 6 && World.world.isValidMove(tile.X, 4, color) == State.VALID) {
                    // South +1 tile
                    tiles.Add(World.world.getTileAt(tile.X, 4));
                }

                if (World.world.isValidMove(tile.X + 1, tile.Y - 1, color) == State.TAKES) {
                    // South-east tile
                    tiles.Add(World.world.getTileAt(tile.X + 1, tile.Y - 1));
                }

                if (World.world.isValidMove(tile.X - 1, tile.Y - 1, color) == State.TAKES) {
                    // South-west tile
                    tiles.Add(World.world.getTileAt(tile.X - 1, tile.Y - 1));
                }
                
                break;

            case Color.WHITE:
                if (!OnlyTakes && World.world.isValidMove(tile.X, tile.Y + 1, color) == State.VALID) {
                    // North tile
                    tiles.Add(World.world.getTileAt(tile.X, tile.Y + 1));
                }

                if (!OnlyTakes && tile.Y == 1 && World.world.isValidMove(tile.X, 3, color) == State.VALID) {
                    // North +1 tile
                    tiles.Add(World.world.getTileAt(tile.X, 3));
                }

                if (World.world.isValidMove(tile.X + 1, tile.Y + 1, color) == State.TAKES) {
                    // North-east tile
                    tiles.Add(World.world.getTileAt(tile.X + 1, tile.Y + 1));
                }

                if (World.world.isValidMove(tile.X - 1, tile.Y + 1, color) == State.TAKES) {
                    // North-west tile
                    tiles.Add(World.world.getTileAt(tile.X - 1, tile.Y + 1));
                }

                break;

            default:
                Debug.LogError("Unknown color");
                return null;
        }

        /// Anpacang, all the tiles are white only.
        return tiles.ToArray();
    }
}
