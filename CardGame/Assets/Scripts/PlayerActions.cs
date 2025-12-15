using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameManager gameManager;
    public void Attack()
    {
        if (gameManager.currentState == GameStates.PlayerTurn)
        {
            Debug.Log("player attacked");
            gameManager.ChangeTurn();
        }
    }

    public void Defend()
    {
        if (gameManager.currentState == GameStates.PlayerTurn)
        {
            Debug.Log("player defended");
            gameManager.ChangeTurn();
        }
    }

    public void Heal()
    {
        if (gameManager.currentState == GameStates.PlayerTurn)
        {
            Debug.Log("player healed");
            gameManager.ChangeTurn();
        }
    }
}
