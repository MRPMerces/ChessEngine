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

    public override Tile[] getMovableTiles(bool OnlyTakes = false) {
        List<Tile> tiles = new List<Tile>(8);

        foreach (Tile tile in tile.getNeighbours(true)) {
            if (OnlyTakes || !canBeCheckedAtTile(tile)) {
                switch (World.world.isValidMove(tile, color)) {
                    case State.VALID:
                        tiles.Add(tile);
                        break;
                    case State.INVALID:
                        break;
                    case State.TAKES:
                        tiles.Add(tile);
                        break;
                    default:
                        break;
                }
            }
        }

        return tiles.ToArray();
    }

    bool canBeCheckedAtTile(Tile tile) {
        foreach (Piece piece in PieceController.pieceController.pieces) {
            if (piece.color != color && piece.getMovableTiles(true).Contains(tile)) {
                return true;
            }
        }
        return false;
    }
}
