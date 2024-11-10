using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BattleControiller : MonoBehaviour
{
    public GameObject playerObject, persona, inimigo;
    public Controller cont;
    public Player player;
    private Enemie enemie;

    public CanvasController canvaController;
    public MC mc;

    public Sprite[] cardFaces;
    public GameObject[] cards;

    public Dice[] Dices;
    public DiceManager Dice;
    public DeckManager Enemie_Deck_Controller;
    public GameObject actualCard;

    public Dictionary<string, GameObject> charactersNames = new Dictionary<string, GameObject>();
    public GameObject[] characters;
    public Transform[] inimigos;         // Prefabs dos inimigos
    public Transform[] posicoesInimigos; // Possíveis posições dos inimigos

    public GameObject battleCamera;

    private void Start()
    {
        enemie = FindObjectOfType(typeof(Enemie)) as Enemie;
        charactersNames.Add("Aiko", characters[0]);
        charactersNames.Add("Picolin", characters[1]);
    }

    public void InitializeBattle(int enemyCount)
    {
        ConfigPlayer();
        ConfigEnemie(enemyCount);
        battleCamera.SetActive(true);
        canvaController.Initiallize();
    }

    public void Skill()
    {
        
        if (player.skill <= 0)
        {
            List<int> results = Dice.PlayDice(3);

            StartCoroutine(WaitingDices(results));

            //player.activeSkill(player.hab[1], results);
            // player.skill = 3;
        }

    }

    public void Card(Card cardActual)
    {
        //Sim, isso é importante e provavelmente esqueci de te falar
        bool tie = false;
        GameObject[] cards = GameObject.FindGameObjectsWithTag("NullCard");
        //Quando acontece um empate, a carta precisa sair da tela e não ser selecionavel, arraste pra cima e entenda o porquê

        int card = cardActual.value + cont.espadas;
        cardActual.changeValue();

        List<int> hand = Enemie_Deck_Controller.DrawCards(1);
        int eneCard = hand[0] + cont.paus;
        print(card + " x " + eneCard);

        //Player perde
        if (card < eneCard)
        {
            canvaController.FreezeBattle();
            player.tomarDano(enemie.damage);
            //anim.SetTrigger("isAttacking");
            tie = false;

        }
        else if (card > eneCard) //Player Ganha
        {
            canvaController.FreezeBattle();
            enemieHitted(player.damage[0]);
            tie = false;

        }
        else //Empates
        {
            enemie.damage++; // O dano é acumulado quando acontece empate, isso de ambas as partes
            player.damage[0]++;
            //  gameObject.GetComponent<Button>().interactable = false; //Isso provavelmente foi uma tentativa falha de tirar a carta do jogo
            cardActual.defaultValue();
            tie = true;
            //O problema? Quando chegarmos à 3 empates seguidos, não sobram cartas na mesa, então ele deve comprar mais duas cartas
        }

        if (player.skill > 0)
        {
            player.skill--;
        }


        //Isso aqui faz voltar tudo ao normal
        if (player.damage[0] > 1 && tie != true || enemie.damage > 1 && tie != true)
        {
            player.damage[0] = 1;
            enemie.damage = 1;

        }

        if (cards.Length > 0 && tie == false)
        {
            foreach (GameObject null_card in cards)
            {
                null_card.GetComponent<Card>().resetCard();
            }
        }
        else if (cards.Length > 1 && tie == true)
        {
            Card( cards[0].GetComponent<Card>() );
        }

        /*if (enemie.life <= 0)
        {
            enemieIsDead();
        }*/

        // loadLife();
    }

    public void enemieIsDead()
    {
        string atk, dmg, gld, live;
        CardLevel prize = mc.Cards_inGame[cont.level - 1].GetComponent<CardLevel>();
        print(prize.value);

        //anim.SetTrigger("isDead");

        switch ( prize.nipe )
        {

            case 0:
                cont.espadas += prize.value;
                break;

            case 1:
                cont.paus += prize.value;
                break;

            case 2:
                cont.ouros += prize.value;
                break;
            case 3:
                cont.copas += prize.value;
                player.life += prize.value;
                player.hpBarLoad("recover",  prize.value);
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


        canvaController.ActiveInventory(atk, dmg, gld, live);

        cont.lifeP = (int)player.life;
        cont.copas = 0;

    }

    public void enemieHitted(int damage)
    {
        enemie.tomarDano(damage);
        //anim.SetTrigger("isHitting");
        if (enemie.life <= 0)
        {
            enemieIsDead();
        }
        //  loadLife();

    }

    public void ConfigPlayer()
    {
        player = playerObject.GetComponent<Player>();
        print(cont.perso);

        switch (cont.perso)
        {
            case "Aiko":
                persona = charactersNames["Aiko"];
                //Player player = playerObject.GetComponent<Player>();
                persona.SetActive(true);
                //player.name = "Aiko";
                //animP = Aiko.GetComponent<Animator>();
                player.classe = "Elemental";
                player.type = "Fire";
                player.lifeMax = 10;
                //player.life = 10;
                player.hab[1] = "Fire Storm";
                player.damage[1] = 1 + cont.skillDmg;
                break;

            case "Picolin":
                persona = charactersNames["Picolin"];
                //Player player = playerObject.GetComponent<Player>();
                persona.SetActive(true);
                //player.name = "Picolin";
                //animP = Picolin.GetComponent<Animator>();
                player.classe = "Paladin";
                player.lifeMax = 10;
                //player.life = 12;
                player.hab[1] = "Fury";
                player.damage[1] = 1 + cont.skillDmg;
                break;

            case "Woul":
                player.name = "Woul";
                break;
        }

        if (cont.level > 1)
        {
            player.life = cont.lifeP;
        } else {
            player.life = player.lifeMax;
        }

    }

    public void FinishBattle()
    {
        battleCamera.SetActive(false);
        enemie.actualSkin.SetActive(false);
        int cardSelected = actualCard.GetComponent<CardLevel>().numberPosition;
        print(cardSelected);

        if (cont.level == 6)
        {
            mc.SegundaRodada();
        } else if (cont.level > 6) {

           if(cardSelected == 1 || cardSelected == 4)
            {
                mc.Cards_inGame[cardSelected + 1].GetComponent<CardLevel>().canUse = true;
                mc.Cards_inGame[cardSelected + 2].GetComponent<CardLevel>().canUse = true;
            } else if (cardSelected == 1 || cardSelected == 3 || cardSelected == 5|| cardSelected == 6)
            {
                mc.Cards_inGame[cardSelected + 1].GetComponent<CardLevel>().canUse = true;
            } else
            {
                print("Você Venceu");
            }
           
        } else
        {
            cardSelected += cont.level;
            mc.Cards_inGame[cardSelected - 2].GetComponent<CardLevel>().canUse = true;
            mc.Cards_inGame[cardSelected - 1].GetComponent<CardLevel>().canUse = true;
        }
        
    }

    public void EndGame()
    {
        FinishBattle();
        persona.SetActive(false);
        mc.resetGame();
        SceneManager.LoadSceneAsync("Editor");

    }

    private void Update()
    {
        
    }

    public IEnumerator WaitingDices(List<int> results)
    {
        canvaController.EndCanva();
        StartCoroutine( player.DelayDice(player.hab[1], results) );
        yield return new WaitForSeconds(1.5f);
        // Aqui você pode colocar qualquer outra ação que deseja executar após a espera
    }

    private void ConfigEnemie(int enemyCount)
    {
        enemie.InitializeEnemie(enemyCount, cont.level);
    }
}
