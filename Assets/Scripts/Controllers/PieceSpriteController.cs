using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpriteController : MonoBehaviour {
    public static PieceSpriteController pieceSpriteController;

    public Sprite test;
    Dictionary<Piece, GameObject> pieceGameObjectMap;

    // Start is called before the first frame update
    void Start() {
        pieceSpriteController = this;

        pieceGameObjectMap = new Dictionary<Piece, GameObject>(32);
    }

    // Update is called once per frame
    void Update() {

    }

    public void addPiece(Piece piece) {
        GameObject gameObject = new GameObject();
        gameObject.transform.position = new Vector3(piece.tile.X, piece.tile.Y, 0);

        gameObject.transform.SetParent(transform, true);

        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "PIECES";
        spriteRenderer.sprite = test;

        pieceGameObjectMap.Add(piece, gameObject);
    }

    void findSprite(Piece piece) {
        switch (piece.color) {
            case Color.BLACK:
                switch (piece.pieceType) {
                    case PieceType.PAWN:

                        break;
                    case PieceType.BISHOP:
                        break;
                    case PieceType.KNIGHT:
                        break;
                    case PieceType.ROOK:
                        break;
                    case PieceType.QUEEN:
                        break;
                    case PieceType.KING:
                        break;
                    default:
                        break;
                }
                break;

            case Color.WHITE:
                switch (piece.pieceType) {
                    case PieceType.PAWN:

                        break;
                    case PieceType.BISHOP:
                        break;
                    case PieceType.KNIGHT:
                        break;
                    case PieceType.ROOK:
                        break;
                    case PieceType.QUEEN:
                        break;
                    case PieceType.KING:
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
}
