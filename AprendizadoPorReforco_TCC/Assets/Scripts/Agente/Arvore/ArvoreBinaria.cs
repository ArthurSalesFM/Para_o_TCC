using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArvoreBinaria
{
    // Refer�ncia para o n� raiz da �rvore
    private NoPrincipalDaArvore raiz;

    // Construtor que inicializa a �rvore bin�ria
    public ArvoreBinaria()
    {
        this.raiz = null; // Inicialmente, a �rvore est� vazia
    }

    // M�todo recursivo para inserir um valor em uma sub�rvore
    // M�todo para inserir um valor na �rvore bin�ria
    public void Inserir(int valor, string[,] matriz)
    {
        // Se a �rvore estiver vazia, o novo n� ser� a raiz
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
        // Se o valor for menor que o valor do n� atual, vai para a esquerda
        if (valor < no.getPontuacaoMaximaDoCaminho())
        {
            if (no.getFilhoEsquerdo() == null)
            {
                no.setFilhoEsquerdo(new NoPrincipalDaArvore(valor, matriz)); // Insere como filho � esquerda
            }
            else
            {
                this.inserirRecursivo(no.getFilhoEsquerdo(), valor, matriz); // Continua buscando � esquerda
            }
        }
        else if (valor > no.getPontuacaoMaximaDoCaminho())
        {
            // Se o valor for maior que o valor do n� atual, vai para a direita
            if (no.getFilhoDireito() == null)
            {
                no.setFilhoDireito(new NoPrincipalDaArvore(valor, matriz)); // Insere como filho � direita
            }
            else
            {
                this.inserirRecursivo(no.getFilhoDireito(), valor, matriz); // Continua buscando � direita
            }
        }
        else
        {
            // Se o valor for igual ao valor do n� atual, apenas adiciona a matriz se n�o existir
            //no.AdicionarMatrizSeNaoExistir(matriz);
        }
    }


}
