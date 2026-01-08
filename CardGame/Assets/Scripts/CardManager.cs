using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public PlayerActions playerScript;
    public CardDisplay[] cardDisplay;
    public CardDisplay cardDisplayScript;

    [SerializeField] private List<CardBase> allCards;

    public List<CardBase> playerHand;
    public List<CardBase> deck;

    public int maxHandSize = 5;

    public GameObject currentSelectedCard;

    public void Start()
    {
        DrawPlayerHand();
    }

    public void DrawPlayerHand()
    {
        playerHand = new List<CardBase>();
        for (int i = 0; i < maxHandSize; i++)
        {
            int randomIndex = Random.Range(0, allCards.Count);
            playerHand.Add(allCards[randomIndex]);
            cardDisplay[i].card = playerHand[i];
        }
    }
    public void SelectCard()
    {
        cardDisplayScript.CardSelected();
    }

    public void SaveCard(GameObject cardToSelect)
    {
        currentSelectedCard = cardToSelect;
    }    

    public void UseCard()
    {
        //use active card's action on player/enemy


        DiscardCard();
    }
    public void DrawCards()
    {
        if (playerHand.Count < maxHandSize)
        {
            
        }    
    }

    public void DiscardCard()
    {
        //remove used card from hand

    }
}
