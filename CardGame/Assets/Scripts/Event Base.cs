using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net;

[CreateAssetMenu(fileName = "EventBase", menuName = "Scriptable Objects/Event Base")]
public class EventBase : ScriptableObject
{
    public string eventTitle;
    public string eventDescription;

    public Image image;

    public string addStat;
    public string reduceStat;

}

