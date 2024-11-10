using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    private Enemie enemie;
    private BattleControiller bc;
    private MC mc;

    public string classe, type;
    public int number,  skill, player = 1;
    public string lifebar;
    public int life, lifeMax;
    public string[] hab;
    public int[] damage;

    public GameObject LifeCam, DiceCam;

    public GameObject[] totalHp;


    

    // Start is called before the first frame update
    void Start()
    {
        //lifeQuant = ttHp.GetComponent<TextMeshProUGUI>();
        mc = FindObjectOfType(typeof(MC)) as MC;
        
        enemie = FindObjectOfType(typeof(Enemie)) as Enemie;
        bc = FindObjectOfType(typeof(BattleControiller)) as BattleControiller;

        //barHp = GameObject.Find("lifebar").transform;

        //life = mc.lifeP;
        //hpBarLoad();

    }

    // Update is called once per frame
   public void tomarDano(int damage)
    {
        LifeCam.SetActive(true);
        life -= damage;

        if (life <= 0)
        {
            life = 0;
            //SceneManager.LoadSceneAsync("Dungeon");
            bc.EndGame();
        }
        StartCoroutine(DelayHpLoad());
        //LifeCam.SetActive(false);

    }

    public void hpBarLoad(string method = "damage", int value = 0)
    {
        
        if (method == "recover")
        {
            if(totalHp[ lifeMax - 1 ].activeSelf != true)
            {
                for (int i = 1; i <= life; i++)
                {
                    totalHp[i - 1].SetActive(true);
                }
            }
           
        }
        else
        {
            for (int i = lifeMax; i > life; i--)
            {
                totalHp[i - 1].SetActive(false);
            }
        }

    }

    public void activeSkill( string skill_name, List<int> results )
    {

        int cust = results[0] / 2 * 10;
        int effective = results[1] / 2 * 10;
        int critic = (results[2] + results[0])  / 2 * 10;

        print(cust + " x " + effective + " x " + critic);

        if ( effective > cust )
        {
            print("Efetivo");
            switch (skill_name)
            {
                case "Fire Storm":
                    directDamageSkill();
                    break;

                case "Fury":
                    battleDamageSkill();
                    break;

                case "Life Wind":
                    regenSkill();
                    break;
            }
        } else
        {
            print("Nada Efetivo");
        }
        
        if ( effective >= critic )
        {
            print("Crítico");
            skill = 0;
        } else
        {
            skill = 3;
        }

    }

    public void directDamageSkill()
    {
        bc.enemieHitted(damage[1]);
    }

    public void battleDamageSkill()
    {
        damage[0] += damage[1];
    }

    public void regenSkill()
    {
        life += damage[1];

        if (life > lifeMax)
        {
            life = lifeMax;
        }
        StartCoroutine(DelayHpLoad("recover"));
    }

    private IEnumerator DelayHpLoad(string value = "damage")
    {
        bc.canvaController.EndCanva();
        LifeCam.SetActive(true);
        // Delay de 2 segundos antes de carregar a barra de vida
        yield return new WaitForSeconds(2f);
        hpBarLoad();
        // Delay de 2 segundos após o carregamento da barra de vida
        yield return new WaitForSeconds(2f);
        bc.canvaController.Initiallize();
        LifeCam.SetActive(false);
    }

    public IEnumerator DelayDice(string skill_name, List<int> results)
    {
        DiceCam.SetActive(true);
        // Delay de 2 segundos antes de carregar a barra de vida
        yield return new WaitForSeconds(2f);
        bc.Dices[0].RolarDado(results[0]);
        bc.Dices[1].RolarDado(results[1]);
        bc.Dices[2].RolarDado(results[2]);
        // Delay de 2 segundos após o carregamento da barra de vida
        yield return new WaitForSeconds(2f);
        activeSkill(skill_name, results);
        bc.canvaController.Initiallize();
        DiceCam.SetActive(false);
    }

}
