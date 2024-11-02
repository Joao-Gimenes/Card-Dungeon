using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Enemie : MonoBehaviour
{
    private Player player;
    private BC bc;
    private MC mc;


   // public string name;
    public float lifeMax, life;
    public int quant;
    
    public string[] hab;
    public int damage;
    private Vector3 vetor;

    private Transform barHp;
    public GameObject ttHp;
    public TextMeshProUGUI lifeQuant;

    // Start is called before the first frame update
    void Start()
    {

        lifeQuant = ttHp.GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType(typeof(Player)) as Player;
        bc = FindObjectOfType(typeof(BC)) as BC;
        mc = FindObjectOfType(typeof(MC)) as MC;

        barHp = GameObject.Find("Lfe").transform;
        life = lifeMax;

    }

    // Update is called once per frame
    public void tomarDano(int damage)
    {
        life -= damage;
        hpBarLoad();


        if (life <= 0)
        {
            life = 0;

        }
    }
    public void hpBarLoad()
    {
        float porc = life / lifeMax;
        vetor = barHp.transform.localScale;
        vetor.x = porc;
        barHp.transform.localScale = vetor;
    }

}
