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

    public RectTransform rectTransform;

    public CardManager cardManager;

    public void Update()
    {
        nameText.text = card.cardName;
        flavorText.text = card.flavorText;
        typeText.text = card.firstType.ToString();
        backgroundImg.sprite = card.cardImage;
        artworkImage.sprite = card.artworkImage;
        manaCost.text = card.cardCost.ToString();
        powerText.text = card.actionValue.ToString();
    }

    public void CardSelected()
    {
        cardManager.SaveCard(this.gameObject);

        //move card up to indicate selection
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += new Vector2(0, 50);
        Debug.Log("card selected");
    }
}
