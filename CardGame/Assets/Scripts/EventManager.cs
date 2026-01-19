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
    public TMP_Text eventDescriptionText;
    public Image eventImage;
    public TMP_Text statAddedText;
    public TMP_Text statRemovedText;

    private GameObject popUpInstance;

    public PlayerActions playerActions;

    public bool isOpen;
    public bool statsChanged;

    public void Start()
    {
        playerActions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>();
    }

    public void DoEvent(int eventChoice)
    {
        popUpInstance = Instantiate(uiPopUp, mainCanvas.transform);
        isOpen = true;
        SetEventText(eventChoice);
        EventStatChanges(eventChoice);
    }

    public void ResetEventStatChanges()
    {
        gameManager.manaCoffee = 3;

    }

    public void EventStatChanges(int eventID)
    {
        if (!statsChanged)
        {
            if (eventOptions[eventID].reduceStat != null)  //reduces if reduceStat is filled
                {
                    playerActions.currentPHealth -= eventOptions[eventID].changeStatValue;
                    statsChanged = true;
                }
            else if (eventOptions[eventID].addStat != null) //adds if addStat is filled
                {
                    playerActions.currentPHealth += eventOptions[eventID].changeStatValue;
                    statsChanged = true;
            }

        }
    }

    public void SetEventText(int eventID)
    {
        eventTitleText = popUpInstance.transform.Find("Event Title").GetComponent<TMP_Text>();
        eventImage = popUpInstance.transform.Find("Img").GetComponent<Image>();
        eventDescriptionText = popUpInstance.transform.Find("Description").GetComponent<TMP_Text>();
        statRemovedText = popUpInstance.transform.Find("RemoveStat").GetComponent<TMP_Text>();
        statAddedText = popUpInstance.transform.Find("AddStat").GetComponent<TMP_Text>();
        Button btn = popUpInstance.transform.Find("Continue").GetComponent<Button>();

        btn.onClick.AddListener(Continue);

        eventTitleText.text = eventOptions[eventID].eventTitle;
        eventDescriptionText.text = eventOptions[eventID].eventDescription;
        eventImage.sprite = eventOptions[eventID].image;
        statAddedText.text = eventOptions[eventID].addStat;
        statRemovedText.text = eventOptions[eventID].reduceStat;
    }

    private void Continue()
    {
        gameManager.NextEncounter();
        gameManager.eventTime = false;
        gameManager.SwitchTurn(GameStates.PlayerTurn);

        Destroy(popUpInstance);
        isOpen = false;
        statsChanged = false;
        popUpInstance = null;
    }
}
