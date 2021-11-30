using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PieceController : MonoBehaviour {

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

        foreach (Piece piece in PieceController.pieceController.pieces) {
            PieceSpriteController.pieceSpriteController.addPiece(piece);
        }
    }

    // Update is called once per frame
    private void Update() {

    }
}
