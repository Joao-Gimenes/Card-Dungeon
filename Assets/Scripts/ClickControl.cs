using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ClickControl : MonoBehaviour
{
    private NavMeshAgent agente;
    // Start is called before the first frame update
    void Start()
    {
        agente = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectarCliqueParaMovimentacao();
        }
    }
    private void DetectarCliqueParaMovimentacao()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            agente.SetDestination(hit.point);
            //acoesRestantes--; // Cada movimento consome uma ação
        }
    }
}
