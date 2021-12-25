using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
    public static AIController aIController;

    Color color = Color.BLACK;

    Dictionary<Piece, Dictionary<Tile, int>> values;
    // Start is called before the first frame update
    void Start() {
        aIController = this;
        values = new Dictionary<Piece, Dictionary<Tile, int>>();
    }

    public void doMoveAI() {
        Piece pieceToMove = null;
        Tile tileToMoveTo = null;
        int highestScore = 0;

        foreach (Piece piece in PieceController.pieceController.blackPieces) {
            //Dictionary<Tile, int> value = new Dictionary<Tile, int>();
            foreach (Tile tile in piece.getMovableTiles()) {
                int score = 0;
                if (tile.piece != null) {
                    score += getValueOfPiece(tile.piece);
                }

                if (score >= highestScore) {
                    pieceToMove = piece;
                    tileToMoveTo = tile;
                    highestScore = score;
                }
            }
        }

        PieceController.pieceController.movePiece(pieceToMove, tileToMoveTo);
    }

    int getValueOfPiece(Piece piece) {
        switch (piece.pieceType) {
            case PieceType.PAWN:
                return 1;

            case PieceType.BISHOP:
                return 3;

            case PieceType.KNIGHT:
                return 3;

            case PieceType.ROOK:
                return 5;

            case PieceType.QUEEN:
                return 10;

            case PieceType.KING:
                return 100;

            default:
                Debug.LogError("Unknown type");
                return 0;
        }
    }
}
