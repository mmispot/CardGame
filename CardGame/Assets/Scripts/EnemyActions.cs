using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyActions : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerActions playerActions;

    [SerializeField] private int intelligenceStat; //if intelligence is high, more likely to heal or defend
    [SerializeField] private int randomIntelCheck;
    public float damageAmount;

    public float totalEHealth;
    public float currentEHealth;
    public float enemyDefAmount;
    public float enemyHealAmount;
    public float currentEDefense;
    [SerializeField] Image healthBar;

    [SerializeField] private TMP_Text health;
    [SerializeField] private TMP_Text defense;

    public void Start()
    {
        totalEHealth = 50;
        damageAmount = 5;
        currentEDefense = 0;

        intelligenceStat = 100; //for testing purposes

        playerActions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();

        currentEHealth = totalEHealth;

        gameObject.SetActive(true);
    }

    public void Update()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentEHealth / totalEHealth;
        }

        health.text = currentEHealth.ToString();
        defense.text = currentEDefense.ToString();

        if (currentEHealth <= 0)
        {
            gameManager.NextEncounter();
            gameObject.SetActive(false);
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
        gameManager.EndTurn();
    }

    public void Defend()
    {
        currentEDefense += 5;
        gameManager.EndTurn();
    }

    public void Heal()
    {
        currentEHealth += 10;
        gameManager.EndTurn();
    }
}
