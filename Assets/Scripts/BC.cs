using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class BC : MonoBehaviour
{
    private Player player;
    private Enemie enemie;
    public Controller cont;
    public MC mc;
    public Animator anim, animP;
    public GameObject menuA, menuB, shop, op, Aiko, Picolin;
    public GameObject cam1;

    public GameObject LojaPanel;
    public GameObject LojaButton;

    public GameObject Player, att, lf, gd, dmg;
    public TextMeshProUGUI Espadas, Copas, Ouro, Paus;

    public GameObject Enemie, Golem, Spider, Bulldog;
    public GameObject barPlayer;
    public GameObject[] terrain, minion;

    public bool loja = true;
    
    



    // Start is called before the first frame update
    void Start()
    {
        

        Instantiate(op);

            



        switch (cont.terrain)
        {
            case 0:
                terrain[0].SetActive(true);
                break;

            case 1:
                terrain[1].SetActive(true);
                break;

            case 2:
                terrain[2].SetActive(true);
                break;
            case 3:
                terrain[3].SetActive(true);
                break;
        }
        enemie = FindObjectOfType(typeof(Enemie)) as Enemie;
        
        player = FindObjectOfType(typeof(Player)) as Player;


        for (; player.player <= cont.players; player.player++)
        {

            Player.SetActive(true);
        }
        player.player = 1;

        switch (cont.level)
        {

            case 6:

                enemie.lifeMax = 11;
                enemie.life = enemie.lifeMax;
                Bulldog.SetActive(true);
                anim = Bulldog.GetComponent<Animator>();

                break;

            case 8:

                enemie.lifeMax = 12;
                enemie.life = enemie.lifeMax;
                Spider.SetActive(true);
                anim = Spider.GetComponent<Animator>();

                break;

            case 10:

                enemie.lifeMax = 13;
                enemie.life = enemie.lifeMax;
                Golem.SetActive(true);
                anim = Golem.GetComponent<Animator>();

                break;

            default:
                int mini = Random.Range(0, 2);
                enemie.lifeMax = cont.inimigo;
                enemie.life = enemie.lifeMax;
                minion[mini].SetActive(true);
                anim = minion[mini].GetComponent<Animator>();


                break;


        }



        for (; player.player <= cont.players; player.player++)
        {
            barPlayer.SetActive(true);
            player.life = cont.lifeP;


            
            



            switch (cont.perso)
            {
                case "Aiko":

                    Aiko.SetActive(true);
                    player.name = "Aiko";
                    animP = Aiko.GetComponent<Animator>();
                    player.classe = "Elemental";
                    player.type = "Fire";
                    player.lifeMax = 10;
                    player.hab[1] = "Fire Storm";
                    player.damage[1] = 1 + cont.skillDmg;

                    break;

                case "Picolin":

                    Picolin.SetActive(true);
                    player.name = "Picolin";
                    anim = Picolin.GetComponent<Animator>();
                    player.classe = "Paladin";
                    player.lifeMax = 12;
                    player.hab[1] = "Fury";
                    player.damage[1] = 1 + cont.skillDmg;

                    break;

                case "Woul":
                    player.name = "Woul";
                    break;
            }


        }

        player.player = 1;

        Espadas = att.GetComponent<TextMeshProUGUI>();
        Copas = lf.GetComponent<TextMeshProUGUI>();
        Ouro = gd.GetComponent<TextMeshProUGUI>();
        Paus = dmg.GetComponent<TextMeshProUGUI>();

        menuB.SetActive(false);
        menuA.SetActive(true);



    }

    // Update is called once per frame
    void Update()
    {
        string vida = player.life + " / " + player.lifeMax;
        string vidaE = enemie.life + " / " + enemie.lifeMax;

        player.lifeQuant.text = vida;
        enemie.lifeQuant.text = vidaE;

       


    }

    public void Lutar()
    {
        menuA.SetActive(false);
        menuB.SetActive(true);

    }

  /*  public void TrocarPlayer()
    {
        player.player++;
        if(player.player >= mc.players)
        {

            player.player = 1;

        }


    }*/

    public void Skill()
    {
        if (player.skill <= 0)
        {
            enemie.tomarDano(player.damage[1]);
            anim.SetTrigger("isHitting");
            
            player.skill = 3;
        }



        if (enemie.life <= 0)
        {
            string atk, dmg, gld, live;


            anim.SetTrigger("isDead");

            menuA.SetActive(false);
            shop.SetActive(true);


            switch (cont.terrain)
            {

                case 0:
                    cont.espadas++;



                    break;

                case 1:
                    cont.paus++;


                    break;

                case 2:
                    cont.ouros += (int)enemie.lifeMax;


                    break;
                case 3:

                    cont.copas += (int)enemie.lifeMax;
                    player.life = player.life + enemie.lifeMax;
                    if (player.life > player.lifeMax)
                    {
                        player.life = player.lifeMax;
                    }




                    break;
            }

            atk = cont.espadas.ToString();
            dmg = cont.paus.ToString();
            gld = cont.ouros.ToString();
            live = cont.copas.ToString();


            Espadas.text = "♠: " + atk;
            Paus.text = "♣: " + dmg;
            Ouro.text = "♦: " + gld;
            Copas.text = "♥: " + live;


            cont.lifeP = (int)player.life;

            cont.copas = 0;






        }


    }

    public void Card()
    {
        bool tie = false;


        int card = Random.Range(1, 10) + cont.espadas;
        int eneCard = Random.Range(1, 10) + cont.paus;
        print(card + " x " + eneCard);
        if(card < eneCard)
        {
            menuB.SetActive(false);


            menuA.SetActive(true);
            player.tomarDano(enemie.damage);
            anim.SetTrigger("isAttacking");
            tie = false;
            
            
        } else if(card > eneCard)
        {
            menuB.SetActive(false);
            menuA.SetActive(true);
            enemie.tomarDano(player.damage[0]);
            anim.SetTrigger("isHitting");
            tie = false;
            
        } else
        {
            enemie.damage++;
            player.damage[0]++;
            gameObject.GetComponent<Button>().interactable = false;
            tie = true;
            
        }

        if (player.skill > 0)
        {
            player.skill--;
        }
        


        if (player.damage[0] > 1 && tie != true || enemie.damage > 1 && tie != true)
        {
            player.damage[0] = 1;
            enemie.damage = 1;
        }

        

        if (enemie.life <= 0)
        {
            string atk, dmg, gld, live;
            

            anim.SetTrigger("isDead");

            menuA.SetActive(false);
            shop.SetActive(true);


            switch (cont.terrain)
            {

                case 0:
                    cont.espadas++;
                    
                    

                    break;

                case 1:
                    cont.paus++;
                    
                    
                    break;

                case 2:
                    cont.ouros += (int)enemie.lifeMax;
                    
                   
                    break;
                case 3:
                    player.life = player.life + enemie.lifeMax;
                    if (player.life > player.lifeMax)
                    {
                        player.life = player.lifeMax;
                    }

                    
                   

                    break;
            }

            atk = cont.espadas.ToString();
            dmg = cont.paus.ToString();
            gld = cont.ouros.ToString();
            live = cont.copas.ToString();


            Espadas.text = "♠: " + atk;
            Paus.text = "♣: " + dmg;
            Ouro.text = "♦: " + gld;
            Copas.text = "♥: " + live;


            cont.lifeP = (int)player.life;
            cont.copas = 0;

            

           

        }

        
    }

    public void abrirLoja()
    {

        LojaButton.SetActive(false);
        LojaPanel.SetActive(true);


    }



    public void proximo() {

        cont.level++;

        if (cont.level >= 6)
        {

            SceneManager.LoadSceneAsync("Cardboss");

        }
        else
        {

            SceneManager.LoadSceneAsync("Cardtable");

        }
        

    }

    public void comprarSkill()
    {

        
        if(cont.ouros >= 3)
        {
            cont.skillDmg++;
            
            cont.ouros -= 3;
           // cont.i++;
            string gld = cont.ouros.ToString();
            Ouro.text = "♦: " + gld;
        }
        else { }


    }

    public void comprarVida()
    {

        
        if (cont.ouros >= 1)
        {
            player.life++;
            
            cont.ouros -= 1;
            string gld = cont.ouros.ToString();
            Ouro.text = "♦: " + gld;
        }
        else { }


    }

}
