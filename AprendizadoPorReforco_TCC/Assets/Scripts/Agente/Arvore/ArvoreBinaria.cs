using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArvoreBinaria
{
    // Referência para o nó raiz da árvore
    private NoPrincipalDaArvore raiz;

    // Construtor que inicializa a árvore binária
    public ArvoreBinaria()
    {
        this.raiz = null; // Inicialmente, a árvore está vazia
    }

    // Método recursivo para inserir um valor em uma subárvore
    // Método para inserir um valor na árvore binária
    public void Inserir(int valor, string[,] matriz)
    {
        // Se a árvore estiver vazia, o novo nó será a raiz
        if (this.raiz == null)
        {
            this.raiz = new NoPrincipalDaArvore(valor, matriz);
        }
        else
        {
            this.inserirRecursivo(this.raiz, valor, matriz);
        }
    }

    private void inserirRecursivo(NoPrincipalDaArvore no, int valor, string[,] matriz)
    {
        // Se o valor for menor que o valor do nó atual, vai para a esquerda
        if (valor < no.getPontuacaoMaximaDoCaminho())
        {
            if (no.getFilhoEsquerdo() == null)
            {
                no.setFilhoEsquerdo(new NoPrincipalDaArvore(valor, matriz)); // Insere como filho à esquerda
            }
            else
            {
                this.inserirRecursivo(no.getFilhoEsquerdo(), valor, matriz); // Continua buscando à esquerda
            }
        }
        else if (valor > no.getPontuacaoMaximaDoCaminho())
        {
            // Se o valor for maior que o valor do nó atual, vai para a direita
            if (no.getFilhoDireito() == null)
            {
                no.setFilhoDireito(new NoPrincipalDaArvore(valor, matriz)); // Insere como filho à direita
            }
            else
            {
                this.inserirRecursivo(no.getFilhoDireito(), valor, matriz); // Continua buscando à direita
            }
        }
        else
        {
            // Se o valor for igual ao valor do nó atual, apenas adiciona a matriz se não existir
            //no.AdicionarMatrizSeNaoExistir(matriz);
        }
    }


}
