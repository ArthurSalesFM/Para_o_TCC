using System;
using System.Collections.Generic;
using UnityEngine;

public class AgenteAprendizado : MonoBehaviour
{
    //public LineRenderer linhaDoRaio;  // Adicione o LineRenderer via Unity Inspector
    private ControleAnimacaoIA controleDeAnimacaoIA;
    private bool processandoAsInformacoes = false;
    private bool executandoAnimacoes = false;
    //private TabelaVelocidadeDeMudancaDePosicao velocidadesMovimentacao = new TabelaVelocidadeDeMudancaDePosicao();
    private float[,] tabelaDeVelocidade;
    /// <summary>
    private bool analisandoDados = false;
    /// </summary>
    private string[,] matrizDeObjetos;
    private string[,] melhorCaminhoMatriz;
    private List<GameObject> listaDeObjetos = new List<GameObject>();
    private float[] pontosOndeDeveIrIA;
    private int posicaoDoObjeto = 0;
    private int posicaoAtualDaIa = 3;
    private int posicaoQueAIADeveIr;
    private int quantidadeMaximaDePontos = 0;
    //private int pontoAtualDaIa = 3;
    //private ArvoreBinaria arvoreBase = new ArvoreBinaria();

    private void Start()         
    {
        this.controleDeAnimacaoIA = GetComponent<ControleAnimacaoIA>();

        // Adicionar verificação para getVelocidadeCorrida()
        float velocidade = controleDeAnimacaoIA.getVelocidadeCorrida();
        
        if (velocidade <= 0)
        {
            Debug.LogError("Velocidade de corrida é inválida.");
            return;
        }
        //this.tabelaVelocidadeDeMudancaDePosicao.printaTabela();
        //this.controleDeAnimacaoIA.AtivaDesativaTeclas(3);
    }

    private void Update()
    {

        if(melhorCaminhoMatriz != null)
        {
            ExecutarAcao(melhorCaminhoMatriz);
        }
        
    }

    //Retorna a matriz com as informações do campo setado
    public string[,] getDadosDaMatriz()
    {
        return this.matrizDeObjetos;
    }
    
    //
    public bool estaAnalisandoOsDados()
    {
        return this.processandoAsInformacoes;
    }

