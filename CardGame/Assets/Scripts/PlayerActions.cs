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
    [SerializeField] Image healthBar;

    public float shield = 0;


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
            Debug.Log("player defended");
            gameManager.EndTurn();
        }
    }

    public void Heal(int actionValue)
    {
        if (gameManager.currentState == GameStates.PlayerTurn)
        {
            Debug.Log("player healed");
            gameManager.EndTurn();
        }
    }
}
