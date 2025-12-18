using UnityEngine;
using UnityEngine.UI;

public class EnemyActions : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerActions playerActions;

    private int intelligenceStat; //if intelligence is high, more likely to heal or defend
    [SerializeField] private int randomIntelCheck;
    public float damageAmount = 10;

    public float totalEHealth = 100;
    public float currentEHealth;
    [SerializeField] Image healthBar;
    public void Start()
    {

        intelligenceStat = 100; //for testing purposes

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

    public void DoAction()
    {
        randomIntelCheck = Random.Range(1, 100);

        if (currentEHealth < (totalEHealth / 4) && intelligenceStat >= randomIntelCheck) //if health is less than 25%
        {
            Defend();
        }
        else if (currentEHealth < (totalEHealth / 2) && intelligenceStat >= randomIntelCheck)
        {
            Heal();
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
            Debug.Log("enemy healed");
            gameManager.EndTurn();
        }
    }
}
