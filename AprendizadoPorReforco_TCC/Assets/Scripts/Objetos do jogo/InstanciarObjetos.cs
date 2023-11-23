using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciarObjetos : MonoBehaviour
{
   
    public GameObject meuPrefab; // Refer�ncia ao Prefab
    public GameObject objetoReferencia; // Refer�ncia ao objeto existente na cena

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            InstanciarMeuPrefabNaPosicaoDoObjeto();
        }
    }

    void InstanciarMeuPrefabNaPosicaoDoObjeto()
    {
        if (meuPrefab != null && objetoReferencia != null)
        {
            // Obtendo a posi��o do objeto de refer�ncia
            Vector3 posicaoReferencia = objetoReferencia.transform.position;

            // Instanciando o Prefab na posi��o do objeto de refer�ncia
            Instantiate(meuPrefab, posicaoReferencia, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab ou objeto de refer�ncia n�o atribu�dos! Por favor, verifique se os objetos est�o corretamente referenciados no Inspector.");
        }
    }
}

