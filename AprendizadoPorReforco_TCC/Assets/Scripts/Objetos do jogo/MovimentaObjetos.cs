using UnityEngine;
using System.Collections.Generic;

public class MovimentaObjetos : MonoBehaviour
{
    public GameObject meuPrefab; // Refer�ncia ao Prefab
    public GameObject objetoReferencia; // Refer�ncia ao objeto existente na cena

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
            // Obtendo a posi��o do objeto de refer�ncia
            Vector3 posicaoReferencia = objetoReferencia.transform.position;

            // Instanciando o Prefab na posi��o do objeto de refer�ncia e salvando uma refer�ncia para o objeto instanciado
            GameObject objetoInstanciado = Instantiate(meuPrefab, posicaoReferencia, Quaternion.identity);
            objetosInstanciados.Add(objetoInstanciado); // Adiciona o objeto � lista de objetos instanciados
        }
        else
        {
            Debug.LogError("Prefab ou objeto de refer�ncia n�o atribu�dos! Por favor, verifique se os objetos est�o corretamente referenciados no Inspector.");
        }
    }

    private void DestruirObjetosNaPosicaoZ()
    {
        for (int i = 0; i < objetosInstanciados.Count; i++)
        {
            GameObject objetoAtual = objetosInstanciados[i];
            // Verifica se o objeto instanciado existe e se sua posi��o Z � menor que -12
            if (objetoAtual != null && objetoAtual.transform.position.z < -12f)
            {
                Destroy(objetoAtual); // Destroi o objeto instanciado
                objetosInstanciados.RemoveAt(i); // Remove o objeto da lista de objetos instanciados
                i--; // Decrementa o contador para compensar a remo��o do objeto da lista
            }
        }
    }
}
