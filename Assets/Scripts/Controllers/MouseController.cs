using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour {

    public GameObject target;
    List<GameObject> dragPreview;
    [SerializeField] IntScriptableObject[] handCardType;
    [SerializeField] HandController handController;
    bool used;

    //Position of the mouse last check and current check
    Vector3 lastFrameposition;
    Vector3 currFramePosition;
    int buttomInUse;

    //position of the start of the mouse drag
    Vector3 dragStart;
    Tile.TileType tileTypeTo;
    Cards.CardType CardTypeTo;
    WorldController worldController;

    void Start()
    {
        dragPreview = new List<GameObject>();
        SimplePool.Preload(target, 500);
        if (!WorldController.TryGetInstance(out WorldController controllerReference))
            return;
        worldController = controllerReference;
    }

    // Update is called once per frame
    void Update(){

        //corrent position of the mouse
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0f;

        //UpdateTargetPosition();
        UpdateDragging();
        UpdateCamera();

        
    }

    
    void UpdateTargetPosition()
    {
        Tile tileUnderMouse = worldController.GetTileAtWorldCoord(currFramePosition);
        if (tileUnderMouse != null)
        {
            target.SetActive(true);
            Vector3 targetPosition = new Vector3(tileUnderMouse.X, tileUnderMouse.Y, 0f);
            target.transform.position = targetPosition;
        }
        else
        {
            target.SetActive(false);
        }
    } 

    //Responsable to Update the position of the endDrag e startDrag
    void UpdateDragging()
    {
        //Selection
            if (EventSystem.current.IsPointerOverGameObject())
                return;

        //Start Selection
        
        if (Input.GetMouseButtonDown(0))
        {
                dragStart = currFramePosition;
            
        }

        //Round and normalize Start position and end position.
        int startX = Mathf.FloorToInt(dragStart.x);
        int endX = Mathf.FloorToInt(currFramePosition.x);
        int startY = Mathf.FloorToInt(dragStart.y);
        int endY = Mathf.FloorToInt(currFramePosition.y);
        if (endX < startX)
        {
            int temp = endX;
            endX = startX;
            startX = temp;
        }
        if (endY < startY)
        {
            int temp = endY;
            endY = startY;
            startY = temp;
        }
        
        //while (dragPreview.Count > 0)
        //{
        //    GameObject go = dragPreview[0];
        //    dragPreview.RemoveAt(0);
        //    SimplePool.Despawn(go);
        //}


        //Selection Preview
        //if (Input.GetMouseButton(0))
        //{
        //    for (int x = startX; x <= endX; x++)
        //    {
        //        {
        //            for (int y = startY; y <= endY; y++)
        //            {
        //                //GameObject go = SimplePool.Spawn(target, new Vector3(x, y, 0), Quaternion.identity);
        //                go.transform.SetParent(this.transform, true);
        //                dragPreview.Add(go);
        //            }
        //        }

        //    }
        //}

        //End Selection
        if (Input.GetMouseButtonUp(0))
        {
            int x = endX, y = endY;
            Tile t = worldController.Map.GetTileAt(x, y);
            switch (CardTypeTo)
            {
                case Cards.CardType.Quad:
                    
                    if (check(t))
                    {

                        t.atack = Hand.Card[buttomInUse].atack;
                        t.dificult = Hand.Card[buttomInUse].dificult;
                        t.health = Hand.Card[buttomInUse].health;
                        t.Type = tileTypeTo;
                        used = true;
                    }
                    break;
                case Cards.CardType.RectangleV:
                    
                    
                    if (check(t))
                    {
                        

                        Tile t2 = worldController.Map.GetTileAt(x, y + 1);
                        if (check(t2))
                            t.atack = Hand.Card[buttomInUse].atack;
                        t.dificult = Hand.Card[buttomInUse].dificult;
                        t.health = Hand.Card[buttomInUse].health;
                        t.Type = tileTypeTo;
                        t2.Type = tileTypeTo;

                            used = true;
                    }
                    break;
                case Cards.CardType.RectangleH:
                    
                    
                    if (check(t))
                    {
                        Tile t2 = worldController.Map.GetTileAt(x + 1, y);
                        t.atack = Hand.Card[buttomInUse].atack;
                        t.dificult = Hand.Card[buttomInUse].dificult;
                        t.health = Hand.Card[buttomInUse].health;
                        t.Type = tileTypeTo;
                        t2.Type = tileTypeTo;
                            used = true;
                    }
                    break;
                case Cards.CardType.LNormal:
                    
                    
                    if (check(t))
                    {
                        Tile t2 = worldController.Map.GetTileAt(x + 1, y);
                        Tile t3 = worldController.Map.GetTileAt(x , y + 1);
                        if (check(t2) && check(t3))
                        {
                            t.atack = Hand.Card[buttomInUse].atack;
                            t.dificult = Hand.Card[buttomInUse].dificult;
                            t.health = Hand.Card[buttomInUse].health;
                            t.Type = tileTypeTo;
                            t2.Type = tileTypeTo;
                            t3.Type = tileTypeTo;
                            used = true;
                        }
                    }
                    break;
                case Cards.CardType.LMirrored:
                    
                    
                    if (check(t))
                    {
                        Tile t2 = worldController.Map.GetTileAt(x + 1, y);
                        Tile t3 = worldController.Map.GetTileAt(x, y + 1);
                        if (check(t2) && check(t3))
                        {
                            t.atack = Hand.Card[buttomInUse].atack;
                            t.dificult = Hand.Card[buttomInUse].dificult;
                            t.health = Hand.Card[buttomInUse].health;
                            t.Type = tileTypeTo;
                            t2.Type = tileTypeTo;
                            t3.Type = tileTypeTo;
                            used = true;
                        }
                    }
                    break;
                case Cards.CardType.UpSideDownL:
                    
                    
                    if (check(t))
                    {
                        Tile t2 = worldController.Map.GetTileAt(x + 1, y);
                        Tile t3 = worldController.Map.GetTileAt(x+1, y + 1);
                        if (check(t2) && check(t3))
                        {
                            t.atack = Hand.Card[buttomInUse].atack;
                            t.dificult = Hand.Card[buttomInUse].dificult;
                            t.health = Hand.Card[buttomInUse].health;
                            t.Type = tileTypeTo;
                            t2.Type = tileTypeTo;
                            t3.Type = tileTypeTo;
                            used = true;
                        }
                    }
                    break;
                case Cards.CardType.UpSideDownLMirrored:
                    
                   
                    if (check(t))
                    {
                        Tile t2 = worldController.Map.GetTileAt(x + 1, y+1);
                        Tile t3 = worldController.Map.GetTileAt(x, y + 1);
                        if (check(t2) && check(t3))
                        {
                            t.atack = Hand.Card[buttomInUse].atack;
                            t.dificult = Hand.Card[buttomInUse].dificult;
                            t.health = Hand.Card[buttomInUse].health;
                            t.Type = tileTypeTo;
                            t2.Type = tileTypeTo;
                            t3.Type = tileTypeTo;
                            used = true;

                        }
                    }
                    break;
                case Cards.CardType.BigSquad:
                    
                    
                    if (check(t))
                    {
                        Tile t2 = worldController.Map.GetTileAt(x + 1, y);
                        Tile t3 = worldController.Map.GetTileAt(x, y + 1);
                        Tile t4 = worldController.Map.GetTileAt(x + 1, y + 1);
                        if (check(t2) && check(t3) && check(t4))
                        {
                            t.atack = Hand.Card[buttomInUse].atack;
                            t.dificult = Hand.Card[buttomInUse].dificult;
                            t.health = Hand.Card[buttomInUse].health;
                            t.Type = tileTypeTo;
                            t2.Type = tileTypeTo;
                            t3.Type = tileTypeTo;
                            t3.Type = tileTypeTo;
                            used = true;
                        }
                    }
                    break;
                default:
                    break;
            }
            handController.UsedButtom = buttomInUse;
        }

    }
    //Responsable to Controll the camera
    void UpdateCamera()
    {
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            Vector3 diff = lastFrameposition - currFramePosition;
            Camera.main.transform.Translate(diff);
        }
        lastFrameposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFrameposition.z = 0f;
        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 20f);
    }
    public void SetMouseMode(int i)
    {
        switch (handCardType[i].value)
        {
            case 0:
                tileTypeTo = Tile.TileType.FloorEnemy;
                CardTypeTo = Cards.CardType.Quad;
                used = false;
                break;
            case 1:
                tileTypeTo = Tile.TileType.FloorEnemy;
                CardTypeTo = Cards.CardType.RectangleV;
                used = false;
                break;
            case 2:
                tileTypeTo = Tile.TileType.FloorEnemy;
                CardTypeTo = Cards.CardType.RectangleH;
                used = false;
                break;
            case 3:
                tileTypeTo = Tile.TileType.FloorEnemy;
                CardTypeTo = Cards.CardType.LNormal;
                used = false;
                break;
            case 4:
                tileTypeTo = Tile.TileType.FloorEnemy;
                CardTypeTo = Cards.CardType.LMirrored;
                used = false;
                break;
            case 5:
                tileTypeTo = Tile.TileType.FloorEnemy;
                CardTypeTo = Cards.CardType.UpSideDownL;
                used = false;
                break;
            case 6:
                tileTypeTo = Tile.TileType.FloorEnemy;
                CardTypeTo = Cards.CardType.UpSideDownLMirrored;
                used = false;
                break;
            case 7:
                tileTypeTo = Tile.TileType.FloorEnemy;
                CardTypeTo = Cards.CardType.BigSquad;
                used = false;
                break;
            default:
                tileTypeTo = Tile.TileType.FloorEnemy;
                CardTypeTo = Cards.CardType.RectangleV;
                break;
        }
        buttomInUse = i;
    }
    bool check(Tile t)
    {
        return (!used &&t != null && t.Type != Tile.TileType.Floor && t.Type != Tile.TileType.Respawn && t.Type != Tile.TileType.FloorEnemy);
    }
}


