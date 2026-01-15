using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.DefaultInputActions;

public class PlayerActions : MonoBehaviour
{
    public GameManager gameManager;
    public CardManager cardManager;
    public EnemyActions enemyActions;

    public float totalPHealth = 100;
    public float currentPHealth;
    [SerializeField] private Image healthBar;

    public float shield = 0;

    [SerializeField] private TMP_Text health;
    [SerializeField] private TMP_Text defense;

    public void Start()
    {
        currentPHealth = totalPHealth;
        enemyActions = null;

        if (currentPHealth > totalPHealth)
        {
            currentPHealth = totalPHealth;
        }
    }

    public void Update()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentPHealth / 100;
        }

        health.text = currentPHealth.ToString();
        defense.text = shield.ToString();

        if (currentPHealth <= 0)
        {
            //game over logic
        }

        if (enemyActions == null && gameManager.eventTime == false)
        {
            Debug.Log("enemy actions is empty");
            enemyActions = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyActions>();
        }
    }  

    public void Attack(int actionValue)
    {
        if (gameManager.currentState == GameStates.PlayerTurn)
        {
            if (actionValue > enemyActions.currentEDefense)
            {
                float dmgAfterDef = actionValue - enemyActions.currentEDefense;
                enemyActions.currentEDefense = 0;
                enemyActions.currentEHealth -= dmgAfterDef;
            }
            else
            {
                enemyActions.currentEDefense -= actionValue;
            }
        }
    }

    public void Defend(int actionValue)
    {
        if (gameManager.currentState == GameStates.PlayerTurn)
        {
            shield += actionValue;
        }
    }

    public void Heal(int actionValue)
    {
        if (gameManager.currentState == GameStates.PlayerTurn)
        {
            currentPHealth += actionValue;
        }
    }
}
