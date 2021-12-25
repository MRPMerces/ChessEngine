using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum WinState { RUNNING, WHITEWON, BLACKWON, REMIS, PATT }
public class PieceController : MonoBehaviour {

    public static PieceController pieceController;
    public List<Piece> whitePieces;
    public List<Piece> blackPieces;

    public WinState winState = WinState.RUNNING;
    public Color playerToMove = Color.WHITE;

    private void OnEnable() {
        pieceController = this;

        whitePieces = new List<Piece>(32);
        blackPieces = new List<Piece>(32);

    }

    // Start is called before the first frame update
    private void Start() {
        // White pawns.
        whitePieces.Add(new Pawn(World.world.getTileAt(0, 1), Color.WHITE));
        whitePieces.Add(new Pawn(World.world.getTileAt(1, 1), Color.WHITE));
        whitePieces.Add(new Pawn(World.world.getTileAt(2, 1), Color.WHITE));
        whitePieces.Add(new Pawn(World.world.getTileAt(3, 1), Color.WHITE));
        whitePieces.Add(new Pawn(World.world.getTileAt(4, 1), Color.WHITE));
        whitePieces.Add(new Pawn(World.world.getTileAt(5, 1), Color.WHITE));
        whitePieces.Add(new Pawn(World.world.getTileAt(6, 1), Color.WHITE));
        whitePieces.Add(new Pawn(World.world.getTileAt(7, 1), Color.WHITE));

        // White officers.
        whitePieces.Add(new Rook(World.world.getTileAt(0, 0), Color.WHITE));
        whitePieces.Add(new Knight(World.world.getTileAt(1, 0), Color.WHITE));
        whitePieces.Add(new Bishop(World.world.getTileAt(2, 0), Color.WHITE));
        whitePieces.Add(new Queen(World.world.getTileAt(3, 0), Color.WHITE));
        whitePieces.Add(new King(World.world.getTileAt(4, 0), Color.WHITE));
        whitePieces.Add(new Bishop(World.world.getTileAt(5, 0), Color.WHITE));
        whitePieces.Add(new Knight(World.world.getTileAt(6, 0), Color.WHITE));
        whitePieces.Add(new Rook(World.world.getTileAt(7, 0), Color.WHITE));

        // Black pawns.
        blackPieces.Add(new Pawn(World.world.getTileAt(0, 6), Color.BLACK));
        blackPieces.Add(new Pawn(World.world.getTileAt(1, 6), Color.BLACK));
        blackPieces.Add(new Pawn(World.world.getTileAt(2, 6), Color.BLACK));
        blackPieces.Add(new Pawn(World.world.getTileAt(3, 6), Color.BLACK));
        blackPieces.Add(new Pawn(World.world.getTileAt(4, 6), Color.BLACK));
        blackPieces.Add(new Pawn(World.world.getTileAt(5, 6), Color.BLACK));
        blackPieces.Add(new Pawn(World.world.getTileAt(6, 6), Color.BLACK));
        blackPieces.Add(new Pawn(World.world.getTileAt(7, 6), Color.BLACK));

        // Black officers
        blackPieces.Add(new Rook(World.world.getTileAt(0, 7), Color.BLACK));
        blackPieces.Add(new Knight(World.world.getTileAt(1, 7), Color.BLACK));
        blackPieces.Add(new Bishop(World.world.getTileAt(2, 7), Color.BLACK));
        blackPieces.Add(new Queen(World.world.getTileAt(3, 7), Color.BLACK));
        blackPieces.Add(new King(World.world.getTileAt(4, 7), Color.BLACK));
        blackPieces.Add(new Bishop(World.world.getTileAt(5, 7), Color.BLACK));
        blackPieces.Add(new Knight(World.world.getTileAt(6, 7), Color.BLACK));
        blackPieces.Add(new Rook(World.world.getTileAt(7, 7), Color.BLACK));

        foreach (Piece piece in whitePieces) {
            // Add the sprite.
            PieceSpriteController.pieceSpriteController.addPiece(piece);

            // Assign pieces to their tiles.
            piece.tile.piece = piece;
        }

        foreach (Piece piece in blackPieces) {
            // Add the sprite.
            PieceSpriteController.pieceSpriteController.addPiece(piece);

            // Assign pieces to their tiles.
            piece.tile.piece = piece;
        }
    }

    public void movePiece(Piece piece, Tile tile) {
        if (tile.piece != null) {
            if (tile.piece.color == piece.color) {
                Debug.LogError("Trying to move piece to a friendly piece");
            }

            else {
                switch (piece.color) {
                    case Color.BLACK:
                        whitePieces.Remove(tile.piece);
                        break;

                    case Color.WHITE:
                        blackPieces.Remove(tile.piece);
                        break;

                    default:
                        Debug.LogError("Unknown color");
                        break;
                }
                // Takes
                PieceSpriteController.pieceSpriteController.takes(tile.piece);
            }
        }

        PieceSpriteController.pieceSpriteController.movePiece(piece, tile);
        piece.move(tile);

        switch (playerToMove) {
            case Color.BLACK:
                playerToMove = Color.WHITE;
                break;

            case Color.WHITE:
                playerToMove = Color.BLACK;
                AIController.aIController.doMoveAI();
                break;

            default:
                Debug.LogError("Unknown color");
                break;
        }
    }

    void checkWinState() {
        if (whitePieces.Count() == 1 && whitePieces[0].pieceType == PieceType.KING && blackPieces.Count() == 1 && blackPieces[1].pieceType == PieceType.KING) {
            winState = WinState.REMIS;
        }
    }
}
