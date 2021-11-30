using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileSpriteController : MonoBehaviour {

    public Sprite blackTile;
    public Sprite whiteTile;

    Dictionary<Tile, GameObject> tileGameObjectMap;

    // Start is called before the first frame update
    void Start() {
        // Instantiate our dictionary that tracks which GameObject is rendering which Tile data.
        tileGameObjectMap = new Dictionary<Tile, GameObject>(64);

        foreach (Tile tile in World.world.tiles) {
            // This creates a new GameObject and adds it to our scene.
            GameObject gameObjectTile = new GameObject("Tile_" + tile.X + "_" + tile.Y);

            gameObjectTile.transform.position = new Vector3(tile.X, tile.Y, 0);

            gameObjectTile.transform.SetParent(transform, true);

            // Add a Sprite Renderer
            // Add a default sprite for empty tiles.
            SpriteRenderer spriteRenderer = gameObjectTile.AddComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerName = "TILES";

            switch (tile.color) {
                case Color.BLACK:
                    spriteRenderer.sprite = blackTile;
                    break;

                case Color.WHITE:
                    spriteRenderer.sprite = whiteTile;
                    break;

                default:
                    Debug.LogError("Unknown tileType");
                    break;
            }

            // Add our tile/GO pair to the dictionary.
            tileGameObjectMap.Add(tile, gameObjectTile);
        }
    }
}
