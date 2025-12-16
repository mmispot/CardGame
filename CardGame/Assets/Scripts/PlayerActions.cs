using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameManager gameManager;
    public void Attack()
    {
        if (gameManager.currentState == GameStates.PlayerTurn)
        {
            Debug.Log("player attacked");
            gameManager.EndTurn();
        }
    }

    public void Defend()
    {
        if (gameManager.currentState == GameStates.PlayerTurn)
        {
            Debug.Log("player defended");
            gameManager.EndTurn();
        }
    }

    public void Heal()
    {
        if (gameManager.currentState == GameStates.PlayerTurn)
        {
            Debug.Log("player healed");
            gameManager.EndTurn();
        }
    }
}
