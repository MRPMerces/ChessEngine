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

    Piece pieceToMove {
        get {
            return _pieceToMove;
        }
        set {
            if (value == _pieceToMove) {
                return;
            }

            if (value == null) {
                clearPreviews();
                _pieceToMove = null;
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
    }

    // Update is called once per frame
    void Update() {
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

        UpdateCameraMovement();

        if (Input.GetMouseButtonDown(0) && currentHoveredTile != null) {
            if (currentHoveredTile.piece != null) {
                pieceToMove = currentHoveredTile.piece;
            }
        }

        if (Input.GetMouseButtonDown(0) && pieceToMove != null && currentHoveredTile != null && currentHoveredTile != pieceToMove.tile) {
            if (pieceToMove.getMovableTiles().Contains(currentHoveredTile)) {
                PieceController.pieceController.movePiece(pieceToMove, currentHoveredTile);
                pieceToMove = null;
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

    public void dragInfrastructure() {

        // If we're over a UI element, then bail out from this.
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        // Clean up old drag previews
        while (movePreviewGameObjects.Count > 0) {
            SimplePool.Despawn(movePreviewGameObjects[0]);
            movePreviewGameObjects.RemoveAt(0);
        }

        // If the player presses escape. Quit the drag function.
        if (Input.GetKeyDown(KeyCode.Escape)) {
        }

        // quit Drag
        if (Input.GetMouseButton(1)) {
            dragStartPosition = currFramePosition;
            canceled = true;
            return;
        }

        // Start Drag
        if (Input.GetMouseButtonDown(0)) {
            dragStartPosition = currFramePosition;
            canceled = false;
        }

        int start_x = Mathf.FloorToInt(dragStartPosition.x + 0.5f);
        int start_y = Mathf.FloorToInt(dragStartPosition.y + 0.5f);
        int end_x = Mathf.FloorToInt(currFramePosition.x + 0.5f);
        int end_y = Mathf.FloorToInt(currFramePosition.y + 0.5f);

        // We may be dragging in the "wrong" direction, so flip things if needed.
        if (end_x < start_x) {
            int temp = end_x;
            end_x = start_x;
            start_x = temp;
        }
        if (end_y < start_y) {
            int temp = end_y;
            end_y = start_y;
            start_y = temp;
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
            gameObject.transform.SetParent(transform, true);
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            movePreviewGameObjects.Add(gameObject);
        }
    }
}
