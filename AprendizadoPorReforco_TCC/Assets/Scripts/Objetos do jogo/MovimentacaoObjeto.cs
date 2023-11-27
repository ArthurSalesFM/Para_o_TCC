using UnityEngine;

public class MovimentacaoObjeto : MonoBehaviour
{
    private float velocidade = 1f;
    private bool velocidadeSetada = false;

    // Update is called once per frame
    void Update()
    {
        // Movendo o objeto no eixo Z continuamente
        transform.Translate(Vector3.forward * Time.deltaTime * this.velocidade);        
    }
    
    public void setaVelocidadeDosObjetos(float velocidadePassada)
    {
        if (!this.velocidadeSetada)
        {
            this.velocidade = velocidadePassada * -1;
            this.velocidadeSetada = true;
        }        
    }
}