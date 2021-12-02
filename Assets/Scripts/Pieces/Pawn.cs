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

                switch (World.world.isValidMove(tile.X + 1, tile.Y - 1, color)) {
                    // South-east tile
                    case State.VALID:
                        tiles.Add(World.world.getTileAt(tile.X + 1, tile.Y - 1));
                        break;
                    case State.INVALID:
                        break;
                    case State.TAKES:
                        tiles.Add(World.world.getTileAt(tile.X + 1, tile.Y - 1));
                        break;
                    default:
                        break;
                }

                switch (World.world.isValidMove(tile.X - 1, tile.Y - 1, color)) {
                    // South-west tile
                    case State.VALID:
                        tiles.Add(World.world.getTileAt(tile.X - 1, tile.Y - 1));
                        break;
                    case State.INVALID:
                        break;
                    case State.TAKES:
                        tiles.Add(World.world.getTileAt(tile.X - 1, tile.Y - 1));
                        break;
                    default:
                        break;
                }

                break;

            case Color.WHITE:
                if (!OnlyTakes && World.world.isValidMove(tile.X, tile.Y + 1, color) == State.VALID) {
                    // North tile
                    tiles.Add(World.world.getTileAt(tile.X, tile.Y + 1));
                }

                if (!OnlyTakes && tile.Y == 6 && World.world.isValidMove(tile.X, 3, color) == State.VALID) {
                    // North +1 tile
                    tiles.Add(World.world.getTileAt(tile.X, 3));
                }

                switch (World.world.isValidMove(tile.X + 1, tile.Y + 1, color)) {
                    // North-east tile
                    case State.VALID:
                        tiles.Add(World.world.getTileAt(tile.X + 1, tile.Y + 1));
                        break;
                    case State.INVALID:
                        break;
                    case State.TAKES:
                        tiles.Add(World.world.getTileAt(tile.X + 1, tile.Y + 1));
                        break;
                    default:
                        Debug.LogError("Unknown State");
                        break;
                }

                switch (World.world.isValidMove(tile.X - 1, tile.Y, color)) {
                    // North-west tile
                    case State.VALID:
                        tiles.Add(World.world.getTileAt(tile.X - 1, tile.Y));
                        break;
                    case State.INVALID:
                        break;
                    case State.TAKES:
                        tiles.Add(World.world.getTileAt(tile.X - 1, tile.Y));
                        break;
                    default:
                        Debug.LogError("Unknown State");
                        break;
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
