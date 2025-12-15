using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    public GameManager gameManager;
    public int randomEnum = Random.Range(1, 3);
    
    public void RandomiseTurn()
    {

        if (randomEnum == 1)
        {
            Attack();
        }
        else if (randomEnum == 2)
        {
            Defend();
        }
        else if (randomEnum == 3) 
        {
            Heal();
        }
    }

    public void Attack()
    {
        if (gameManager.currentState == GameStates.EnemyTurn)
        {
            Debug.Log("player attacked");
            gameManager.ChangeTurn();
        }
    }

    public void Defend()
    {
        if (gameManager.currentState == GameStates.EnemyTurn)
        {
            Debug.Log("player defended");
            gameManager.ChangeTurn();
        }
    }

    public void Heal()
    {
        if (gameManager.currentState == GameStates.EnemyTurn)
        {
            Debug.Log("player heal");
            gameManager.ChangeTurn();
        }
    }
}
