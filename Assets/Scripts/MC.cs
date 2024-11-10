using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class MC : MonoBehaviour
{
    public GameObject mainCameraMove, charactersCameraMove, tableCameraMove;
    public Controller actualController;

    public GameObject[] Cards_Positions;
    public GameObject[] Boss_Positions;
    public GameObject[] Cards_Paus;
    public GameObject[] Cards_Ouro;
    public GameObject[] Cards_Copas;
    public GameObject[] Cards_Espadas;
    public List<GameObject> Cards_inGame;

    public GameObject[] camP;
    
    public GameObject[] PanelC;
    public GameObject canvM, canvP, canvC;
    public int playerId = 0;

    private Player player;
    public string perso;
    public int inimigo, terrain, players;
    public int lifeP;
    
    public GameObject P1, P2, P3, P4;

    public int level = 1;
    private Enemie enemie;


    // Start is called before the first frame update
    void Start()
    {
        //Encontra os objetos respectivos em cena
        player = FindObjectOfType(typeof(Player)) as Player;
        enemie = FindObjectOfType(typeof(Enemie)) as Enemie;

        resetGame();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetGame( )
    {

        //Reseta as configurações iniciais do game
        actualController.level = 1;
        actualController.copas = 0; actualController.paus = 0; actualController.ouros = 0; actualController.espadas = 0; actualController.skillDmg = 0;

    }

    //Função chamada ao clicar em cada botão de quantia de players
    public void QuantidadePlayers(int playersQ)
    {
        switch (playersQ)
        {
            case 1:
                actualController.players = 1;               
                break;
            case 2:
                actualController.players = 2;
                break;
            case 3:
                actualController.players = 3;
                break;
            case 4:
                actualController.players = 4;
                break;
        }

    }
    //Ação do botão de Start
    public void Play()
    {
        //Desativa a cena inicial
        mainCameraMove.SetActive(false);
        canvM.SetActive(false);

        //Ativa a jogatina
        charactersCameraMove.SetActive(true);
        canvP.SetActive(true);

    }
    //Função chamada no botão dos personagens
    public void selectCharacter(string name)
    {

        switch (name)
        {
            

            case "Aiko":

                camP[1].SetActive(false); 
                PanelC[1].SetActive(false); 
                actualController.perso = "Aiko";
                camP[0].SetActive(true);
                PanelC[0].SetActive(true);
                actualController.lifeP = 10;
                
                break;

            case "Picolin":

                camP[0].SetActive(false);
                PanelC[0].SetActive(false);
                actualController.perso = "Picolin";
                camP[1].SetActive(true);
                PanelC[1].SetActive(true);
                actualController.lifeP = 12;
               
                break;

            case "Woul":

                perso = "Woul";
                camP[2].SetActive(true);
                PanelC[2].SetActive(true);
                lifeP = 10;
                
                break;

        }

    }
    
        public void table()
        {

        // if(playerId >= players)

            CartasNaMesa();
            charactersCameraMove.SetActive(false);
            tableCameraMove.SetActive(true);
              
             canvP.SetActive(false);
            
             //canvC.SetActive(true);
            

         }
    //Essa função é o que define qual será o terreno jogado
    public void fase(GameObject gb) 
    {
        int card = Random.Range(1, 10);
        int nipe = Random.Range(0, 4);
        actualController.inimigo = card;

        if (nipe == 0)
        {
            actualController.terrain = 0;

        }
        else if (nipe == 1)
        {
            actualController.terrain = 1;

        }
        else if (nipe == 2)
        {
            actualController.terrain = 2;

        }
        else if (nipe == 3)
        {
            actualController.terrain = 3;

        }

       /* string localPath = "Assets/MainC/Prefab" + gb.name + ".prefab";
        // localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
    
        PrefabUtility.SaveAsPrefabAsset(gb, localPath);*/

        SceneManager.LoadSceneAsync("Battle");

    }

    public void CartasNaMesa()
    {
        for (int i = 0; i < Cards_Positions.Length; i++)
        {

            int card_number = Random.Range(0, 10);
            int nipe = Random.Range(0, 4);
            GameObject card = Cards_Copas[0];
            GameObject new_card;

            if (nipe == 0)
            {
                card = Cards_Espadas[card_number];
            }
            else if (nipe == 1)
            {
                card = Cards_Paus[card_number];
            }
            else if (nipe == 2)
            {
                card = Cards_Ouro[card_number];
            }
            else if (nipe == 3)
            {
                card = Cards_Copas[card_number];
            }

            if (i == 1 || i == 2 || i == 6 || i == 7 || i == 8 || i == 9)
            {
                Quaternion rotacao = Quaternion.Euler(0, 90, 180);
                new_card = Instantiate(card, Cards_Positions[i].transform.position, rotacao);
                new_card.GetComponent<CardLevel>().value = card_number + 1;
                new_card.GetComponent<CardLevel>().nipe = nipe;
            }
            else
            {
                // Instancia a carta na posição selecionada
                Quaternion rotacao = Quaternion.Euler(0, 0, 180);
                new_card = Instantiate(card, Cards_Positions[i].transform.position, rotacao);
                new_card.GetComponent<CardLevel>().value = card_number + 1;
                new_card.GetComponent<CardLevel>().nipe = nipe;
                if (i == 0)
                {
                    new_card.GetComponent<CardLevel>().canUse = true;
                   
                }
            }
            new_card.GetComponent<CardLevel>().numberPosition = i + 1;
            Cards_inGame.Add(new_card);
           
        }

    }

    public void SegundaRodada()
    {
        ClearCardsInGame();

        for (int i = 0; i < Boss_Positions.Length; i++)
        {
            GameObject card = Cards_Espadas[0];
            GameObject new_card;
            int nipe = Random.Range(0, 4);
            int card_number = 0;

            if (i == 0 || i == 3 || i == 6)
            {
                if (i == 0)
                {
                   card_number = 10;
                }
                else if (i == 3)
                {
                    card_number = 11;
                }
                else if (i == 6)
                {
                    card_number = 12;
                }

                if (nipe == 0)
                {
                    card = Cards_Espadas[card_number];
                }
                else if (nipe == 1)
                {
                    card = Cards_Paus[card_number];
                }
                else if (nipe == 2)
                {
                    card = Cards_Ouro[card_number];
                }
                else if (nipe == 3)
                {
                    card = Cards_Copas[card_number];
                }

                Quaternion rotacao = Quaternion.Euler(0, 0, 180);
                new_card = Instantiate(card, Boss_Positions[i].transform.position, rotacao);
                new_card.GetComponent<CardLevel>().value = card_number + 1;
                new_card.GetComponent<CardLevel>().nipe = nipe;
            }
            else
            {
                card_number = Random.Range(0, 10);
                
                if (nipe == 0)
                {
                    card = Cards_Espadas[card_number];
                }
                else if (nipe == 1)
                {
                    card = Cards_Paus[card_number];
                }
                else if (nipe == 2)
                {
                    card = Cards_Ouro[card_number];
                }
                else if (nipe == 3)
                {
                    card = Cards_Copas[card_number];
                }

                Quaternion rotacao = Quaternion.Euler(0, 90, 180);
                new_card = Instantiate(card, Boss_Positions[i].transform.position, rotacao);
                new_card.GetComponent<CardLevel>().value = card_number + 1;
                new_card.GetComponent<CardLevel>().nipe = nipe;

            }
        
            if (i == 0)
            {
                new_card.GetComponent<CardLevel>().canUse = true;

            }
            
            new_card.GetComponent<CardLevel>().numberPosition = i + 1;
            Cards_inGame.Add(new_card);

        }

    }

    public void ClearCardsInGame()
    {
        // Destrói todas as cartas da lista Cards_inGame
        foreach (GameObject card in Cards_inGame)
        {
            Destroy(card);  // Destrói o objeto da carta
        }
        // Limpa a lista Cards_inGame
        Cards_inGame.Clear();
    }


}
