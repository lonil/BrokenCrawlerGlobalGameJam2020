using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    Tile[,] tiles;
    Tile tresure;
    Tile boss;
    int level;

    public int Width { get => width; set => width = value; }
    int width;

    public int Hight { get => hight; set => hight = value; }
    public int Level
    {
        get => level; set
        {
            level = value;
            hight += (level * 6);
            width += (level * 8);
                }
    }

    public Tile[,] Tiles { get => tiles; }

    int hight;

    public Map(int width = 6, int hight = 6)
    {
        this.width = width;
        this.hight = hight;

        tiles = new Tile[width, hight];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < hight; y++)
            {
                tiles[x, y] = new Tile(this, x, y);
            }
        }
        Debug.Log("Map Created width" + (width * hight) + "tiles...");
    }

    public void RandomizeTiles()
    {
        tiles[0, 3].Type = Tile.TileType.Respawn;
        do
        {
            tresure = tiles[Random.Range(0, 5), Random.Range(0, 5)];
        }
        while (tresure.Type == Tile.TileType.Respawn);
        tresure.Type = Tile.TileType.Reward;

        do
        {
            boss = tiles[Random.Range(0, 5), Random.Range(0, 5)];
        }
        while (boss.Type == Tile.TileType.Respawn && boss.Type == Tile.TileType.Reward);
        boss.Type = Tile.TileType.Boss;

    }
    public Tile GetTileAt(int x, int y)
    {
        if (x > width || x < 0)
        {
            Debug.LogError("Tile (" + x + "," + y + ") is out of range.");
            return null;
        }
        return tiles[x, y];
    }

}
