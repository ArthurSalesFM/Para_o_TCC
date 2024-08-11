using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections.Generic;
using UnityEngine;

public class TestaTodosOsCaminhos
{
    private string[,] shapeBase;
    private List<string[,]> caminhosPossiveis = new List<string[,]>();
    private List<int?> pontosTotais = new List<int?>();

    /*
    public TestaTodosOsCaminhos(string[,] matriz)
    {
        this.shapeBase = (string[,])matriz.Clone();
        this.caminhosPossiveis = new List<string[,]>();
        this.pontosTotais = new List<int?>();

        int tamanho = matriz.GetLength(0);


        // Inicializa a matriz com espaços em branco
        for (int i = 0; i < tamanho; i++)
        {
            for (int j = 0; j < tamanho; j++)
            {
                matriz[i, j] = " ";
            }
        }
        GerarTodosCaminhos(matriz);

        Debug.Log("*************************** %TODOS OS CAMINHOS% ***************************\n\n");
        this.ImprimirCaminhosPossiveis();
    }
    */

    public void setaValores(string[,] matriz)
    {
        this.shapeBase = matriz;


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

    public void setShapeBase(string[,] matrizBase)
    {
        this.shapeBase = matrizBase;
    }

    private void GerarTodosCaminhos(string[,] matriz)
    {
        // Faz uma cópia da matriz inicial para evitar modificações indesejadas
        //string[,] matrizCopia = (string[,])matriz.Clone();asdadsads
        ExplorarCaminhos(matriz, 0, 0);kjkjhkhs
    }

    private void ExplorarCaminhos(string[,] matriz, int linha, int coluna)
    {
        // Marca a posição atual com "!"
        matriz[linha, coluna] = "!";

        // Se todos os passos foram dados, armazena a matriz atual na lista de caminhos
        if (VerificarTodosEspacosPreenchidos(matriz))
        {
            string[,] caminhoCompleto = (string[,])matriz.Clone();
            this.caminhosPossiveis.Add(caminhoCompleto);
            pontosTotais.Add(null); // Adiciona um valor null à lista pontosTotais
        }
        else
        {
            // Movimentos possíveis: para baixo, para a direita, para cima, para a esquerda
            if (linha + 1 < matriz.GetLength(0) && matriz[linha + 1, coluna] == " ")
            {
                ExplorarCaminhos(matriz, linha + 1, coluna);
            }

            if (coluna + 1 < matriz.GetLength(1) && matriz[linha, coluna + 1] == " ")
            {
                ExplorarCaminhos(matriz, linha, coluna + 1);
            }

            if (linha - 1 >= 0 && matriz[linha - 1, coluna] == " ")
            {
                ExplorarCaminhos(matriz, linha - 1, coluna);
            }

            if (coluna - 1 >= 0 && matriz[linha, coluna - 1] == " ")
            {
                ExplorarCaminhos(matriz, linha, coluna - 1);
            }
        }

        // Desmarca a posição atual antes de retornar (backtracking)
        matriz[linha, coluna] = " ";
    }

    private bool VerificarTodosEspacosPreenchidos(string[,] matriz)
    {
        // Verifica se todos os espaços na matriz estão preenchidos com "!"
        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                if (matriz[i, j] == " ")
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void ImprimirCaminhosPossiveis()
    {

        //string caminho = "";
        string saidaGeral = "";
        for(int inicio = 0; inicio < this.caminhosPossiveis.Count; inicio++)
        {
            Debug.Log("Caminho " + inicio);
            saidaGeral+=ImprimirMatriz(this.caminhosPossiveis[inicio]);
        }

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

        saida += "\n*\n";

        return saida;
    }
}
