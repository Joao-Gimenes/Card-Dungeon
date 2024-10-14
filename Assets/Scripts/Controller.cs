using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Controller", menuName = "Controller/New Controller", order = 1)]
public class Controller : ScriptableObject
{
    // Start is called before the first frame update

    public string perso;
    public int inimigo, terrain, players;
    public int level = 1;
    public int copas = 0, espadas = 0, ouros = 0, paus = 0;
    public int lifeP;
    public int skillDmg = 1;

    // Start is called before the first frame update
    


}
