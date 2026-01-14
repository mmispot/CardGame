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
        FindEnemyScript();
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
    }

    public void FindEnemyScript()
    {
        if (gameManager.currentEncounterIndex == 0)
        {
            enemyActions = GameObject.Find("Low-Value Bug").GetComponent<EnemyActions>();
            return;
        }
        else if (gameManager.currentEncounterIndex == 2)
        {
            enemyActions = GameObject.Find("Feature Bug").GetComponent<EnemyActions>();
            return;
        }
        else if (gameManager.currentEncounterIndex == 4)
        {
            enemyActions = GameObject.Find("Final Boss").GetComponent<EnemyActions>();
            return;
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
