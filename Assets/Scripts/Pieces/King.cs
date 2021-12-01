using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class King : Piece
{
    public King(Tile tile, PieceType pieceType, Color color) {
        this.tile = tile;
        this.pieceType = pieceType;
        this.color = color;
    }

    public override Tile[] getMovableTiles() {
        List<Tile> tiles = new List<Tile>(8);

        foreach (Tile tile in tile.getNeighbours(true)) {
            if(tile != null && !canBeCheckedAtTile(tile) && tile.piece == null || tile.piece.color != color) {
                tiles.Add(tile);
            }
        }

        return tiles.ToArray();
    }

    bool canBeCheckedAtTile(Tile tile) {
        foreach (Piece piece in PieceController.pieceController.pieces) {
            if (piece.color != color && piece.getMovableTiles().Contains(tile)) {
                return true;
            }
        }
        return false;
    }
}
