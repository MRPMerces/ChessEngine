using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum WinState { RUNNING, WHITEWON, BLACKWON, REMIS, PATT }
public class PieceController : MonoBehaviour
{

    public static PieceController pieceController;
    public List<Piece> pieces;

    public WinState winState = WinState.RUNNING;

    private void OnEnable() {
        pieceController = this;

        pieces = new List<Piece>(32);

    }

    // Start is called before the first frame update
    private void Start() {
        // White pawns.
        pieces.Add(new Pawn(World.world.getTileAt(0, 1), PieceType.PAWN, Color.WHITE));
        pieces.Add(new Pawn(World.world.getTileAt(1, 1), PieceType.PAWN, Color.WHITE));
        pieces.Add(new Pawn(World.world.getTileAt(2, 1), PieceType.PAWN, Color.WHITE));
        pieces.Add(new Pawn(World.world.getTileAt(3, 1), PieceType.PAWN, Color.WHITE));
        pieces.Add(new Pawn(World.world.getTileAt(4, 1), PieceType.PAWN, Color.WHITE));
        pieces.Add(new Pawn(World.world.getTileAt(5, 1), PieceType.PAWN, Color.WHITE));
        pieces.Add(new Pawn(World.world.getTileAt(6, 1), PieceType.PAWN, Color.WHITE));
        pieces.Add(new Pawn(World.world.getTileAt(7, 1), PieceType.PAWN, Color.WHITE));

        // White officers.
        pieces.Add(new Rook(World.world.getTileAt(0, 0), PieceType.ROOK, Color.WHITE));
        pieces.Add(new Knight(World.world.getTileAt(1, 0), PieceType.KNIGHT, Color.WHITE));
        pieces.Add(new Bishop(World.world.getTileAt(2, 0), PieceType.BISHOP, Color.WHITE));
        pieces.Add(new Queen(World.world.getTileAt(3, 0), PieceType.QUEEN, Color.WHITE));
        pieces.Add(new King(World.world.getTileAt(4, 0), PieceType.KING, Color.WHITE));
        pieces.Add(new Bishop(World.world.getTileAt(5, 0), PieceType.BISHOP, Color.WHITE));
        pieces.Add(new Knight(World.world.getTileAt(6, 0), PieceType.KNIGHT, Color.WHITE));
        pieces.Add(new Rook(World.world.getTileAt(7, 0), PieceType.ROOK, Color.WHITE));

        // Black pawns.
        pieces.Add(new Pawn(World.world.getTileAt(0, 6), PieceType.PAWN, Color.BLACK));
        pieces.Add(new Pawn(World.world.getTileAt(1, 6), PieceType.PAWN, Color.BLACK));
        pieces.Add(new Pawn(World.world.getTileAt(2, 6), PieceType.PAWN, Color.BLACK));
        pieces.Add(new Pawn(World.world.getTileAt(3, 6), PieceType.PAWN, Color.BLACK));
        pieces.Add(new Pawn(World.world.getTileAt(4, 6), PieceType.PAWN, Color.BLACK));
        pieces.Add(new Pawn(World.world.getTileAt(5, 6), PieceType.PAWN, Color.BLACK));
        pieces.Add(new Pawn(World.world.getTileAt(6, 6), PieceType.PAWN, Color.BLACK));
        pieces.Add(new Pawn(World.world.getTileAt(7, 6), PieceType.PAWN, Color.BLACK));

        // Black officers
        pieces.Add(new Rook(World.world.getTileAt(0, 7), PieceType.ROOK, Color.BLACK));
        pieces.Add(new Knight(World.world.getTileAt(1, 7), PieceType.KNIGHT, Color.BLACK));
        pieces.Add(new Bishop(World.world.getTileAt(2, 7), PieceType.BISHOP, Color.BLACK));
        pieces.Add(new Queen(World.world.getTileAt(3, 7), PieceType.QUEEN, Color.BLACK));
        pieces.Add(new King(World.world.getTileAt(4, 7), PieceType.KING, Color.BLACK));
        pieces.Add(new Bishop(World.world.getTileAt(5, 7), PieceType.BISHOP, Color.BLACK));
        pieces.Add(new Knight(World.world.getTileAt(6, 7), PieceType.KNIGHT, Color.BLACK));
        pieces.Add(new Rook(World.world.getTileAt(7, 7), PieceType.ROOK, Color.BLACK));

        foreach (Piece piece in pieces) {
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
                // Takes
                pieces.Remove(tile.piece);
                PieceSpriteController.pieceSpriteController.takes(tile.piece);
            }
        }

        PieceSpriteController.pieceSpriteController.movePiece(piece, tile);
        piece.move(tile);
    }

    void checkWinState() {
        if(pieces.Count() == 2 && pieces[0].pieceType == PieceType.KING && pieces[1].pieceType == PieceType.KING) {
            winState = WinState.REMIS;
        }
    }
}
