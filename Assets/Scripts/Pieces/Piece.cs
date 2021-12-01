using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType { PAWN, BISHOP, KNIGHT, ROOK, QUEEN, KING }

public class Piece
{
    public Tile tile;
    public PieceType pieceType;
    public Color color;

    public void move(Tile tile) {
        this.tile.piece = null;
        tile.piece = this;
        this.tile = tile;
    }

    public virtual Tile[] getMovableTiles() {
        return null;
    }
}
