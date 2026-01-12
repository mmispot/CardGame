using System.Collections;
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

    public float manaCoffee;
    [SerializeField] private TMP_Text manaText;
    [SerializeField] private GameObject endTurnButton;

    void Start()
    {
        currentState = GameStates.PlayerTurn;

        manaCoffee = 4;
    }

    public void Update()
    {
        manaText.text = manaCoffee.ToString();

        //remove button for ending turn when not players turn
        if (manaCoffee <= 0 && currentState == GameStates.PlayerTurn)
        {
            EndTurn();
            endTurnButton.SetActive(false);
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
