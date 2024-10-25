using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine;

public class LC : MonoBehaviour
{
    
    public GameObject canvC, op, cardsD, cardsB;
    public Controller cont;
    private Player player;
    
    public string perso;
    public int inimigo, terrain, players, lifeP;

    public GameObject[] levels;
    
    private BC bc;
    public MC mc;
    private Enemie enemie;


    // Start is called before the first frame update
    void Start()
    {
        
        player = FindObjectOfType(typeof(Player)) as Player;
        enemie = FindObjectOfType(typeof(Enemie)) as Enemie;
        bc = FindObjectOfType(typeof(BC)) as BC;

        verifyStage();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void verifyStage()
    {
        switch (cont.level)
        {
            case 1:
                break;
            case 2:
                levels[0].SetActive(false);
                levels[1].SetActive(true);
                levels[2].SetActive(true);
                break;
            case 3:
                levels[0].SetActive(false);
                levels[1].SetActive(false);
                levels[2].SetActive(false);
                levels[3].SetActive(true);
                levels[4].SetActive(true);
                levels[5].SetActive(true);
                break;
            case 4:
                levels[0].SetActive(false);
                levels[3].SetActive(false);
                levels[4].SetActive(false);
                levels[5].SetActive(false);
                levels[6].SetActive(true);
                levels[7].SetActive(true);
                levels[8].SetActive(true);
                levels[9].SetActive(true);
                break;
            case 5:
                levels[0].SetActive(false);
                levels[6].SetActive(false);
                levels[7].SetActive(false);
                levels[8].SetActive(false);
                levels[9].SetActive(false);
                levels[10].SetActive(true);
                levels[11].SetActive(true);
                levels[12].SetActive(true);
                levels[13].SetActive(true);
                levels[14].SetActive(true);
                break;
            case 6:
                levels[10].SetActive(false);
                levels[11].SetActive(false);
                levels[12].SetActive(false);
                levels[13].SetActive(false);
                levels[14].SetActive(false);
                levels[0].SetActive(true);
                break;
            case 7:
                levels[0].SetActive(false);
                levels[1].SetActive(true);
                levels[2].SetActive(true);
                levels[3].SetActive(true);
                break;
            case 8:
                levels[0].SetActive(false);
                levels[1].SetActive(false);
                levels[2].SetActive(false);
                levels[3].SetActive(false);
                levels[4].SetActive(true);
                break;
            case 9:
                levels[0].SetActive(false);
                levels[1].SetActive(false);
                levels[2].SetActive(false);
                levels[3].SetActive(false);
                levels[4].SetActive(false);
                levels[5].SetActive(true);
                levels[6].SetActive(true);
                break;
            case 10:
                levels[0].SetActive(false);
                levels[1].SetActive(false);
                levels[2].SetActive(false);
                levels[3].SetActive(false);
                levels[4].SetActive(false);
                levels[5].SetActive(false);
                levels[6].SetActive(false);
                levels[7].SetActive(true);
                break;

            case 11:
                SceneManager.LoadSceneAsync("Dungeon");

                break;
        }
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
        
      /*  string localPath = "Assets/MainC/Prefab" + gb.name + ".prefab";
        // localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        PrefabUtility.SaveAsPrefabAssetAndConnect(gb, localPath, InteractionMode.UserAction);*/

        SceneManager.LoadSceneAsync("Battle");

    }

}
