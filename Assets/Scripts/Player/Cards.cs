using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cards
{
    Action<Cards> CallBack_TileTypeChenged;
    public enum CardType
    {
        Quad, RectangleV, RectangleH, LNormal, LMirrored, UpSideDownL, UpSideDownLMirrored, BigSquad,
    }
    public int health;
    public int atack;
    public int dificult;
    public int index;
    int reward;
    

    CardType type;
    public Cards(CardType type)
    {
        Type = type;
    }
    public CardType Type { get => type; set { type = value;
            
            switch (type)
            {
               
                case CardType.Quad:
                    health = RandomRange.Randomrange(1, 3);
                    atack = RandomRange.Randomrange(1, 3);
                    dificult = 2;
                    break;
                case CardType.RectangleV:

                    health = RandomRange.Randomrange(2, 4);
                    atack = RandomRange.Randomrange(1, 3);
                    dificult = 2;
                    break;
                case CardType.RectangleH:

                    health = RandomRange.Randomrange(2, 4);
                    atack = RandomRange.Randomrange(1, 3);
                    dificult = 2;
                    break;
                case CardType.LNormal:

                    health = RandomRange.Randomrange(3, 6);
                    atack = RandomRange.Randomrange(2, 4);
                    dificult = 3;
                    break;
                case CardType.LMirrored:

                    health = RandomRange.Randomrange(3, 6);
                    atack = RandomRange.Randomrange(2, 4);
                    dificult = 3;
                    break;
                case CardType.UpSideDownL:

                    health = RandomRange.Randomrange(3, 6);
                    atack = RandomRange.Randomrange(2, 4);
                    dificult = 3;
                    break;
                case CardType.UpSideDownLMirrored:

                    health = RandomRange.Randomrange(3, 6);
                    atack = RandomRange.Randomrange(2, 4);
                    dificult = 3;
                    break;
                case CardType.BigSquad:

                    health = RandomRange.Randomrange(5, 10);
                    atack = RandomRange.Randomrange(4, 6);

                    dificult = 3;
                    break;
                default:
                    
                    health = RandomRange.Randomrange(1, 3);
                    atack = RandomRange.Randomrange(1, 3);
                    break;
            }
            reward = (health + atack + dificult * 2) / 8;
        }
    }
    public void AddCallBack()
    {
        CallBack_TileTypeChenged(this);
    }
    public void RegisterTileTypeChangedCallBack(Action<Cards> CallBack)
    {
        CallBack_TileTypeChenged += CallBack;
    }
}
