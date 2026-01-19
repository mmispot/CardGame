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

    public bool doubleDmgBuff;

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

            if (CanUse())
            {
                UseCard();
            }
            currentSelectedCard = null;

            WaitForSeconds();
        }
        else
        {
            currentSelectedCard = null;
        }
    }

    private bool CanUse()
    {
        return gameManager.manaCoffee >= chosenDisplay.card.cardCost;
    }

    private void ModifyMana(int amount)
    {
        gameManager.manaCoffee += amount;
    }

    public void UseCard()
    {
        ModifyMana(-chosenDisplay.card.cardCost);

        if (chosenDisplay.card.firstType == CardBase.actionTypes.Attack)
        {
            if (chosenDisplay.card.cardName == "While Loop" && Random.Range(1, 3) == 2)
            {
                gameManager.damageCounter += 2; //do 2dmg instead

            }
            else
            {
                gameManager.damageCounter += chosenDisplay.card.actionValue;
            }
            chosenDisplay.VisualDeselect();
            DiscardChosenCard();
            return;
        }
        else if (chosenDisplay.card.firstType == CardBase.actionTypes.Defend)
        {
            gameManager.shieldCounter += chosenDisplay.card.actionValue;
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
            if (chosenDisplay.card.cardName == "Windows Update" && !doubleDmgBuff)
            {
                gameManager.damageCounter += chosenDisplay.card.actionValue;
                doubleDmgBuff = true; //next attack does double damage
                chosenDisplay.VisualDeselect();
                DiscardChosenCard();
                gameManager.currentState = GameStates.EnemyTurn;
                return;
            }
            else if (chosenDisplay.card.cardName == "Free Coffee")
            {
                ModifyMana(3); //gain 3 mana
                chosenDisplay.VisualDeselect();
                DiscardChosenCard();
                return;
            }
        }

        gameManager.ManaCheck();

    }
    public void DrawCards()
    {
        for (int i = 0; i < playerHand.Count; i++)
        {
            if (playerHand[i] == null)
            {
                int randomIndex = Random.Range(0, deck.Count);
                CardBase card = deck[randomIndex];

                deck.Remove(card);
                playerHand[i] = card;
                cardDisplay[i].card = card;
            }

            ReactivateCardDisplays();
        }
    }

    public void DiscardChosenCard()
    {
        currentSelectedCard.SetActive(false);
        discardPile.Add(chosenDisplay.card);
        int index = playerHand.IndexOf(chosenDisplay.card);
        playerHand[index] = null;

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
