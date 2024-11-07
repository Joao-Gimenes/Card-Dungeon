using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private List<int> deck;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializa o deck com cartas de 1 a 10
        InitializeDeck();
        ShuffleDeck();  // Embaralha o deck inicialmente
    }

    // Método para inicializar o deck
    private void InitializeDeck()
    {
        deck = new List<int>();
        for (int i = 1; i <= 10; i++)
        {
            deck.Add(i);
        }
    }


    public void ShuffleDeck()
    {
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1); // Gera um índice aleatório entre 0 e i
            // Troca a carta em i com a carta em j
            int temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
    }

    // Método para comprar uma quantidade de cartas
    public List<int> DrawCards(int count)
    {
        List<int> hand = new List<int>();

        for (int i = 0; i < count; i++)
        {
            // Verifica se o deck e a mão está vazio, se sim, reembaralha
            if (deck.Count == 0)
            {
                InitializeDeck();
                ShuffleDeck();
            }

            // Remove a carta do topo do deck e adiciona na mão
            int card = deck[0];
            deck.RemoveAt(0);  // Remove a carta do topo
            hand.Add(card);
        }

        return hand;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
