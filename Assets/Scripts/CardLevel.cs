using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLevel : MonoBehaviour
{
    public GameObject[] card_unlockeds;
    public bool canUse;
    public int numberPosition;
    public int value;
    public int nipe;

    public BattleControiller battleControiller;
    public float alturaSalto = 1f;         // Altura do salto
    public float duracaoSalto = 0.5f;      // Duração do salto
    public float duracaoRotacao = 0.5f;    // Duração da rotação

    private bool estaPulando = false;

    // Start is called before the first frame update
    void Start()
    {
        battleControiller = FindObjectOfType(typeof(BattleControiller)) as BattleControiller;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && canUse == true) // Detecta o clique com o botão esquerdo do mouse
        {
            DetectarClique();
        }

    }

    private void DetectarClique()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Cria um ray a partir da posição do mouse
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) // Dispara o raycast
        {
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Chama o evento quando o raycast atinge o objeto
                canUse = false;
                StartCoroutine(SaltarEGirar());
            }
        }
    }

    private IEnumerator SaltarEGirar()
    {
        estaPulando = true;
        Vector3 posicaoInicial = transform.position;
        Quaternion rotacaoInicial = transform.rotation;
        Quaternion rotacaoFinal = rotacaoInicial * Quaternion.Euler(0, 0, 180); // Rotação de 180º no eixo Y

        float tempo = 0;

        while (tempo < duracaoSalto)
        {
            tempo += Time.deltaTime;
            float progresso = tempo / duracaoSalto;

            // Calcula a posição do salto usando uma curva senoidal
            float alturaAtual = Mathf.Sin(progresso * Mathf.PI) * alturaSalto;
            transform.position = posicaoInicial + new Vector3(0, alturaAtual, 0);

            yield return null;
        }

        transform.position = posicaoInicial; // Restaura a posição inicial ao final do salto

        // Reinicia o tempo para a rotação
        tempo = 0;

        while (tempo < duracaoRotacao)
        {
            tempo += Time.deltaTime;
            float progresso = tempo / duracaoRotacao;

            // Interpola a rotação entre o início e o final
            transform.rotation = Quaternion.Slerp(rotacaoInicial, rotacaoFinal, progresso);

            yield return null;
        }

        transform.rotation = rotacaoFinal; // Garante a rotação final precisa

        estaPulando = false; // Permite outro clique para nova animação
        battleControiller.actualCard = this.gameObject;
        StartBattle(value);
    }

    public void StartBattle(int enemyCount)
    {
        // Ativa o BattleController e passa os parâmetros de batalha
        battleControiller.InitializeBattle(enemyCount);
    }

    public void Winned()
    {
        for(int i = 0; i < card_unlockeds.Length; i++)
        {
            card_unlockeds[i].GetComponent<CardLevel>().canUse = true;
        } 

    }

}
