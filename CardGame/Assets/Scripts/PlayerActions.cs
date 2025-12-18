using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.DefaultInputActions;

public class PlayerActions : MonoBehaviour
{
    public GameManager gameManager;
    public EnemyActions enemyActions;

    public float totalPHealth = 100;
    public float currentPHealth;
    [SerializeField] Image healthBar;

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
    }

    public void Attack()
    {
        if (gameManager.currentState == GameStates.PlayerTurn)
        {
            Debug.Log("player attacked");
            gameManager.EndTurn();
            enemyActions.currentEHealth -= 10;
        }
    }

    public void Defend()
    {
        if (gameManager.currentState == GameStates.PlayerTurn)
        {
            Debug.Log("player defended");
            gameManager.EndTurn();
        }
    }

    public void Heal()
    {
        if (gameManager.currentState == GameStates.PlayerTurn)
        {
            Debug.Log("player healed");
            gameManager.EndTurn();
        }
    }
}
