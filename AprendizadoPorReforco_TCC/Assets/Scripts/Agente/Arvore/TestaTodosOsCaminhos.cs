using System;
using System.Collections.Generic;
using UnityEngine;

public class TestaTodosOsCaminhos
{
    private string[,] shapeBase;
    private List<string[,]> caminhosPossiveis = new List<string[,]>();
    private List<int?> pontosTotais = new List<int?>();

    public void setaValores(string[,] matriz)
    {
        this.setShapeBase(matriz);

        if (this.caminhosPossiveis.Count == 0)
        {
            GerarTodosCaminhos(matriz);
        }

        Debug.Log("*************************** %TODOS OS CAMINHOS% ***************************\n\n");
        this.ImprimirCaminhosPossiveis();
    }

    public string[,] getShapeBase()
    {
        return this.shapeBase;
    }

    private void setShapeBase(string[,] matrizBase)
    {
        this.shapeBase = matrizBase;
    }

    private void GerarTodosCaminhos(string[,] matriz)
    {
        string[,] caminho = new string[matriz.GetLength(0), matriz.GetLength(1)];

        //this.caminhosPossiveis.Add(criaCaminho(caminho, 0, caminho.GetLength(1)) );
    }

    /*
    private string[,] criaCaminho(string[,] matriz, int inicio, int fim)
    {


        
        return caminhos;
    }
    */

    private void ImprimirCaminhosPossiveis()
    {

        //string caminho = "";
        string saidaGeral = "";
        for (int inicio = 0; inicio < this.caminhosPossiveis.Count; inicio++)
        {
            //Debug.Log("Caminho " + inicio);
            saidaGeral += ImprimirMatriz(this.caminhosPossiveis[inicio]);
        }

        saidaGeral += "\n\n";

        Debug.Log(saidaGeral);

    }

    private string ImprimirMatriz(string[,] matriz)
    {
        string saida = "";
        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                saida += matriz[i, j] + " | ";
            }
            saida += "\n";
        }

        saida += "\n*";

        return saida;
    }

}



/*
using System;
using System.Collections.Generic;
using UnityEngine;

public class TestaTodosOsCaminhos
{
    private string[,] shapeBase;
    private List<string[,]> caminhosPossiveis = new List<string[,]>();
    private List<int?> pontosTotais = new List<int?>();

    public void setaValores(string[,] matriz)
    {
        this.setShapeBase(matriz);


        if (this.caminhosPossiveis.Count == 0)
        {
            GerarTodosCaminhos(matriz);
        }

        Debug.Log("*************************** %TODOS OS CAMINHOS% ***************************\n\n");
        this.ImprimirCaminhosPossiveis();
    }
    
    public string[,] getShapeBase()
    {
        return this.shapeBase;
    }

    private void setShapeBase(string[,] matrizBase)
    {
        this.shapeBase = matrizBase;
    }

    private void GerarTodosCaminhos(string[,] matriz)
    {
        string[,] caminho = new string[matriz.GetLength(0), matriz.GetLength(1)];              
     
        this.caminhosPossiveis = CriarTodosOsCaminhos(caminho, 0, caminho.GetLength(0));//kjkjhkhs)
        //this.ImprimirCaminhosPossiveis();
    }


    private List<string[,]> CriarTodosOsCaminhos(string[,] matriz, int inicio, int fim)
    {
        List<string[,]> caminhos = new List<string[,]>();  // Lista para armazenar todas as matrizes completas

        // Condição de parada: matriz com apenas uma linha
        if (fim - inicio <= 1)
        {
            // Adiciona uma cópia da matriz à lista
            string[,] caminho = new string[matriz.GetLength(0), matriz.GetLength(1)];
            Array.Copy(matriz, caminho, matriz.Length);
            caminhos.Add(caminho);
            return caminhos;
        }

        // Divida a matriz em duas partes e processe cada parte
        int meio = (inicio + fim) / 2;

        caminhos.AddRange(CriarTodosOsCaminhos(matriz, inicio, meio));
        caminhos.AddRange(CriarTodosOsCaminhos(matriz, meio, fim));

        // Processar a matriz 'caminho' para o intervalo atual
        string[,] caminhoAtual = new string[matriz.GetLength(0), matriz.GetLength(1)];
        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                if (j >= inicio && j < meio)
                {
                    caminhoAtual[i, j] = "!";
                }
                else
                {
                    caminhoAtual[i, j] = ".";
                }
            }
        }

        // Adiciona a matriz 'caminhoAtual' à lista de caminhos
        caminhos.Add(caminhoAtual);

        return caminhos;
    }

    private void ImprimirCaminhosPossiveis()
    {

        //string caminho = "";
        string saidaGeral = "";
        for(int inicio = 0; inicio < this.caminhosPossiveis.Count; inicio++)
        {
            //Debug.Log("Caminho " + inicio);
            saidaGeral += ImprimirMatriz(this.caminhosPossiveis[inicio]);
        }

        saidaGeral += "\n\n";

        Debug.Log(saidaGeral);
        
    }

    private string ImprimirMatriz(string[,] matriz)
    {
        string saida = "";
        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for(int j = 0; j < matriz.GetLength(1); j++)
            {
                saida += matriz[i, j] + " | ";
            }
            saida += "\n";
        }

        saida += "\n*";

        return saida;
    }
}
*/