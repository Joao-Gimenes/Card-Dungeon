using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class MC : MonoBehaviour
{
    public GameObject mainCameraMove, charactersCameraMove, tableCameraMove;
    public Controller actualController;
    public GameObject[] camP;
    
    public GameObject[] PanelC;
    public GameObject canvM, canvP, canvC;
    public int playerId = 0;

    private Player player;
    public GameObject play;
    public string perso;
    public int inimigo, terrain, players;
    public int lifeP;
    
    public GameObject P1, P2, P3, P4;

    public int level = 1;
    private BC bc;
    private Enemie enemie;


    // Start is called before the first frame update
    void Start()
    {
        //Encontra os objetos respectivos em cena
        player = FindObjectOfType(typeof(Player)) as Player;
        enemie = FindObjectOfType(typeof(Enemie)) as Enemie;
        bc = FindObjectOfType(typeof(BC)) as BC;

        resetGame();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetGame( )
    {
        //Torna o botão de Start inativo
        play.SetActive(false);

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
        play.SetActive(true);

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


            charactersCameraMove.SetActive(false);
            tableCameraMove.SetActive(true);
              
             canvP.SetActive(false);
            
             canvC.SetActive(true);
            

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



}
