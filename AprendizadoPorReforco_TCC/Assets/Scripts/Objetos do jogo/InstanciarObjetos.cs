using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciarObjetos : MonoBehaviour
{
   
    public GameObject meuPrefab; // Referência ao Prefab
    public GameObject objetoReferencia; // Referência ao objeto existente na cena

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
            // Obtendo a posição do objeto de referência
            Vector3 posicaoReferencia = objetoReferencia.transform.position;

            // Instanciando o Prefab na posição do objeto de referência
            Instantiate(meuPrefab, posicaoReferencia, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab ou objeto de referência não atribuídos! Por favor, verifique se os objetos estão corretamente referenciados no Inspector.");
        }
    }
}

