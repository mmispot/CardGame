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
            Attack(damageAmount);
        }
    }

    public void Attack(float incomingDamage)
    {
        Mathf.Clamp(playerActions.shield, 0, 100);
        Mathf.Clamp(playerActions.currentPHealth, 0, 100);

        if (incomingDamage > playerActions.shield)
        {
            float damageAfterShield = incomingDamage - playerActions.shield;
            playerActions.shield = 0;
            playerActions.currentPHealth -= damageAfterShield;
        }
        else
        {
            playerActions.shield -= incomingDamage;
        }
        Debug.Log("enemy attacked");
        gameManager.EndTurn();
    }

    public void Defend()
    {
            Debug.Log("enemy defended");
            gameManager.EndTurn();
    }

    public void Heal()
    {
            Debug.Log("enemy healed");
            gameManager.EndTurn();
    }
}
