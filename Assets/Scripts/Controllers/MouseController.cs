using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class MouseController : MonoBehaviour
{
    public static MouseController mouseController { get; protected set; }

    public GameObject moveOverlay;

    // The world-position of the mouse last frame.
    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    // The world-position start of our left-mouse drag operation
    Vector3 dragStartPosition;

    List<GameObject> movePreviewGameObjects;

    GameObject hoveredPreviewGameObject;

    bool canceled = false;

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
            return World.world.getTileAt(Mathf.FloorToInt(currFramePosition.x + 0.5f), Mathf.FloorToInt(currFramePosition.y + 0.5f));
        }
    }

    /// public Piece currentHoveredPiece ???

    // Use this for initialization
    void Start() {
        movePreviewGameObjects = new List<GameObject>();

        mouseController = this;

        hoveredPreviewGameObject = moveOverlay;
        hoveredPreviewGameObject.name = "HoveredPreviewGameObject";
    }

    // Update is called once per frame
    void Update() {
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

        UpdateCameraMovement();

        if (Input.GetMouseButtonDown(0) && (pieceToMove == null || pieceToMove.color == playerToMove) && currentHoveredTile != null && currentHoveredTile != pieceToMove.tile) {
            if (pieceToMove.getMovableTiles().Contains(currentHoveredTile)) {
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
        }

        if (Input.GetMouseButtonDown(0) && (pieceToMove == null || pieceToMove.color == playerToMove) && currentHoveredTile != null) {
            if (currentHoveredTile.piece != null) {
                pieceToMove = currentHoveredTile.piece;
            }
        }

        if (currentHoveredTile != null) {
            if(hoveredPreviewGameObject.activeSelf == false) {
                hoveredPreviewGameObject.SetActive(true);
            }
            hoveredPreviewGameObject.transform.position = currentHoveredTile.toVector3();
        }

        else {
            hoveredPreviewGameObject.SetActive(false);
        }

        // Save the mouse position from this frame
        // We don't use currFramePosition because we may have moved the camera.
        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
    }

    void UpdateCameraMovement() {
        // Handle screen panning
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2)) {   // Right or Middle Mouse Button

            Vector3 difference = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(difference);
        }

        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 25f);
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
            gameObject.transform.SetParent(transform, true);
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            movePreviewGameObjects.Add(gameObject);
        }
    }
}
