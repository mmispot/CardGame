using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    public GameManager gameManager;
    public int randomEnum;
    
    public void RandomiseTurn()
    {
        randomEnum = Random.Range(1, 3);
    }

    public void ChooseAction()
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
            Debug.Log("enemy attacked");
            gameManager.EndTurn();
        }
    }

    public void Defend()
    {
        if (gameManager.currentState == GameStates.EnemyTurn)
        {
            Debug.Log("enemy defended");
            gameManager.EndTurn();
        }
    }

    public void Heal()
    {
        if (gameManager.currentState == GameStates.EnemyTurn)
        {
            Debug.Log("enemy heal");
            gameManager.EndTurn();
        }
    }
}
