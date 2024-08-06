using System;
using UnityEngine;

public class DadosDoObjeto : MonoBehaviour
{
    private float velocidade = 1f;
    private int nivelDoObjeto;
    private int pontoDeOrigem;
    private float pontoFinalDeChegada = 23.7f;
    private float tempoPraChegar = 0.0f;
    private int qualObjetoFoiInstanciado;

    public void instaciarObjetos(float velocidade, int nivelDoObjeto, int pontoDeOrigem, int qualObjetoFoiInstanciado)
    {
        this.velocidade = velocidade * -1;
        this.nivelDoObjeto = nivelDoObjeto;
        this.pontoDeOrigem = pontoDeOrigem;
        this.qualObjetoFoiInstanciado = qualObjetoFoiInstanciado;
    }

    // Update is called once per frame
    void Update()
    {
        this.tempoPraChegar = (transform.position.z - this.pontoFinalDeChegada) / velocidade;
        // Movendo o objeto no eixo Z continuamente
        transform.Translate(Vector3.forward * Time.deltaTime * this.velocidade);        
    }

    public int getNivelObjetoObjeto()
    {
        return this.nivelDoObjeto;
    }

    public int getPontoDeOrigem()
    {
        return this.pontoDeOrigem;
    }

    public float getTempoRestanteParaChegar()
    {
        return Math.Abs( this.tempoPraChegar);
    }

    public int getQualObjetoFoiInstanciado()
    {
        return this.qualObjetoFoiInstanciado;
    }
}