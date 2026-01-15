using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public enum GameStates
{
    PlayerTurn,
    EnemyTurn
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
    }

    public void Update()
    {
        manaText.text = manaCoffee.ToString();
        enemyName.text = enemyActions.enemyName;

        if (manaCoffee <= 0 && currentState == GameStates.PlayerTurn)
        {
            EndTurn();
            endTurnButton.SetActive(false);
        }

    }

    public void NextEncounter()
    {
        currentEncounterIndex += 2;
        //currentEncounterIndex++;

        enemyName.text = enemyActions.enemyName;

        if (currentEncounterIndex == 2 || currentEncounterIndex == 4)
        {
            Instantiate(encounters[currentEncounterIndex]);
        } else if (currentEncounterIndex == 1 || currentEncounterIndex == 3)
        {
            eventManager.DoEvent(Random.Range(0, 4));
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
                currentState = GameStates.EnemyTurn;
                StartCoroutine(TakeTimeForTurn());
                break;
            case GameStates.EnemyTurn:
                currentState = GameStates.PlayerTurn;
                endTurnButton.SetActive(true); //reactivate end turn button
                manaCoffee = maxMana; //reset mana
                cardManager.DrawCards();
                break;
        }
    }
}
