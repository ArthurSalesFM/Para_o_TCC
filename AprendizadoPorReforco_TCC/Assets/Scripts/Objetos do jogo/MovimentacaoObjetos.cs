using UnityEngine;

public class MovimentacaoObjetos : MonoBehaviour
{
    private float velocidade;

    public MovimentacaoObjetos(float velocidadePassada)
    {
        this.velocidade = velocidadePassada * -1;
    }

    // Update is called once per frame
    void Update()
    {
        // Movendo o objeto no eixo Z continuamente
        transform.Translate(Vector3.forward * Time.deltaTime * velocidade);
    }
}