using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public float rolarVelocidade = 10f;  // Velocidade de rotação
    public float alturaPulo = 0.5f;      // Altura máxima do pulo
    public float duracaoPulo = 0.5f;     // Duração do pulo (em segundos)

    private bool estaRolando = false;
    private bool estaChacoalhando = false;
    private Quaternion rotacaoInicial;
    private Quaternion rotacaoFinal;
    private float tempoRolando;
    private float tempoPulo;
    private float tempoChacoalhada;
    private int valorFinal;

    public float intensidadeChacoalhada = 1f; // Intensidade da chacoalhada final
    public float duracaoChacoalhada = 0.5f; // Duração da chacoalhada


    // Dicionário com as rotações específicas para cada valor do dado
    private Dictionary<int, Quaternion> rotacoesPorFace;
    private Vector3 posicaoInicial;


    private void Start()
    {
        rotacaoInicial = Quaternion.Euler(-70, 205, -570); // Rotação inicial para face 1 voltada para cima
        posicaoInicial = transform.position;
        ConfigurarRotacoes();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RolarDado(Random.Range(1, 20));
        }

        if (estaRolando)
        {
            tempoRolando += Time.deltaTime;
            tempoPulo += Time.deltaTime;
            RolandoDado();
        }
        else if (estaChacoalhando)
        {
            tempoChacoalhada += Time.deltaTime;
            ChacoalhandoDado();
        }

    }

    public void RolarDado(int valorDesejado)
    {
        if (!estaRolando && !estaChacoalhando)
        {
            estaRolando = true;
            tempoRolando = 0;
            tempoPulo = 0;
            tempoChacoalhada = 0;
            valorFinal = valorDesejado;
        }
    }

    private void ConfigurarRotacoes()
    {
        // Configurações de rotação para cada face do dado (ajuste conforme necessário)
        rotacoesPorFace = new Dictionary<int, Quaternion>
        {
            {1, Quaternion.Euler(-70, 205, -600)},
            {2, Quaternion.Euler(-292, 205, -600)},
            {3, Quaternion.Euler(-385, 180, -735)},
            {4, Quaternion.Euler(-340, 65, -515)},
            {5, Quaternion.Euler(-540, -40, -760)},
            {6, Quaternion.Euler(-540, 150, -980)},
            {7, Quaternion.Euler(-150, -55, -830)},
            {8, Quaternion.Euler(35, 180, -15)},
            {9, Quaternion.Euler(-30, 180, 260)},
            {10, Quaternion.Euler(-205, 55, 215)},
            {11, Quaternion.Euler(-380, -75, 215)},
            {12, Quaternion.Euler(-205, 180, 260)},
            {13, Quaternion.Euler(-30, 180, 160)},
            {14, Quaternion.Euler(-215, 180, 80)},
            {15, Quaternion.Euler(0, 180, 100)},
            {16, Quaternion.Euler(0, 10, -35)},
            {17, Quaternion.Euler(-520, 25, -510)},
            {18, Quaternion.Euler(30, 0, -190)},
            {19, Quaternion.Euler(-70, 205, -785)},
            {20, Quaternion.Euler(-290, 115, -790)},
            // Continuar com as rotações para cada face do dado
        };
    }

    private void RolandoDado()
    {
        // Gira o dado em torno dos eixos para simular rolagem
        float rotacaoX = Random.Range(90f, 360f);
        float rotacaoY = Random.Range(90f, 360f);
        float rotacaoZ = Random.Range(90f, 360f);

        transform.Rotate(new Vector3(rotacaoX, rotacaoY, rotacaoZ) * Time.deltaTime * rolarVelocidade);

        float alturaAtual = Mathf.Sin((tempoPulo / duracaoPulo) * Mathf.PI) * alturaPulo;
        transform.position = posicaoInicial + new Vector3(0, alturaAtual, 0);

        if (tempoRolando >= 1f)
        {
            AjustarParaValorFinal();
            estaRolando = false;
            estaChacoalhando = true; // Inicia a chacoalhada após o rolamento
        }
    }

    private void AjustarParaValorFinal()
    {
        if (rotacoesPorFace.ContainsKey(valorFinal))
        {
            rotacaoFinal = rotacoesPorFace[valorFinal];
            transform.rotation = rotacaoFinal;
            transform.position = posicaoInicial; // Restaura a posição inicial ao final
        }
        else
        {
            Debug.LogWarning("Valor do dado não tem uma rotação configurada!");
        }
    }

    private void ChacoalhandoDado()
    {
        // Reduz a intensidade da chacoalhada ao longo do tempo
        float fatorIntensidade = Mathf.Lerp(intensidadeChacoalhada, 0, tempoChacoalhada / duracaoChacoalhada);

        // Gera uma leve rotação aleatória para simular a chacoalhada
        float rotacaoX = Random.Range(-fatorIntensidade, fatorIntensidade);
        float rotacaoY = Random.Range(-fatorIntensidade, fatorIntensidade);
        float rotacaoZ = Random.Range(-fatorIntensidade, fatorIntensidade);

        transform.rotation = rotacaoFinal * Quaternion.Euler(rotacaoX, rotacaoY, rotacaoZ);

        // Finaliza a chacoalhada após a duração especificada
        if (tempoChacoalhada >= duracaoChacoalhada)
        {
            transform.rotation = rotacaoFinal; // Garante que termina na rotação final exata
            estaChacoalhando = false;
            Debug.Log("Rolagem Finalizada! Valor final: " + valorFinal);
        }
    }

}
