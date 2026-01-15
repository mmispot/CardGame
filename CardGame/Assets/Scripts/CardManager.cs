using NUnit.Framework;
using System.Collections;
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
    public List<CardBase> discardPile;

    public int maxHandSize = 5;

    public GameObject currentSelectedCard;
    public GameObject removedCard;
    public CardDisplay chosenDisplay; 

    public EnemyActions enemyActions;
    public PlayerActions playerActions;
    public GameManager gameManager;

    public void Start()
    {
        playerActions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();
        DrawPlayerHand();

        discardPile = new List<CardBase>();
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(1);
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
        if (currentSelectedCard == null)
        {
            currentSelectedCard = cardToSelect;

            chosenDisplay = currentSelectedCard.GetComponent<CardDisplay>();

            CheckManaCost();
            currentSelectedCard = null;

            WaitForSeconds();
        }
        else
        {
            currentSelectedCard = null;
        }
    }

    public void CheckManaCost()
    {
        if (gameManager.manaCoffee >= chosenDisplay.card.cardCost)
        {
            gameManager.manaCoffee = gameManager.manaCoffee - chosenDisplay.card.cardCost;
            UseCard();
        }
        else
        {
            Debug.Log("Not enough mana to play this card");
        }
    }

    public void UseCard()
    {

        if (chosenDisplay.card.firstType == CardBase.actionTypes.Attack )
        {
            gameManager.damageCounter += chosenDisplay.card.actionValue;
            //playerActions.Attack(chosenDisplay.card.actionValue);
            chosenDisplay.VisualDeselect();
            DiscardChosenCard();
            return;
        }
        else if (chosenDisplay.card.firstType == CardBase.actionTypes.Defend)
        {
            gameManager.shieldCounter += chosenDisplay.card.actionValue;
            //playerActions.Defend(chosenDisplay.card.actionValue);
            chosenDisplay.VisualDeselect();
            DiscardChosenCard();
            return;
        }
        else if (chosenDisplay.card.firstType == CardBase.actionTypes.Heal)
        {
            playerActions.Heal(chosenDisplay.card.actionValue);
            chosenDisplay.VisualDeselect();
            DiscardChosenCard();
            return;
        }
        else if (chosenDisplay.card.firstType == CardBase.actionTypes.Buff || chosenDisplay.card.firstType == CardBase.actionTypes.Debuff)
        {
            if (chosenDisplay.card.cardName == "Windows Update")
            {
                gameManager.damageCounter += chosenDisplay.card.actionValue;
                gameManager.damageCounter *= 2; //double damage this turn
                chosenDisplay.VisualDeselect();
                DiscardChosenCard();
                gameManager.currentState = GameStates.EnemyTurn;
                return;
            }
            else if (chosenDisplay.card.cardName == "Free Coffee")
            {
                gameManager.manaCoffee = +3; //gain 3 mana
                chosenDisplay.VisualDeselect();
                DiscardChosenCard();
                return;
            }
        }

    }
    public void DrawCards()
    {
        ReactivateCardDisplays();

        for (int i = playerHand.Count; i < maxHandSize; i++)
        {
            int randomIndex = Random.Range(0, deck.Count);
            playerHand.Add(deck[randomIndex]);
            deck.Remove(deck[randomIndex]);
            cardDisplay[i].card = playerHand[i];
        } 
    }

    public void DiscardChosenCard()
    {
        currentSelectedCard.SetActive(false);
        discardPile.Add(chosenDisplay.card);
        playerHand.Remove(chosenDisplay.card);
    }

    public void ReactivateCardDisplays()
    {
        for (int i = 0; i < cardDisplay.Length; i++)
        {
            if (cardDisplay[i] != null)
            {
                cardDisplay[i].gameObject.SetActive(true);
            }
        }
    }
}
