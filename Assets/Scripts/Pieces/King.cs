using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class King : Piece {
    public King(Tile tile, Color color) {
        this.tile = tile;
        this.color = color;

        pieceType = PieceType.KING;
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
        switch (color) {
            case Color.BLACK:
                foreach (Piece piece in PieceController.pieceController.whitePieces) {
                    if (piece.color != color && piece.getMovableTiles(true).Contains(tile)) {
                        return true;
                    }
                }
                break;

            case Color.WHITE:
                foreach (Piece piece in PieceController.pieceController.blackPieces) {
                    if (piece.color != color && piece.getMovableTiles(true).Contains(tile)) {
                        return true;
                    }
                }
                break;

            default:
                Debug.LogError("Unknown color");
                break;
        }

        return false;
    }
}
