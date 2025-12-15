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
    public IEnumerator TakeTimeForTurn()
    {
        yield return new WaitForSeconds(2);
    }
    void Start()
    {
        currentState = GameStates.PlayerTurn;
    }
    public void ChangeTurn()
    {
        if (currentState == GameStates.EnemyTurn)
        {
            enemyActions.RandomiseTurn();
            Debug.Log(enemyActions.randomEnum);
            StartCoroutine(TakeTimeForTurn());
            currentState = GameStates.PlayerTurn;
            Debug.Log(currentState);
        }
        if (currentState == GameStates.PlayerTurn)
        {
            currentState = GameStates.EnemyTurn;
            Debug.Log(currentState);
        }
    }
}
