using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    [SerializeField] Sprite QuadSprite;
    [SerializeField] Sprite RectangleVSprite;
    [SerializeField] Sprite RectangleHSprite;
    [SerializeField] Sprite LNormalSprite;
    [SerializeField] Sprite LMirroredSprite;
    [SerializeField] Sprite UpSideDownLSprite;
    [SerializeField] Sprite UpSideDownLMirroredSprite;
    [SerializeField] Sprite BigSquadSprite;
    [SerializeField]DeckControllerObject deckOrder;
    [SerializeField] IntScriptableObject mouseChangeFunction;
    [SerializeField] IntScriptableObject[] handCardType;
    public GameObject[] cardHand;
    int usedButtom;
    int usedOld=-1;
    int z;

    
    
    Queue<Cards> deck;
    [SerializeField]MenuController menuController;

    public int UsedButtom { get => usedButtom; set
        { usedButtom = value; cardHand[value].SetActive(false); if (usedOld == -1) usedOld = usedButtom; if (++z > 2) { cardHand[usedOld].SetActive(true); cardHand[usedButtom].SetActive(true); DrawCard();usedButtom = -1; usedOld = -1; } }
    }

    private void Start()
    {
        
        
        int a = deckOrder.inicialized();
        deck = new Queue<Cards>();
        Cards[] c = new Cards[a];
        int i = 0;
        for (i = 0; i < a; i++)
        {
            c[i] = new Cards(deckOrder.Deck[i]);
            deck.Enqueue(c[i]);
        }

        Hand.init();
        DrawCard();

    }
    void OnHandTypeChanged(Cards cardInfo, GameObject handCard)
    {
       
        switch (cardInfo.Type)
        {
            
            case Cards.CardType.Quad:
                handCard.GetComponent<Image>().sprite = QuadSprite;
                handCardType[cardInfo.index].value = 0;
                break;
            case Cards.CardType.RectangleV:
                handCard.GetComponent<Image>().sprite = RectangleVSprite;
                handCardType[cardInfo.index].value = 1;
                break;
            case Cards.CardType.RectangleH:
                handCard.GetComponent<Image>().sprite = RectangleHSprite;
                handCardType[cardInfo.index].value = 2;
                break;
            case Cards.CardType.LNormal:
                handCard.GetComponent<Image>().sprite = LNormalSprite;
                handCardType[cardInfo.index].value = 3;
                break;
            case Cards.CardType.LMirrored:
                handCard.GetComponent<Image>().sprite = LMirroredSprite;
                handCardType[cardInfo.index].value = 4;
                break;
            case Cards.CardType.UpSideDownL:
                handCard.GetComponent<Image>().sprite = UpSideDownLSprite;
                handCardType[cardInfo.index].value = 5;
                break;
            case Cards.CardType.UpSideDownLMirrored:
                handCard.GetComponent<Image>().sprite = UpSideDownLMirroredSprite;
                handCardType[cardInfo.index].value = 6;
                break;
            case Cards.CardType.BigSquad:
                handCard.GetComponent<Image>().sprite = BigSquadSprite;
                handCardType[cardInfo.index].value = 7;
                break;
            default:
                break;
        }

    }
    public void DrawCard()
    {
        z = 0;
        if(deck.Count >= 5)
            for (int i = 0; i < Hand.Handsize; i++)
            {
                Cards temp = deck.Dequeue();
                temp.RegisterTileTypeChangedCallBack((Cards) => { OnHandTypeChanged(Cards, cardHand[i]); });
                temp.index = i;
                Hand.Card[i] = temp;
                Hand.ChangeCardState(i, true);
            }
        else
        {
            menuController.HideMenus();
            menuController.ShowMenu(1);
            string[] s;
            s = new string[2];
            s[0] = "you lose!";
            s[1] = "try Again?";
            menuController.ChangeText(s , 1); 
        }
    }
}
