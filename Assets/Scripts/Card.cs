using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    private BC bc;
    public int value;
    public int priority;
    public Sprite face;
    public DeckManager Deck_Controller;
   
    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType(typeof(BC)) as BC;
        Deck_Controller.ShuffleDeck();
        changeValue();
    }

    public void letsBattle()
    {
        bc.Card(this);
    }

    public void changeValue()
    {
        List<int> hand = Deck_Controller.DrawCards(1);
        value = hand[0];

        face = bc.cardFaces[value];
        this.gameObject.GetComponent<Image>().sprite = face;
    }

    public void defaultValue()
    {
        value = 0;
        this.gameObject.tag = "NullCard";
        this.gameObject.GetComponent<Image>().sprite = bc.cardFaces[0];
        this.gameObject.GetComponent<Button>().interactable = false;
    }

    public void resetCard()
    {
        this.gameObject.tag = "NewCard";
        this.gameObject.GetComponent<Button>().interactable = true;
        changeValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
