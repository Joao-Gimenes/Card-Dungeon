using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    private Enemie enemie;
    private BC bc;
    private MC mc;

    public string classe, type;
    public int number,  skill, player = 1;
    public string lifebar;
    public float life, lifeMax;
    public string[] hab;
    public int[] damage;

    public Transform barHp;
    public GameObject ttHp;
   // public GameObject[] Skins;
    public TextMeshProUGUI lifeQuant;
    private Vector3 vetor;

    

    // Start is called before the first frame update
    void Start()
    {
        lifeQuant = ttHp.GetComponent<TextMeshProUGUI>();
        mc = FindObjectOfType(typeof(MC)) as MC;
        
        enemie = FindObjectOfType(typeof(Enemie)) as Enemie;
        bc = FindObjectOfType(typeof(BC)) as BC;

        barHp = GameObject.Find("lifebar").transform;

        life = mc.lifeP;
        hpBarLoad();

    }

    // Update is called once per frame
   public void tomarDano(int damage)
    {
        life -= damage;
        hpBarLoad();

        if (life <= 0)
        {
            life = 0;
            SceneManager.LoadSceneAsync("Dungeon");
        }

    }

    public void hpBarLoad()
    {
        float porc = life / lifeMax;
        vetor = barHp.transform.localScale;
        vetor.x = porc;
        barHp.transform.localScale = vetor;
    }

    public void activeSkill( string skill_name )
    {

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
        hpBarLoad();
    }


}
