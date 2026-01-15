using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject uiPopUp;
    public GameObject mainCanvas;
    public int eventChoice;

    public EventBase[] eventOptions;

    public TMP_Text eventTitleText;
    private GameObject popUpInstance;

    public void DoEvent(int eventChoice)
    {
        popUpInstance = Instantiate(uiPopUp, mainCanvas.transform);
        Debug.Log("spawned event pop up");
        SetEventText(eventChoice);
    }

    public void SetEventText(int eventID)
    {
        eventTitleText = popUpInstance.transform.Find("Event Title").GetComponent<TMP_Text>();
        Button btn = popUpInstance.transform.Find("Continue").GetComponent<Button>();


        btn.onClick.AddListener(Continue);

        eventTitleText.text = eventOptions[eventID].eventTitle;
    }

    private void Continue()
    {
        gameManager.NextEncounter();
        gameManager.EndTurn();
        Destroy(popUpInstance);
        popUpInstance = null;
    }
}
