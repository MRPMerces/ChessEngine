using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PieceSpriteController : MonoBehaviour {
    public static PieceSpriteController pieceSpriteController;

    public Sprite test;
    Dictionary<Piece, GameObject> pieceGameObjectMap;
    Dictionary<string, Sprite> pieceSprites;

    private void OnEnable() {
        loadSprites();
    }

    // Start is called before the first frame update
    void Start() {
        pieceSpriteController = this;

        pieceGameObjectMap = new Dictionary<Piece, GameObject>(32);
    }

    public void addPiece(Piece piece) {
        GameObject gameObject = new GameObject(piece.color.ToString() + "_" + piece.pieceType.ToString());
        gameObject.transform.position = piece.tile.toVector3();

        gameObject.transform.SetParent(transform, true);

        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "PIECES";
        spriteRenderer.sprite = pieceSprites[piece.color.ToString() + "_" + piece.pieceType.ToString()];

        pieceGameObjectMap.Add(piece, gameObject);
    }

    public void movePiece(Piece piece, Tile tile) {
        pieceGameObjectMap[piece].transform.position = tile.toVector3();
    }

    public void takes(Piece piece) {
        Destroy(pieceGameObjectMap[piece]);
        pieceGameObjectMap.Remove(piece);
    }

    void loadSprites() {
        pieceSprites = new Dictionary<string, Sprite>(12);
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Pieces/");

        foreach (Sprite sprite in sprites) {
            pieceSprites[sprite.name] = sprite;
        }
    }
}
