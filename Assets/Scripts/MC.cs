using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class MC : MonoBehaviour
{
    public GameObject cam1, cam2, cam3;
    public Controller cont;
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
        player = FindObjectOfType(typeof(Player)) as Player;
        enemie = FindObjectOfType(typeof(Enemie)) as Enemie;
        bc = FindObjectOfType(typeof(BC)) as BC;
        play.SetActive(false);
        cont.level = 1;
        cont.copas = 0; cont.paus = 0; cont.ouros = 0; cont.espadas = 0; cont.skillDmg = 0;
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuantidadePlayers(int playersQ)
    {
        switch (playersQ)
        {
            case 1:
                cont.players = 1;
                
                break;
            case 2:
                cont.players = 2;
                break;
            case 3:
                cont.players = 3;
                break;
            case 4:
                cont.players = 4;
                break;
        }

        
        play.SetActive(true);

    }
    public void Play()
    {
       
        cam1.SetActive(false);
        canvM.SetActive(false);
       
        cam2.SetActive(true);
        canvP.SetActive(true);



    }
    public void selectCharacter(string name)
    {

       // int i = 0;

        switch (name)
        {
            

            case "Aiko":

                //while(i <= camP.Length) { camP[i].SetActive(false); while (i <= PanelC.Length) { PanelC[i].SetActive(false); } }

                camP[1].SetActive(false);
                PanelC[1].SetActive(false);
                cont.perso = "Aiko";
                camP[0].SetActive(true);
                PanelC[0].SetActive(true);
                cont.lifeP = 10;
                
                break;

            case "Picolin":


                // while (i <= camP.Length) { camP[i].SetActive(false); while (i <= PanelC.Length) { PanelC[i].SetActive(false); } }

                camP[0].SetActive(false);
                PanelC[0].SetActive(false);
                cont.perso = "Picolin";
                camP[1].SetActive(true);
                PanelC[1].SetActive(true);
                cont.lifeP = 12;
               
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
             

             cam2.SetActive(false);
             cam3.SetActive(true);
              
             canvP.SetActive(false);
            
             canvC.SetActive(true);
            

         }
    public void fase(GameObject gb) 
    {
        int card = Random.Range(1, 10);
        int nipe = Random.Range(0, 4);
        cont.inimigo = card;

        if (nipe == 0)
        {
            cont.terrain = 0;

        }
        else if (nipe == 1)
        {
            cont.terrain = 1;

        }
        else if (nipe == 2)
        {
            cont.terrain = 2;

        }
        else if (nipe == 3)
        {
            cont.terrain = 3;

        }

       /* string localPath = "Assets/MainC/Prefab" + gb.name + ".prefab";
        // localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
    
        PrefabUtility.SaveAsPrefabAsset(gb, localPath);*/





        SceneManager.LoadSceneAsync("Battle");

    }



}
