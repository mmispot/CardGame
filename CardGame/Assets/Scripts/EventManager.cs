using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject uiPopUp;
    public int eventChoice;

    public void DoEvent(int eventChoice)
    {
        if (eventChoice == 1)
        {
            //kickstarter failed, lose 5HP but gain 1 extra mana from now on
        }
        else if (eventChoice == 2)
        {
            //found a good tutorial, heal 20HP
        }
        else if (eventChoice == 3)
        {
            //colleague brings coffee, fully heal up
        }
        else if (eventChoice == 4)
        {
            //github conflict error, lose 1/4th of your hp
        }
    }
}
