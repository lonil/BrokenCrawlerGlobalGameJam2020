using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldController : MonoBehaviour
{
    static WorldController instance;

    [SerializeField] Sprite floorSprite;
    [SerializeField] Sprite respawnSprite;
    [SerializeField] Sprite bossSprite;
    [SerializeField] Sprite tresureSprite;
    [SerializeField] Sprite emptySprite;
    Map map;

    public Map Map { get => map; }
    public static WorldController Instance { get => instance; }

    public static bool TryGetInstance (out WorldController worldController)
    {
        worldController = null;
        if (instance == null || !instance.isActiveAndEnabled)
            return false;
        worldController = instance;
        return true;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
            Debug.Log("There should not be more then one WorldController instace at any time");
        instance = this;

        map = new Map();
        WorldCreator();

    }

    void OnTileTypeChanged(Tile tileInfo, GameObject tileObject)
    {
        switch (tileInfo.Type)
        {
            case Tile.TileType.Empty:  
                tileObject.GetComponent<SpriteRenderer>().sprite = emptySprite;
                break;
            case Tile.TileType.Floor:
                tileObject.GetComponent<SpriteRenderer>().sprite = floorSprite;
                break;
            case Tile.TileType.Respawn:
                tileObject.GetComponent<SpriteRenderer>().sprite = respawnSprite;
                tileObject.tag = "Respawn";
                break;
            case Tile.TileType.Reward:
                tileObject.GetComponent<SpriteRenderer>().sprite = tresureSprite;
                break;
            case Tile.TileType.Boss:
                tileObject.GetComponent<SpriteRenderer>().sprite = bossSprite;
                break;
            case Tile.TileType.FloorEnemy:
                tileObject.GetComponent<SpriteRenderer>().sprite = floorSprite;
                tileObject.tag = "FloorEnemy";
                break;
            default:
                tileObject.GetComponent<SpriteRenderer>().sprite = emptySprite;
                break;
        }
    }
    private void WorldCreator()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //Create a GameObject per Tile.
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Hight; y++)
            {
                Tile tileInfo = map.GetTileAt(x, y);
                GameObject tileObject = new GameObject();
                tileObject.name = "Tile_" + x + "_" + y;
                tileObject.transform.position = new Vector3(tileInfo.X, tileInfo.Y, 0);
                tileObject.transform.SetParent(this.transform, true);
                tileObject.AddComponent<SpriteRenderer>().sprite = emptySprite;

                tileInfo.RegisterTileTypeChangedCallBack((tile) => { OnTileTypeChanged(tile, tileObject); });

            }

        }
        map.RandomizeTiles();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public Tile GetTileAtWorldCoord(Vector3 coord)
    {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        return instance.Map.GetTileAt(x, y);
    }
}
