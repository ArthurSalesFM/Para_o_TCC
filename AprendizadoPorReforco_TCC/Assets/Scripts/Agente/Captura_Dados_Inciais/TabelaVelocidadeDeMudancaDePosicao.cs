using UnityEngine;

public class TabelaVelocidadeDeMudancaDePosicao
{
    private float[,] tabelaDeVelocidadeDeMudancaDePosicao;


    public void setaValoresNaTabela(GameObject[] posicoes, float velocidadeDeCorrida)
    {
        int numPosicoes = posicoes.Length;
        tabelaDeVelocidadeDeMudancaDePosicao = new float[numPosicoes, numPosicoes];

        for (int linha = 0; linha < numPosicoes; linha++)
        {
            for (int coluna = 0; coluna < numPosicoes; coluna++)
            {
                if (linha == coluna)
                {
                    this.tabelaDeVelocidadeDeMudancaDePosicao[linha, coluna] = 0.0f;
                }
                else
                {
                    // Calcula a distância entre os dois pontos (usando a posição x para simplificação)
                    float espaco = Mathf.Abs(posicoes[linha].transform.position.x - posicoes[coluna].transform.position.x);

                    // Calcula o tempo com base na velocidade de corrida
                    float tempo = espaco / velocidadeDeCorrida;
                    this.tabelaDeVelocidadeDeMudancaDePosicao[linha, coluna] = tempo;
                }
            }
        }
    }

    public float[,] getTabelaDeVelocidadeDeMudancaDePosicao()
    {
        return this.tabelaDeVelocidadeDeMudancaDePosicao;
    }

    public void printaTabela()
    {
        int linhas = tabelaDeVelocidadeDeMudancaDePosicao.GetLength(0);
        int colunas = tabelaDeVelocidadeDeMudancaDePosicao.GetLength(1);
        string linhaSaida = "";

        for (int linha = 0; linha < linhas; linha++)
        {
            //Debug.Log("Linha " + linha + " - \n");
            for (int coluna = 0; coluna < colunas; coluna++)
            {
                linhaSaida += tabelaDeVelocidadeDeMudancaDePosicao[linha, coluna] + " | ";                
            }
            Debug.Log("Linha " + linha +" : " + linhaSaida);
            Debug.Log("\n");
            linhaSaida = "";
            System.Console.WriteLine();
        }
    }
}