    public void setaMatrizParaAnalise(List<GameObject> objetos, GameObject [] pontosDeMovimentacao)
    {
        //transform.position = new Vector3(998.3f, transform.position.y, transform.position.z);
        //Debug.Log("2Posicao do personagem em x: " + transform.position.x);
        this.processandoAsInformacoes = true;
        this.listaDeObjetos = objetos;
        this.matrizDeObjetos = new string[objetos.Count , 8];
        this.pontosOndeDeveIrIA = new float[pontosDeMovimentacao.Length];
        //this.velocidadesMovimentacao.setaValoresNaTabela(pontosDeMovimentacao, 12.5f);
        //this.tabelaDeVelocidade = this.velocidadesMovimentacao.getTabelaDeVelocidadeDeMudancaDePosicao();
        //Debug.Log("3Posicao do personagem em x: " + transform.position.x);


        //Setando os valores de x como referencia de movimentação.
        for (int x = 0; x < pontosDeMovimentacao.Length; x++)
        {
            this.pontosOndeDeveIrIA[x] = pontosDeMovimentacao[x].transform.position.x; 
        }

        
       // Debug.Log("4Posicao do personagem em x: " + transform.position.x);

        //Setando os tipos de objetos 
        for (int linha = 0; linha < objetos.Count; linha++)
        {
            
           if (objetos[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 0)
           {
                this.setaObstaculoNaMatriz(linha, objetos[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 1);
           }
           else if (objetos[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 1)
           {
                this.setaObstaculoNaMatriz(linha, objetos[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 5);
           }
           else if (objetos[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 2)
           {
                this.setaObstaculoNaMatriz(linha, objetos[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 5);
           }
           else if (objetos[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 3)
           {
                this.setaObstaculoNaMatriz(linha, objetos[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 5);
           }
           else if (objetos[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 4)
           {
                this.setaObstaculoNaMatriz(linha, objetos[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 4);
           }
           else if (objetos[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 5)
           {
                this.setaObstaculoNaMatriz(linha, objetos[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 2);
           }
           else if (objetos[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 6)
           {
                this.setaObstaculoNaMatriz(linha, objetos[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 3);
           }
           else if (objetos[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 7)
           {
                this.setaObstaculoNaMatriz(linha, objetos[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 3);
           }
           else if (objetos[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 8)
           {
                this.setaObstaculoNaMatriz(linha, objetos[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 4);
           }
           else if (objetos[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 9)
           {
                this.setaObstaculoNaMatriz(linha, objetos[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 2);
           }
           else if (objetos[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 10)
           {
                this.setaObstaculoNaMatriz(linha, objetos[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 5);
           }
           else if (objetos[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 11)
           {
                this.setaObstaculoNaMatriz(linha, objetos[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 4);
           }
           else if (objetos[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 12)
           {
                this.setaVidaNaMatriz(linha, objetos[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem());
            }
           else if (objetos[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 13)
           {
                this.setaMoedaNaMatriz(linha, objetos[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem());
           }
            
        }
              
        this.melhorCaminhoMatriz = this.caminhoMaisVantajoso(matrizDeObjetos);
        Debug.Log("\ninfo....");
        this.imprimirMatriz(this.melhorCaminhoMatriz, "Caminho mais vantajoso");
        Debug.Log("\nQuantidade total de pontos possível : " + this.quantidadeMaximaDePontos );

        this.analisandoDados = true;
        //ExecutarAcao(this.melhorCaminhoMatriz);

    }    

    /*
                PARA SETAR INFORMAÇÕES NA MATRIZ
     */
    private void setaObstaculoNaMatriz(int indiceLinha, int colunaInicial, int quantidadeDePontosOcupados)
    {
        int pontosUsados = 0;        
        for (int comeca = colunaInicial; comeca < 8; comeca++)
        {
            if(quantidadeDePontosOcupados == 3)
            {
                if (pontosUsados < 2)
                {
                    this.matrizDeObjetos[indiceLinha , comeca] = "X";
                    pontosUsados++;
                }
                else if(pontosUsados == 2)
                {
                    pontosUsados++;
                }
                else if(pontosUsados == 3)
                {
                    this.matrizDeObjetos[indiceLinha, comeca] = "X";
                    pontosUsados++;
                }
                else
                {
                    pontosUsados = 0;
                    return;
                }
                
            }

            else
            {
                if (pontosUsados < quantidadeDePontosOcupados)
                {
                    this.matrizDeObjetos[indiceLinha , comeca] = "X";
                    pontosUsados++;
                }
                else
                {
                    pontosUsados = 0;
                    return;
                }
            }
                        
        }
    }

    private void setaMoedaNaMatriz(int indiceLinha, int colunaInicial)
    {
        this.matrizDeObjetos[indiceLinha , colunaInicial] = "$";
    }

    private void setaVidaNaMatriz(int indiceLinha, int colunaInicial)
    {
        this.matrizDeObjetos[indiceLinha, colunaInicial] = "+";
    }

    //**************************************************************************************************************************************

    private void imprimirMatriz(string[,] matriz, string texto)
    {
        string saida = "\n*************************************" + texto + "*************************************\n";

        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                saida += matriz[i, j] + " ";
            }
            saida += "\n";  // Pula uma linha após cada linha da matriz
        }

        Debug.Log(saida);
    }

    private string[,] caminhoMaisVantajoso(string[,] matriz)
    {
        for(int linha = 0; linha < matriz.GetLength(0); linha++)
        {
            for(int coluna = 0; coluna < matriz.GetLength(1); coluna++)
            {
                if (matriz[linha, coluna] == "$" || matriz[linha, coluna] == "+")
                {
                    if (matriz[linha, coluna] == "$")
                    {
                        this.quantidadeMaximaDePontos += 10;
                        matriz[linha, coluna] = "$";
                    }
                    else
                    {
                        this.quantidadeMaximaDePontos += 30;
                        matriz[linha, coluna] = "+";
                    }
                    
                }
                else if (matriz[linha, coluna] == "" || matriz[linha, coluna] == null)
                {
                    matriz[linha, coluna] = " . ";
                }
                else
                {
                    for (int coluna2 = 0; coluna2 < matriz.GetLength(1); coluna2++)
                    {
                        if (matriz[linha, coluna2] == "" || matriz[linha, coluna2] == " . " || matriz[linha, coluna2] == null)
                        {
                            matriz[linha, coluna2] = "!";
                        }
                    }
                    this.quantidadeMaximaDePontos += 1;
                    coluna = matriz.GetLength(1) + 10;
                }
            }
        }

        return matriz;
    }


    //**************************************************************************************************************************************

    // Função para obter observações do ambiente
    /*private bool ObterObservacao(out string objetoDetectado)
    {
        // Lógica para detecção de objetos ao redor
        // ...
        objetoDetectado = null;
        return false;
    }
    */

    // Função para executar ações com base nas observações
    private void ExecutarAcao(string[,] matriz)
    {
        
            if (!executandoAnimacoes)
            {
                int posicao = -1;
                List<int> posicoes = new List<int>();

                for (int inic = 0; inic < 8; inic++)
                {

                    //Debug.Log(matriz[posicaoDoObjeto, inic]);
                    if (matriz[this.posicaoDoObjeto, inic] == "$")
                    {
                        posicao = inic;
                        inic = 99;
                    }
                    else if (matriz[this.posicaoDoObjeto, inic] == "+")
                    {
                        posicao = inic;
                        inic = 99;
                    }
                    else
                    {
                        if (matriz[this.posicaoDoObjeto, inic] == "!")
                        {
                            posicoes.Add(inic);
                        }
                    }
                }

                if (posicao < 0)
                {
                    System.Random random = new System.Random();
                    posicao = posicoes[random.Next(0, posicoes.Count)];
                }
                this.posicaoQueAIADeveIr = posicao;
                executandoAnimacoes = true;
            }
            else
            {
                if (transform.position.x != pontosOndeDeveIrIA[this.posicaoQueAIADeveIr])
                {
                    if (transform.position.x > pontosOndeDeveIrIA[this.posicaoQueAIADeveIr])
                    {
                        this.controleDeAnimacaoIA.AtivaDesativaTeclas(0);
                        this.controleDeAnimacaoIA.MovimentaPersonagem();
                }
                    else if (transform.position.x < pontosOndeDeveIrIA[this.posicaoQueAIADeveIr])
                    {
                        this.controleDeAnimacaoIA.AtivaDesativaTeclas(1);
                        this.controleDeAnimacaoIA.MovimentaPersonagem();
                    }
                    else if(transform.position.x == pontosOndeDeveIrIA[this.posicaoQueAIADeveIr])
                    {
                        this.controleDeAnimacaoIA.AtivaDesativaTeclas(3);
                        this.controleDeAnimacaoIA.MovimentaPersonagem();
                        this.executandoAnimacoes = false;
                    }
                }
            }
        

        
        
        //this.analisandoDados = false;

    }


    private float[] getMaiorTempo(int posicaoDaIA)
    {
        float[] tempos = new float[2];
        float menorValor = 9999999999999.0f;
        float maiorValor = -9999999999999.0f;

        for (int i = 0; i < this.tabelaDeVelocidade.GetLength(1); i++)
        {
            if(menorValor > this.tabelaDeVelocidade[posicaoDaIA, i])
            {
                menorValor = this.tabelaDeVelocidade[posicaoDaIA, i];
            }

            if (maiorValor < this.tabelaDeVelocidade[posicaoDaIA, i])
            {
                maiorValor = this.tabelaDeVelocidade[posicaoDaIA, i];
            }
            
        }

        tempos[0] = menorValor;
        tempos[1] = maiorValor;
        return tempos;
    }

    // Função para tomar decisões com base nas observações
    private void TomarDecisao()
    {

        //controleDeAnimacaoIA
    }

    private void MoverEmDirecaoAoAlvo()
    {
        // Exemplo: Movimenta o agente na direção do alvo (moeda)
        //transform.Translate(Vector3.forward * velocidadeMovimento * Time.deltaTime);
    }

    

}


