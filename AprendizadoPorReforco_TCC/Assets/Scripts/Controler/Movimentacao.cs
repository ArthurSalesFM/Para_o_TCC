using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    private float velocidadeCaminhada = 5f;
    private float velocidadeCorrida = 17.5f;

    
    public void MovimentaPersonagem()
    {
        MoverObjetoLateralmente();
    }

    private void MoverObjetoLateralmente()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

        bool teclaShift_Pressionanda = Input.GetKey(KeyCode.LeftShift);

        float velocidadeAtual = teclaShift_Pressionanda ? velocidadeCorrida : velocidadeCaminhada;

        transform.position += movement * Time.deltaTime * velocidadeAtual;
    }
}
