using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    private List<int> dice;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializa o deck com cartas de 1 a 10
        InitializeDice();
        ShuffleDice();  // Embaralha o deck inicialmente
    }

    // Método para inicializar o deck
    private void InitializeDice()
    {
        dice = new List<int>();
        for (int i = 1; i <= 20; i++)
        {
            dice.Add(i);
        }
    }


    public void ShuffleDice()
    {
        for (int i = dice.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1); // Gera um índice aleatório entre 0 e i
            // Troca a carta em i com a carta em j
            int temp = dice[i];
            dice[i] = dice[j];
            dice[j] = temp;
        }
    }

    // Método para comprar uma quantidade de cartas
    public List<int> PlayDice(int count)
    {
        List<int> hand = new List<int>();

        for (int i = 0; i < count; i++)
        {
            // Verifica se o deck e a mão está vazio, se sim, reembaralha
            if (dice.Count == 0)
            {
                InitializeDice();
                ShuffleDice();
            }

            // Remove a carta do topo do deck e adiciona na mão
            int card = dice[0];
            dice.RemoveAt(0);  // Remove a carta do topo
            hand.Add(card);
        }

        return hand;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
