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

    public int maxHandSize = 5;

    public GameObject currentSelectedCard;
    public CardDisplay chosenDisplay; 

    public EnemyActions enemyActions;
    public PlayerActions playerActions;
    public GameManager gameManager;

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
        currentSelectedCard = cardToSelect;

        chosenDisplay = currentSelectedCard.GetComponent<CardDisplay>();
        chosenDisplay.VisualSelected();

        UseCard();
    }

    public void UseCard()
    {
        if (chosenDisplay.card.firstType == CardBase.actionTypes.Attack)
        {
            playerActions.Attack(chosenDisplay.card.actionValue);
            WaitForSeconds();
            chosenDisplay.VisualDeselect();
            return;
        }
        else if (chosenDisplay.card.firstType == CardBase.actionTypes.Defend)
        {
            playerActions.Defend(chosenDisplay.card.actionValue);
            WaitForSeconds();
            chosenDisplay.VisualDeselect();
            return;
        }
        else if (chosenDisplay.card.firstType == CardBase.actionTypes.Heal)
        {
            playerActions.Heal(chosenDisplay.card.actionValue);
            WaitForSeconds();
            chosenDisplay.VisualDeselect();
            return;
        }

    }
    public void DrawCards()
    {
        if (playerHand.Count < maxHandSize)
        {
            //draw cards until playerHand is full

        }    
    }
}
