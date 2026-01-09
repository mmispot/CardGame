using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public CardDisplay chosenDisplay; 

    public EnemyActions enemyActions;
    public PlayerActions playerActions;

    public void Start()
    {
        playerActions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();
        DrawPlayerHand();
    }

    public void Update()
    {
        if (playerHand == null)
        {
            gameObject.SetActive(false);
        }
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
    public void ClearDeck()
    {
        playerHand.Clear();
    }

    public void SaveCard(GameObject cardToSelect)
    {
        currentSelectedCard = cardToSelect;

        //get card component of current selected card (chosenCard)
        chosenDisplay = currentSelectedCard.GetComponent<CardDisplay>();
        chosenDisplay.VisualSelected();
    }

    public void UseCard()
    {
        //use active card's action on player/enemy
        //check what type it is, then do function
        if (chosenDisplay.card.firstType == 0)
        {
            playerActions.Attack(chosenDisplay.card.actionValue);
        }

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
