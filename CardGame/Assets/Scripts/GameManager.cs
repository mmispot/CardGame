using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public SceneManager sceneManager;

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

    public int pickEvent = -1;

    void Start()
    {

        currentEncounterIndex = 0;

        enemyActions = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyActions>();
        playerActions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();

        maxMana = 3;
        manaCoffee = maxMana;

        damageCounter = 0;
        shieldCounter = 0;

        eventTime = false;

        StartPlayerTurn();
    }

    public void Update()
    {
        manaText.text = manaCoffee.ToString();

        enemyName.text = enemyActions.enemyName;
    }

    public void WinGame()
    {
        sceneManager.WinGame();
    }

    public void LoseGame()
    {
        sceneManager.LoseGame();
    }

    public void ManaCheck()
    {
        if (manaCoffee <= 0 && !eventTime)
        {
            SwitchTurn(GameStates.EnemyTurn);
        }
    }

    public void NextEncounter()
    {
        currentEncounterIndex++;

        if (currentEncounterIndex == 2 || currentEncounterIndex == 4)
        {
            Instantiate(encounters[currentEncounterIndex]);
            enemyActions.enemyName = encounters[currentEncounterIndex].name;
            enemyName.text = enemyActions.enemyName;
            Debug.Log(enemyActions.enemyName);

        } else if (currentEncounterIndex == 1 || currentEncounterIndex == 3)
        {
            SwitchTurn(GameStates.EventTime);
        }

        if (currentEncounterIndex >= encounters.Count)
        {
            Debug.Log("All encounters completed!");
            WinGame();
            return;
        }
    }

    public IEnumerator TakeTimeForTurn()
    {
        yield return new WaitForSeconds(3);
        enemyActions.DoAction();
    }

    public void SwitchTurn(GameStates nextTurn)
    {
        currentState = nextTurn;

        switch (currentState)
        {
            case GameStates.PlayerTurn:
                StartPlayerTurn();
                break;

            case GameStates.EnemyTurn:
                StartEnemyTurn();
                break;

            case GameStates.EventTime:
                StartEvent();
                break;
        }
    }

    public void SwitchTurnBtn()
    {
        SwitchTurn(GameStates.EnemyTurn);
    }

    public void StartPlayerTurn()
    {
        manaCoffee = maxMana;
        endTurnButton.SetActive(true);
        cardManager.DrawCards();
    }

    public void StartEnemyTurn()
    {
        playerActions.Attack(damageCounter);
        playerActions.Defend(shieldCounter);
        damageCounter = 0;
        shieldCounter = 0;

        endTurnButton.SetActive(false);
        StartCoroutine(TakeTimeForTurn());
    }

    public void StartEvent()
    {
        endTurnButton.SetActive(false);
        eventManager.DoEvent(Random.Range(0, eventManager.eventOptions.Length));
    }
}
