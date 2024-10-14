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

        
        
        
        
       




    }

    // Update is called once per frame
   public void tomarDano(int damage)
    {
        life -= damage;
        float porc = life / lifeMax;


        vetor = barHp.transform.localScale;
        vetor.x = porc;
        barHp.transform.localScale = vetor;

        if (life <= 0)
        {
            life = 0;
            SceneManager.LoadSceneAsync("Dungeon");

        }

    }
}
