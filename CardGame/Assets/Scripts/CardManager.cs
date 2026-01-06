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

    public int maxHandSize = 4;

    public void Start()
    {
        DrawPlayerHand();
    }

    public void DrawPlayerHand()
    {
        deck = new List<CardBase>();
        for (int i = 0; i < maxHandSize; i++)
        {
            int randomIndex = Random.Range(0, allCards.Count);
            deck.Add(allCards[randomIndex]);
            cardDisplay[i].card = deck[i];
        }
    }
    public void SelectCard()
    {
        //clicked card
        //transform up slightly
        // set as active card to be used in variable

        this.transform.up += new Vector3(0, 0.2f, 0);
        Debug.Log("card selected");
    }
    public void UseCard()
    {
        //use active card's action on player/enemy

        DiscardCard();
    }
    public void DrawCards()
    {
        //if hand size < max hand size draw card from deck to hand
    }

    public void DiscardCard()
    {
        //remove used card from hand
    }
}
