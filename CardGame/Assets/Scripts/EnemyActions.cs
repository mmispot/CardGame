using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    public GameManager gameManager;

    private int enemyHealth;
    private int intelligenceStat; //if intelligence is high, more likely to heal or defend
    private int randomIntelCheck;

    public void Start()
    {
        enemyHealth = 100;
        intelligenceStat = 50; //for testing purposes
    }

    public void RandomiseAction()
    {
        randomIntelCheck = Random.Range(1, 100);
    }

    public void DoAction()
    {
        if (enemyHealth < enemyHealth / 4 && intelligenceStat > randomIntelCheck) //if health is less than 25%
        {
            Heal();
        }
        else if (enemyHealth < enemyHealth / 2 && intelligenceStat > randomIntelCheck)
        {
            Defend();
        }
        else
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (gameManager.currentState == GameStates.EnemyTurn)
        {
            Debug.Log("enemy attacked");
            randomEnum = 0;
            gameManager.EndTurn();
        }
    }

    public void Defend()
    {
        if (gameManager.currentState == GameStates.EnemyTurn)
        {
            Debug.Log("enemy defended");
            randomEnum = 0;
            gameManager.EndTurn();
        }
    }

    public void Heal()
    {
        if (gameManager.currentState == GameStates.EnemyTurn)
        {
            Debug.Log("enemy heal");
            randomEnum = 0;
            gameManager.EndTurn();
        }
    }
}
