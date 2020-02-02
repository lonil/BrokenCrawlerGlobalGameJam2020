using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Hand
{
    static private Cards[] card;
    static Cards emptyCard;
    static int handSize = 5;
    static int i = 0;
    static bool[] cardState = new bool[] { false, false, false, false, false };
    static public int Handsize { get => handSize; }
    static public Cards[] Card { get => card; set
        {
            card = value;
        }
    }

    public static void ChangeCardState(int i, bool state)
    {
        cardState[i] = state;
        card[i].AddCallBack();
    }

    public static void init()
    {

        emptyCard = new Cards(Cards.CardType.Quad);
        Card = new Cards[] { emptyCard, emptyCard, emptyCard, emptyCard, emptyCard };
    }
}
