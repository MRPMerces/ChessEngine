using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PieceController : MonoBehaviour
{

    public static PieceController pieceController;
    public List<Piece> pieces;

    private void OnEnable() {
        pieceController = this;

        pieces = new List<Piece>(32);

    }

    // Start is called before the first frame update
    private void Start() {
        pieces.Add(new Pawn(World.world.getTileAt(0, 1), PieceType.PAWN, Color.WHITE));
        pieces.Add(new Pawn(World.world.getTileAt(1, 1), PieceType.PAWN, Color.WHITE));
        pieces.Add(new Pawn(World.world.getTileAt(2, 1), PieceType.PAWN, Color.WHITE));
        pieces.Add(new Pawn(World.world.getTileAt(3, 1), PieceType.PAWN, Color.WHITE));
        pieces.Add(new Pawn(World.world.getTileAt(4, 1), PieceType.PAWN, Color.WHITE));
        pieces.Add(new Pawn(World.world.getTileAt(5, 1), PieceType.PAWN, Color.WHITE));
        pieces.Add(new Pawn(World.world.getTileAt(6, 1), PieceType.PAWN, Color.WHITE));
        pieces.Add(new Pawn(World.world.getTileAt(7, 1), PieceType.PAWN, Color.WHITE));
        pieces.Add(new King(World.world.getTileAt(4, 0), PieceType.KING, Color.WHITE));

        foreach (Piece piece in pieces) {
            PieceSpriteController.pieceSpriteController.addPiece(piece);
            piece.tile.piece = piece;
        }
    }

    // Update is called once per frame
    private void Update() {

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
}
