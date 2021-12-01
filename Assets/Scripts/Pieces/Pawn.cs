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

    void checkMove(Tile tile) {
        if (tile.X != this.tile.X) {
            if (tile.piece != null) {
                move(tile);
            }
            //Invalid move
            return;
        }

        if (tile.Y > this.tile.Y + 2) {
            //Invalid move
            return;
        }

        else if (tile.Y == 4) {
            move(tile);
        }
    }

    public override Tile[] getMovableTiles() {
        List<Tile> tiles = new List<Tile>();

        switch (color) {
            case Color.BLACK:

                break;

            case Color.WHITE:
                if (World.world.getTileAt(tile.X, tile.Y + 1) != null && World.world.getTileAt(tile.X, tile.Y + 1).piece == null) {
                    // North tile
                    tiles.Add(World.world.getTileAt(tile.X, tile.Y + 1));
                    if (tile.Y == 1 && World.world.getTileAt(tile.X, 3) != null && World.world.getTileAt(tile.X, 3).piece == null) {
                        // North +1 tile
                        tiles.Add(World.world.getTileAt(tile.X, 3));
                    }
                }

                if (World.world.getTileAt(tile.X + 1, tile.Y + 1) != null && World.world.getTileAt(tile.X + 1, tile.Y + 1).piece != null) {
                    // North-east tile
                    tiles.Add(World.world.getTileAt(tile.X + 1, tile.Y + 1));
                }

                if (World.world.getTileAt(tile.X - 1, tile.Y + 1) != null && World.world.getTileAt(tile.X - 1, tile.Y + 1).piece != null) {
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
