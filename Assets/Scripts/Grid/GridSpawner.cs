using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject tilePrefab;
    public float tileSize = 1f;

    [HideInInspector]
    public GameObject[,] tiles; // Store all tiles for easy access

    public bool gridReady = false;  // <-- Add this flag

    void Start()
    {
        tiles = new GameObject[width, height];
        GenerateGrid();
        gridReady = true; // <-- Set flag here after grid generation finishes
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 position = new Vector3(x * tileSize, 0, z * tileSize);

                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
                tile.name = $"Tile_{x}_{z}";

                tiles[x, z] = tile;

                Tile tileScript = tile.GetComponent<Tile>();
                if (tileScript != null)
                {
                    tileScript.gridX = x;
                    tileScript.gridZ = z;
                }
            }
        }
    }
}



