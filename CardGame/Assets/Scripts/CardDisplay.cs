using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CardDisplay : MonoBehaviour
{
    public CardBase card;

    public TMP_Text nameText;
    public TMP_Text flavorText;
    public TMP_Text typeText;

    public Image backgroundImg;
    public Image artworkImage;

    public TMP_Text manaCost;
    public TMP_Text powerText; //block, attack, heal amount etc.

    void Start()
    {
        nameText.text = card.cardName;
        flavorText.text = card.flavorText;
        typeText.text = card.firstType.ToString();
        backgroundImg.sprite = card.cardImage;
        artworkImage.sprite = card.artworkImage;
        manaCost.text = card.cardCost.ToString();
        powerText.text = card.actionValue.ToString();
    }
}
