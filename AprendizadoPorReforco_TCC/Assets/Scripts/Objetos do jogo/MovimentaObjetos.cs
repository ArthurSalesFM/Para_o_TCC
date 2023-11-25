using UnityEngine;
using System.Collections.Generic;

public class MovimentaObjetos : MonoBehaviour
{
    public GameObject meuPrefab; // Referência ao Prefab
    public GameObject objetoReferencia; // Referência ao objeto existente na cena

    private List<GameObject> objetosInstanciados = new List<GameObject>(); // Lista de objetos instanciados

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            InstanciarMeuPrefabNaPosicaoDoObjeto();
        }

        DestruirObjetosNaPosicaoZ();
    }

    private void InstanciarMeuPrefabNaPosicaoDoObjeto()
    {
        if (meuPrefab != null && objetoReferencia != null)
        {
            // Obtendo a posição do objeto de referência
            Vector3 posicaoReferencia = objetoReferencia.transform.position;

            // Instanciando o Prefab na posição do objeto de referência e salvando uma referência para o objeto instanciado
            GameObject objetoInstanciado = Instantiate(meuPrefab, posicaoReferencia, Quaternion.identity);
            objetosInstanciados.Add(objetoInstanciado); // Adiciona o objeto à lista de objetos instanciados
        }
        else
        {
            Debug.LogError("Prefab ou objeto de referência não atribuídos! Por favor, verifique se os objetos estão corretamente referenciados no Inspector.");
        }
    }

    private void DestruirObjetosNaPosicaoZ()
    {
        for (int i = 0; i < objetosInstanciados.Count; i++)
        {
            GameObject objetoAtual = objetosInstanciados[i];
            // Verifica se o objeto instanciado existe e se sua posição Z é menor que -12
            if (objetoAtual != null && objetoAtual.transform.position.z < -12f)
            {
                Destroy(objetoAtual); // Destroi o objeto instanciado
                objetosInstanciados.RemoveAt(i); // Remove o objeto da lista de objetos instanciados
                i--; // Decrementa o contador para compensar a remoção do objeto da lista
            }
        }
    }
}
