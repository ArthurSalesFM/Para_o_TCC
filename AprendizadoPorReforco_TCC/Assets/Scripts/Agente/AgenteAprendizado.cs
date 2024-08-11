using System;
using System.Collections.Generic;
using UnityEngine;

public class AgenteAprendizado : MonoBehaviour
{
    //public LineRenderer linhaDoRaio;  // Adicione o LineRenderer via Unity Inspector
    private ControleMovimentacaoIA controleDeAnimacaoIA;
    private bool pegandoOsDados = false;
    //private bool analisandoDados = false;
    private string[,] matrizDeObjetos;
    private float[] pontosOndeDeveIrIA;
    private int quantidadeMaximaDePontos = 0;
    private int pontoAtualDaIa = 3;
    private ArvoreBinaria arvoreBase = new ArvoreBinaria();

    private void Start()
    {
        this.controleDeAnimacaoIA = new ControleMovimentacaoIA();

        // Adicionar verificação para getVelocidadeCorrida()
        float velocidade = controleDeAnimacaoIA.getVelocidadeCorrida();
        if (velocidade <= 0)
        {
            Debug.LogError("Velocidade de corrida é inválida.");
            return;
        }
        //this.tabelaVelocidadeDeMudancaDePosicao.printaTabela();
    }

    void Update()
    {
        // A cada quadro, obtemos observações, tomamos decisões e executamos ações

        //bool observacao = ObterObservacao(out _);
        
    }

    //Retorna a matriz com as informações do campo setado
    public string[,] getDadosDaMatriz()
    {
        return this.matrizDeObjetos;
    }
    
    //
    public bool estaAnalisandoOsDados()
    {
        return this.pegandoOsDados;
    }

    public void setaMatrizParaAnalise(List<GameObject> objeto, GameObject [] pontosDeMovimentacao)
    {

        this.pegandoOsDados = true;
        this.matrizDeObjetos = new string[objeto.Count , 8];
        this.pontosOndeDeveIrIA = new float[pontosDeMovimentacao.Length];


        //Setando os valores de x como referencia de movimentação.
        for (int x = 0; x < pontosDeMovimentacao.Length; x++)
        {
            this.pontosOndeDeveIrIA[x] = pontosDeMovimentacao[x].transform.position.x; 
        }
        

        //Setando os tipos de objetos 
        for (int linha = 0; linha < objeto.Count; linha++)
        {
            
           if (objeto[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 0)
           {
                this.setaObstaculoNaMatriz(linha, objeto[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 1);
           }
           else if (objeto[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 1)
           {
                this.setaObstaculoNaMatriz(linha, objeto[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 5);
           }
           else if (objeto[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 2)
           {
                this.setaObstaculoNaMatriz(linha, objeto[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 5);
           }
           else if (objeto[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 3)
           {
                this.setaObstaculoNaMatriz(linha, objeto[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 5);
           }
           else if (objeto[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 4)
           {
                this.setaObstaculoNaMatriz(linha, objeto[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 4);
           }
           else if (objeto[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 5)
           {
                this.setaObstaculoNaMatriz(linha, objeto[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 2);
           }
           else if (objeto[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 6)
           {
                this.setaObstaculoNaMatriz(linha, objeto[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 3);
           }
           else if (objeto[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 7)
           {
                this.setaObstaculoNaMatriz(linha, objeto[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 3);
           }
           else if (objeto[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 8)
           {
                this.setaObstaculoNaMatriz(linha, objeto[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 4);
           }
           else if (objeto[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 9)
           {
                this.setaObstaculoNaMatriz(linha, objeto[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 2);
           }
           else if (objeto[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 10)
           {
                this.setaObstaculoNaMatriz(linha, objeto[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 5);
           }
           else if (objeto[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 11)
           {
                this.setaObstaculoNaMatriz(linha, objeto[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem(), 4);
           }
           else if (objeto[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 12)
           {
                this.setaVidaNaMatriz(linha, objeto[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem());
            }
           else if (objeto[linha].GetComponent<DadosDoObjeto>().getQualObjetoFoiInstanciado() == 13)
           {
                this.setaMoedaNaMatriz(linha, objeto[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem());
           }
            
        }
              
        string[,] melhorCaminhoMatriz = this.caminhoMaisVantajoso(matrizDeObjetos);

        this.imprimirMatriz(melhorCaminhoMatriz, "Caminho mais vantajoso");
        Debug.Log("\nQuantidade total de pontos possível : " + this.quantidadeMaximaDePontos );

        arvoreBase.Inserir(this.quantidadeMaximaDePontos, melhorCaminhoMatriz);


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
                    }
                    else
                    {
                        this.quantidadeMaximaDePontos += 30;
                    }
                    matriz[linha, coluna] = "!";
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
    private bool ObterObservacao(out string objetoDetectado)
    {
        // Lógica para detecção de objetos ao redor
        // ...
        objetoDetectado = null;
        return false;
    }

    // Função para executar ações com base nas observações
    private void ExecutarAcao()
    {
        // Lógica para executar ações com base nas observações
        string objetoDetectado;
        if (ObterObservacao(out objetoDetectado))
        {
            Debug.Log($"Objeto detectado: {objetoDetectado}. Executando ação...");
        }
    }

    // Função para tomar decisões com base nas observações
    private void TomarDecisao()
    {
        // Lógica para tomar decisões com base nas observações
        string objetoDetectado;
        if (ObterObservacao(out objetoDetectado))
        {
            switch (objetoDetectado)
            {
                case "moeda":
                    MoverEmDirecaoAoAlvo();  // Implemente essa função
                    break;
                case "barreira":
                    EvitarObstaculo();  // Implemente essa função
                    break;
                case "vida":
                    // Lógica para ação específica ao detectar um objeto "vida"
                    break;
                default:
                    // Lógica padrão ou nenhuma ação se nenhum objeto detectado
                    break;
            }
        }
    }

    private void MoverEmDirecaoAoAlvo()
    {
        // Exemplo: Movimenta o agente na direção do alvo (moeda)
        //transform.Translate(Vector3.forward * velocidadeMovimento * Time.deltaTime);
    }

    private void EvitarObstaculo()
    {
        // Exemplo: Gira o agente para evitar a barreira
        //transform.Rotate(Vector3.up * velocidadeRotacao * Time.deltaTime);
    }

}


