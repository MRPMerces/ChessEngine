using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class MouseController : MonoBehaviour
{

    public GameObject moveOverlay;

    List<GameObject> movePreviewGameObjects;

    GameObject hoveredPreviewGameObject;

    private Piece _pieceToMove;

    Color playerToMove = Color.WHITE;

    Piece pieceToMove {
        get {
            return _pieceToMove;
        }
        set {
            if (value == null || value == _pieceToMove) {
                clearPreviews();
                _pieceToMove = null;
                return;
            }

            if (value == _pieceToMove) {
                return;
            }

            _pieceToMove = value;

            clearPreviews();
            foreach (Tile tile in value.getMovableTiles()) {
                createPreview(tile);
            }
        }
    }

    // The currently hovered tile
    // NOTE migth be null!
    public Tile currentHoveredTile {
        get {
            return World.world.getTileAt(Mathf.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + 0.5f), Mathf.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y + 0.5f));
        }
    }

    public Piece currentHoveredPiece {
        get {
            if(currentHoveredTile == null) {
                return null;
            }

            return currentHoveredTile.piece;
        }
    }

    // Use this for initialization
    void Start() {
        movePreviewGameObjects = new List<GameObject>();

        hoveredPreviewGameObject = SimplePool.Spawn(moveOverlay, new Vector3(0, 0, 0), Quaternion.identity);
        hoveredPreviewGameObject.name = "HoveredTilePreviewGameObject";
        hoveredPreviewGameObject.transform.position = new Vector3(0, 0, 0);
        hoveredPreviewGameObject.transform.SetParent(transform, true);
        hoveredPreviewGameObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
        hoveredPreviewGameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0) && currentHoveredTile != null) {
            if (pieceToMove != null && (currentHoveredPiece == null || currentHoveredPiece.color != playerToMove) && pieceToMove.getMovableTiles().Contains(currentHoveredTile)) {

                PieceController.pieceController.movePiece(pieceToMove, currentHoveredTile);
                pieceToMove = null;

                switch (playerToMove) {
                    case Color.BLACK:
                        playerToMove = Color.WHITE;
                        break;

                    case Color.WHITE:
                        playerToMove = Color.BLACK;
                        break;

                    default:
                        Debug.LogError("Unknown color");
                        break;
                }
            }

            if (currentHoveredPiece != null && currentHoveredTile.piece.color == playerToMove) {
                pieceToMove = currentHoveredTile.piece;
            }

            if (pieceToMove != null && currentHoveredPiece == null) {
                pieceToMove = null;
            }
        }

        if (currentHoveredTile != null) {
            hoveredPreviewGameObject.SetActive(true);
            hoveredPreviewGameObject.transform.position = currentHoveredTile.toVector3();
        }

        else {
            hoveredPreviewGameObject.SetActive(false);
        }
    }

    void clearPreviews() {
        // Clean up old drag previews
        while (movePreviewGameObjects.Count > 0) {
            SimplePool.Despawn(movePreviewGameObjects[0]);
            movePreviewGameObjects.RemoveAt(0);
        }
    }

    void createPreview(Tile tile) {
        if (tile != null) {
            // Display the building hint on top of this tile position
            GameObject gameObject = SimplePool.Spawn(moveOverlay, new Vector3(tile.X, tile.Y, 0), Quaternion.identity);
            gameObject.transform.name = "MoveableTile: " + movePreviewGameObjects.Count();
            gameObject.transform.SetParent(transform, true);
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            movePreviewGameObjects.Add(gameObject);
        }
    }
}
