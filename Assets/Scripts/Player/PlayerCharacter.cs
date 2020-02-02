using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public int health, atack, precision,die, reward;
    int x, y;
    int vertical;
    int horizontal;
    public Sprite playerSprite;
    WorldController world;
    Map map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
    Tile check;
    MenuController menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (!WorldController.TryGetInstance(out WorldController controllerReference))
            return;
        world = controllerReference;
        player = new GameObject();
        player.AddComponent<SpriteRenderer>().sprite = playerSprite;
        player.tag = "player";
        player.transform.position = new Vector3 (0f,3f,player.transform.position.z);
    }

    void Update()
    {
        if (health < 0)
        {
            Destroy(player);
        }
        if (reward >= 8)
        {
            RollReward();
            reward -= 8;
        }
        if(Input.GetButtonDown("Vertical"))
            vertical = (int)(Input.GetAxisRaw("Vertical"));
        if(Input.GetButtonDown("Horizontal"))
            horizontal = (int)(Input.GetAxisRaw("Horizontal"));

        if (vertical != 0)
            horizontal = 0;
        if (horizontal != 0 || vertical != 0)
        {
            Vector3 to = new Vector3(player.transform.position.x + horizontal, player.transform.position.y + vertical, 0f);
            check = world.GetTileAtWorldCoord(to);
        }
            if (check != null)
        if (check.Type != Tile.TileType.Empty)
        {
            if (check.Type != Tile.TileType.FloorEnemy && check.Type != Tile.TileType.Boss)
                if (check.Type == Tile.TileType.Reward)
                {
                    RollReward();
                        check = null;
                }
                else
                {
                    player.transform.position += new Vector3(horizontal, vertical);
                        check = null;
                    }
            else
            {
                if (check.dificult <= Random.Range(precision, precision + die))
                {
                    Debug.Log(check.health + "-" + atack);
                    check.health -= atack;
                    Debug.Log(check.health);
                        if (check.health <= 0)
                        {
                            check.Type = Tile.TileType.Floor;
                            reward = +check.atack + check.health + check.dificult;
                        }
                    check = null;
                }
                else
                {
                    health -= check.atack;
                    if (health <= 0)
                    {
                        menuController.HideMenus();
                        menuController.ShowMenu(1);
                        string[] s;
                        s = new string[2];
                        s[0] = "you lose!";
                        s[1] = "try Again?";
                        menuController.ChangeText(s, 1);
                    }
                        check = null;
                    }
            }
            }
            else { check = null; }
            vertical = 0; horizontal = 0;
    }
    public void RollReward()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                Debug.Log("AtackUp");
                atack += Random.Range(1 + (1 * world.Map.Level), 2 + (1 * world.Map.Level));
                break;
            case 1:
                Debug.Log("AtackUp");
                health += Random.Range(2 + (1 * world.Map.Level), 4 + (2 * world.Map.Level));
                break;
            case 2:
                Debug.Log("AtackUp");
                atack += Random.Range(0 + (1 * world.Map.Level), 1 + (1 * world.Map.Level));
                health += Random.Range(1 + (1 * world.Map.Level), 2 + (2 * world.Map.Level));
                break;
            case 3:
                precision += 1 + world.Map.Level;
                break;
        }
    }

}
