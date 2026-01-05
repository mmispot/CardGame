using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "CardBase", menuName = "Scriptable Objects/CardBase")]
public class CardBase : ScriptableObject
{
    public enum actionTypes
    { 
      Attack = 0, 
      Defend = 1, 
      Heal = 2, 
      Buff = 3, 
      Debuff = 4
    }

    public string cardName;
    public string flavorText;

    public Sprite cardImage;
    public Sprite artworkImage;

    public int cardCost;
    public actionTypes firstType;
    public int actionValue; //amount of damage, block, heal, etc.

}
