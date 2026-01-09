using System.Collections;
using UnityEngine;

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

    void Start()
    {
        currentState = GameStates.PlayerTurn;
    }
    public IEnumerator TakeTimeForTurn()
    {
        yield return new WaitForSeconds(3);
        enemyActions.DoAction();
    }

    public void EndTurn()
    {
        switch (currentState)
        {
            case GameStates.PlayerTurn:
                cardManager.ClearDeck();
                currentState = GameStates.EnemyTurn;
                Debug.Log(currentState);
                StartCoroutine(TakeTimeForTurn());
                break;
            case GameStates.EnemyTurn:
                cardManager.DrawPlayerHand();
                currentState = GameStates.PlayerTurn;
                Debug.Log(currentState);
                break;
        }
    }
}
