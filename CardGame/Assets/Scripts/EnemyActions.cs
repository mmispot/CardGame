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
    public string enemyName;
    public float totalEHealth;
    public float currentEHealth;
    public float enemyDefAmount;
    public float enemyHealAmount;
    public float currentEDefense;
    [SerializeField] Image healthBar;

    [SerializeField] private TMP_Text health;
    [SerializeField] private TMP_Text defense;

    public bool isDefeated;

    public void Start()
    {
        currentEDefense = 0;

        playerActions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();

        currentEHealth = totalEHealth;

        isDefeated = false;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        healthBar = GameObject.Find("Enemy HP Bar").GetComponent<Image>();
        health = GameObject.Find("Enemy HP TXT").GetComponent<TMP_Text>();
        defense = GameObject.Find("Enemy Shield TXT").GetComponent<TMP_Text>();

        enemyName = gameObject.name;
    }

    public void Update()
    {
        healthBar.fillAmount = currentEHealth / totalEHealth;
        health.text = currentEHealth.ToString();
        defense.text = currentEDefense.ToString();

        if (currentEHealth <= 0 && !isDefeated && gameManager.eventTime == false) //start next encounter
        {
            isDefeated = true;
            gameManager.NextEncounter();
            Destroy(gameObject);
        }
    }

    public void DoAction()
    {
        randomIntelCheck = Random.Range(1, 100); //enemy AI based on intelligence stat

        if (currentEHealth < (totalEHealth / 4) && intelligenceStat >= randomIntelCheck)
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

    public void Attack(float incomingDamage) //changes dmg based on player defense
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

        gameManager.SwitchTurn(GameStates.PlayerTurn);
    }

    public void Defend()
    {
        currentEDefense += enemyDefAmount;
        gameManager.SwitchTurn(GameStates.PlayerTurn);
    }

    public void Heal()
    {
        currentEHealth += enemyHealAmount;
        gameManager.SwitchTurn(GameStates.PlayerTurn);
    }
}
