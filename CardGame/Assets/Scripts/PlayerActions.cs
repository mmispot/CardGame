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
        enemyActions = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyActions>();
    }

    public void Update()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentPHealth / 100;
        }

        health.text = currentPHealth.ToString();
        defense.text = shield.ToString();
    }

    public void Attack(int actionValue)
    {
        if (gameManager.currentState == GameStates.PlayerTurn)
        {
            enemyActions.currentEHealth -= actionValue;
            Debug.Log("player attacked");
            gameManager.EndTurn();
        }
    }

    public void Defend(int actionValue)
    {
        if (gameManager.currentState == GameStates.PlayerTurn)
        {
            shield += actionValue;
            Debug.Log("player defended");
            gameManager.EndTurn();
        }
    }

    public void Heal(int actionValue)
    {
        if (gameManager.currentState == GameStates.PlayerTurn)
        {
            currentPHealth += actionValue;
            Debug.Log("player healed");
            gameManager.EndTurn();
        }
    }
}
