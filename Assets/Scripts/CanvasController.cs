using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{

    public GameObject bar, menuBar, cardsBar, shop;
    public TextMeshProUGUI Espadas, Copas, Ouro, Paus;
    public GameObject LojaPanel;
    public GameObject LojaButton;
    private BattleControiller bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType(typeof(BattleControiller)) as BattleControiller;
    }

    public void Initiallize()
    {
        bar.SetActive(true);
        menuBar.SetActive(true);
    }

    public void EndCanva()
    {
        bar.SetActive(false);
        //menuBar.SetActive(true);
    }

    public void Lutar()
    {
        menuBar.SetActive(false);
        cardsBar.SetActive(true);
    }

    public void FreezeBattle()
    {
        cardsBar.SetActive(false);
        menuBar.SetActive(true);
    }

    public void ActiveInventory(string atk, string dmg, string gld, string live)
    {
        menuBar.SetActive(false);
        shop.SetActive(true);

        Espadas.text = "♠: " + atk;
        Paus.text = "♣: " + dmg;
        Ouro.text = "♦: " + gld;
        Copas.text = "♥: " + live;
    }

    public void OpenShop()
    {
        LojaButton.SetActive(false);
        LojaPanel.SetActive(true);
    }

    public void CloseShop()
    {
        LojaButton.SetActive(true);
        LojaPanel.SetActive(false);
    }

    public void DisableInventory()
    {
        menuBar.SetActive(true);
        shop.SetActive(false);
    }

    public void Next()
    {
        bc.cont.level++;
        CloseShop();
        DisableInventory();
        FreezeBattle();
        EndCanva();

        if (bc.cont.level >= 6)
        {
            //SceneManager.LoadSceneAsync("Cardboss");
        }
        else
        {
            //SceneManager.LoadSceneAsync("Cardtable");
        }

        bc.FinishBattle();

    }

    public void comprarSkill()
    {

        if (bc.cont.ouros >= 3)
        {
            bc.cont.skillDmg++;

            bc.cont.ouros -= 3;
            // cont.i++;
            string gld = bc.cont.ouros.ToString();
            Ouro.text = "♦: " + gld;
        }
        else { }

    }

    public void comprarVida()
    {

        if (bc.cont.ouros >= 1)
        {
            bc.player.life++;

            bc.cont.ouros -= 1;
            string gld = bc.cont.ouros.ToString();
            Ouro.text = "♦: " + gld;
        }
        else { }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
