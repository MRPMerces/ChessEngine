using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {
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
}
