using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class DeckControllerObject : ScriptableObject
{
    public int[] type;
    private List<Cards.CardType> deck;
    public int numOfCards = 0;

    public List<Cards.CardType> Deck { get { Debug.Log(deck); return deck; } set => deck = value; }

    public List<Cards.CardType> GenerateCardDeck()
    {
    int loops = 0;
        List<Cards.CardType> deck = new List<Cards.CardType>();
        foreach (var c in (Cards.CardType[])Enum.GetValues(typeof(Cards.CardType)))
        {
            for (int i = 0; i < type[loops]; i++)
            {
                deck.Add(c);
                numOfCards = +1;
            }
            loops += 1; 
        }
        return deck; 
    }
    public int inicialized()
    {
        deck = GenerateCardDeck();
        return deck.Count;
    }

}
