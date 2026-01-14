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

    [SerializeField] public float manaCoffee = 3;
    [SerializeField] private TMP_Text manaText;
    [SerializeField] private GameObject endTurnButton;

    public List<GameObject> encounters;
    public int currentEncounterIndex = 0;

    void Start()
    {
        currentState = GameStates.PlayerTurn;

        currentEncounterIndex = 0;

        enemyActions = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyActions>();
        playerActions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();
    }

    public void Update()
    {
        manaText.text = manaCoffee.ToString();

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

        if (currentEncounterIndex == 2 || currentEncounterIndex == 4)
        {
            Instantiate(encounters[currentEncounterIndex]);
        } else if (currentEncounterIndex == 1 || currentEncounterIndex == 3)
        {
            eventManager.RollForEvent();
        }

        if (currentEncounterIndex >= encounters.Count)
        {
            Debug.Log("All encounters completed!");
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
                currentState = GameStates.EnemyTurn;
                Debug.Log(currentState);
                StartCoroutine(TakeTimeForTurn());
                break;
            case GameStates.EnemyTurn:
                currentState = GameStates.PlayerTurn;
                endTurnButton.SetActive(true); //reactivate end turn button
                manaCoffee = 4; //reset mana
                Debug.Log(currentState);
                cardManager.DrawCards();
                break;
        }
    }
}
