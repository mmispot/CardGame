using UnityEngine;
using UnityEngine.UI;

public class EnemyActions : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerActions playerActions;

    private int enemyHealth;
    private int intelligenceStat; //if intelligence is high, more likely to heal or defend
    private int randomIntelCheck;
    public float damageAmount = 10;

    public float totalEHealth = 100;
    public float currentEHealth;
    [SerializeField] Image healthBar;
    public void Start()
    {
        enemyHealth = 100;
        intelligenceStat = 50; //for testing purposes

        playerActions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();

        currentEHealth = totalEHealth;
    }

    public void Update()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentEHealth / 100;
        }

        if (currentEHealth <= 0)
        {
            //Despawn enemy and continue with map
        }
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
            gameManager.EndTurn();
            playerActions.currentPHealth -= damageAmount;
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
