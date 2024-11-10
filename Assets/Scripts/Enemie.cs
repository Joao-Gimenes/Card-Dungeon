using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Enemie : MonoBehaviour
{
    private Player player;
    private MC mc;
    public GameObject actualSkin;


   // public string name;
    public int lifeMax, life;
    public int quant;
    public GameObject[] totalHp;

    public string[] hab;
    public int damage;
    public GameObject[] skins;

    public GameObject LifeCam;
    //private Vector3 vetor;

    /*private Transform barHp;
    public GameObject ttHp;
    public TextMeshProUGUI lifeQuant;*/

    // Start is called before the first frame update
    void Start()
    {

       // lifeQuant = ttHp.GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType(typeof(Player)) as Player;
        mc = FindObjectOfType(typeof(MC)) as MC;

        //barHp = GameObject.Find("Lfe").transform;
        life = lifeMax;

    }

    public void InitializeEnemie(int enemyCount, int level)
    {
        
        lifeMax = enemyCount;
        life = enemyCount;

        switch (enemyCount)
        {

            case 11:
                lifeMax = 10;
                life = lifeMax;
                actualSkin = skins[3];
                //anim = Bulldog.GetComponent<Animator>();
                break;

            case 12:
                lifeMax = 10;
                life = lifeMax;
                actualSkin = skins[4];
                //anim = Spider.GetComponent<Animator>();
                break;

            case 13:
                lifeMax = 10;
                life = lifeMax;
                actualSkin = skins[5];
                //anim = Golem.GetComponent<Animator>();
                break;

            default:
                int mini = Random.Range(0, 3);
                lifeMax = enemyCount;
                life = lifeMax;
                actualSkin = skins[mini];
                //anim = minion[mini].GetComponent<Animator>();
                break;
        }

        hpBarLoad("recover", enemyCount);

        actualSkin.SetActive(true);

    }

    // Update is called once per frame
    public void tomarDano(int value)
    {
        life -= value;       

        if (life <= 0)
        {
            life = 0;

        }
        StartCoroutine(DelayHpLoad());

    }

    public void Regenerate(int recover)
    {
        life += recover;

        if (life >lifeMax)
        {
            life = lifeMax;
        }
        hpBarLoad("recover");
    }
    public void hpBarLoad(string method = "damage", int regenValue = 0)
    {
        //LifeCam.SetActive(true);
        if (method == "recover")
        {
            for (int i = 0; i < life; i++)
            {

                if( totalHp[lifeMax - 1].activeSelf != true )
                {
                    totalHp[ i ].SetActive(true);
                }
                
            }
        } else
        {
            for (int i = lifeMax; i > life; i--)
            {
                totalHp[i - 1].SetActive(false);
            }
        }
        //LifeCam.SetActive(false);

    }

    public void LoadEnemie(int value)
    {

    }

     private IEnumerator DelayHpLoad(string value = "damage")
    {
        LifeCam.SetActive(true);
        // Delay de 2 segundos antes de carregar a barra de vida
        yield return new WaitForSeconds(2f);
        hpBarLoad();
        // Delay de 2 segundos após o carregamento da barra de vida
        yield return new WaitForSeconds(2f);

        LifeCam.SetActive(false);
    }

}
