using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public enum GameStates
{
    PlayerTurn,
    EnemyTurn,
    EventTime
}
public class GameManager : MonoBehaviour
{
    public PlayerActions playerActions;
    public EnemyActions enemyActions;

    public GameStates currentState;
    public CardManager cardManager;
    public EventManager eventManager;

    [SerializeField] public float manaCoffee;
    [SerializeField] public float maxMana;
    [SerializeField] private TMP_Text manaText;
    [SerializeField] private GameObject endTurnButton;
    [SerializeField] private TMP_Text enemyName;

    public List<GameObject> encounters;
    public int currentEncounterIndex = 0;

    public int damageCounter;
    public int shieldCounter;

    public bool eventTime;

    void Start()
    {
        currentState = GameStates.PlayerTurn;

        currentEncounterIndex = 0;

        enemyActions = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyActions>();
        playerActions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();

        maxMana = 3;
        manaCoffee = maxMana;

        damageCounter = 0;
        shieldCounter = 0;

        eventTime = false;
    }

    public void Update()
    {
        manaText.text = manaCoffee.ToString();

        enemyName.text = enemyActions.enemyName;

        if ((manaCoffee <= 0 || enemyActions.isDefeated) && currentState == GameStates.PlayerTurn)
        {
            EndTurn();
            endTurnButton.SetActive(false);
        }

    }

    public void NextEncounter()
    {
        //currentEncounterIndex += 2;
        currentEncounterIndex++;

        if (currentEncounterIndex == 2 || currentEncounterIndex == 4)
        {
            Instantiate(encounters[currentEncounterIndex]);
            enemyActions.enemyName = encounters[currentEncounterIndex].name;
            enemyName.text = enemyActions.enemyName;
            Debug.Log(enemyActions.enemyName);

        } else if (currentEncounterIndex == 1 || currentEncounterIndex == 3)
        {
            // calculate event ID
            eventTime = true;
        }

        if (currentEncounterIndex >= encounters.Count)
        {
            Debug.Log("All encounters completed!");
            //win screen
            return;
        }
    }

    public IEnumerator TakeTimeForTurn()
    {
        yield return new WaitForSeconds(3);
        enemyActions.DoAction();
        currentState = GameStates.PlayerTurn;
    }

    public void EndTurn()
    {
        endTurnButton.SetActive(false);

        switch (currentState)
        {
            case GameStates.PlayerTurn:

                if (cardManager.doubleDmgBuff)
                {
                    damageCounter *= 2; //double damage
                }
                playerActions.Attack(damageCounter);
                playerActions.Defend(shieldCounter);
                damageCounter = 0;
                shieldCounter = 0;
                cardManager.doubleDmgBuff = false; //reset buff

                if (eventTime)
                {

                    currentState = GameStates.EventTime;
                    eventManager.DoEvent(0); //for testing purposes, always do first event
                }
                else
                {
                    currentState = GameStates.EnemyTurn;
                    StartCoroutine(TakeTimeForTurn());
                }
                break;
            case GameStates.EnemyTurn:
                currentState = GameStates.PlayerTurn;
                endTurnButton.SetActive(true); //reactivate end turn button
                manaCoffee = maxMana; //reset mana
                cardManager.DrawCards();
                break;
            case GameStates.EventTime:
                endTurnButton.SetActive(true); //reactivate end turn button
                currentState = GameStates.PlayerTurn;
                eventTime = false;
                break;

        }
    }
}
