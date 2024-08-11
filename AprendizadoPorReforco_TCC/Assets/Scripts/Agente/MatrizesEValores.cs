using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrizesEValores : MonoBehaviour
{
    private int valorTotal = 0;
    private string[,] matrizReferente;
    

    public MatrizesEValores(int valorTotal, string[,] Caminho)
    {
        this.valorTotal = valorTotal;
        this.matrizReferente = Caminho;
    }

    public int getValorTotal()
    {
        return this.valorTotal;
    }

    public string[,] getMatrizDeCaminho()
    {
        return this.matrizReferente;
    }


}
