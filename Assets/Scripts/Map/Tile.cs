using System;
using UnityEngine;

public class Tile
{
    public enum TileType { Empty, Floor, FloorEnemy, Respawn, Reward, Boss};
    TileType oldType;

    TileType type = TileType.Empty;

    Action<Tile> CallBack_TileTypeChenged;

    Map map;
    int x;
    int y;
    public int health;
    public int atack;
    public int dificult;

    public TileType Type
    {
        get => type;

        set
        {
            type = value;
            //CallBack To change Tiles
            CallBack_TileTypeChenged(this);
        }
    }
    public int X { get => x; set => x = value; }
    public int Y { get => y; }


    public Tile(Map map, int x, int y)
    {
        this.map = map;
        this.x = x;
        this.y = y;

    }

    public void RegisterTileTypeChangedCallBack(Action<Tile> CallBack)
    {
        CallBack_TileTypeChenged += CallBack;
    }
    public void UnRegisterTileTypeChangedCallBack(Action<Tile> CallBack)
    {
        CallBack_TileTypeChenged -= CallBack;
    }
}