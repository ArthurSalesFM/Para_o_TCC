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
    private int pontoAtualDaIa = 3;

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
        this.matrizDeObjetos = new string[objeto.Count + 1, 8];
        this.pontosOndeDeveIrIA = new float[pontosDeMovimentacao.Length];


        //Setando os valores de x como referencia de movimentação.
        for (int x = 0; x < pontosDeMovimentacao.Length; x++)
        {
            this.pontosOndeDeveIrIA[x] = pontosDeMovimentacao[x].transform.position.x; 
        }
        
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
           else
            {
                this.setaCaminhoLivre(linha, objeto[linha].GetComponent<DadosDoObjeto>().getPontoDeOrigem());
            }
            
        }
              
        string[,] melhorCaminhoMatriz = this.encontrarMelhorCaminho(matrizDeObjetos, 0, 7);

        this.imprimirMatriz(melhorCaminhoMatriz);
        //Debug.Log("%%%%%%%%%%   QUANTIDADE DE TESTES: " + (matrizDeObjetos.GetLength(0) - 1) * matrizDeObjetos.GetLength(1));

        /*
        string saida = "";
        for (int i = 0; i < melhorCaminhoMatriz.GetLength(0); i++)
        {
            for (int j = 0; j < melhorCaminhoMatriz.GetLength(1); j++)
            {
                saida += melhorCaminhoMatriz[i, j] + " | ";
                //Debug.Log();
            }
            saida += "\n";
        }

        Debug.Log(saida);
        */
        //this.analisandoDados = true;
        //this.analisaMelhorCaminhoNaMatriz(this.matrizDeObjetos);
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
                    this.matrizDeObjetos[indiceLinha + 1, comeca] = "X";
                    pontosUsados++;
                }
                else if(pontosUsados == 2)
                {
                    pontosUsados++;
                }
                else if(pontosUsados == 3)
                {
                    this.matrizDeObjetos[indiceLinha + 1, comeca] = "X";
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
                    this.matrizDeObjetos[indiceLinha + 1, comeca] = "X";
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
        this.matrizDeObjetos[indiceLinha + 1, colunaInicial] = "$";
    }

    private void setaVidaNaMatriz(int indiceLinha, int colunaInicial)
    {
        this.matrizDeObjetos[indiceLinha + 1, colunaInicial] = "+";
    }

    private void setaCaminhoLivre(int indiceLinha, int colunaInicial)
    {
        this.matrizDeObjetos[indiceLinha + 1, colunaInicial] = "*";
    }






    //
    /*
     
                FUNÇÃO PARA ESCOLHER O MELHOR CAMINHO, O QUE DER MAIS PONTOS





    ******************************** FALTA FAZER ******************
    *
    *
    *
    *fazer uma matriz recursiva para analisar todos os caminhos e escolher o melhor
     
     
    private void analisaMelhorCaminhoNaMatriz(string[,] matriz)
    {
        
    }*/


    private int caminhoMaisVantajoso(string[,] matriz, int linha, int coluna, string[,] caminhoAtual, ref string[,] melhorCaminho, ref int melhorValor)
    {
        // Caso base: se chegou na última linha da matriz
        if (linha == matriz.GetLength(0) - 1)
        {
            caminhoAtual[linha, coluna] = "!";
            int valorAtual = calcularValorCaminho(caminhoAtual);
            if (valorAtual > melhorValor)
            {
                melhorValor = valorAtual;
                melhorCaminho = (string[,])caminhoAtual.Clone(); // Copia o caminho atual para o melhor caminho
            }
            caminhoAtual[linha, coluna] = matriz[linha, coluna]; // Restaura o valor original
            return valorAtual;
        }

        // Marca a célula atual como parte do caminho
        caminhoAtual[linha, coluna] = "!";

        // Movimentos possíveis: frente, diagonal à esquerda, diagonal à direita
        int maxValor = int.MinValue;

        // Movimento para frente (mesma linha, coluna + 1)
        if (coluna + 1 < matriz.GetLength(1))
        {
            int valor = caminhoMaisVantajoso(matriz, linha + 1, coluna, caminhoAtual, ref melhorCaminho, ref melhorValor);
            if (valor > maxValor)
                maxValor = valor;
        }

        // Movimento diagonal à esquerda (linha - 1, coluna + 1)
        if (coluna > 0 && linha + 1 < matriz.GetLength(0))
        {
            int valor = caminhoMaisVantajoso(matriz, linha + 1, coluna - 1, caminhoAtual, ref melhorCaminho, ref melhorValor);
            if (valor > maxValor)
                maxValor = valor;
        }

        // Movimento diagonal à direita (linha + 1, coluna + 1)
        if (coluna < matriz.GetLength(1) - 1 && linha + 1 < matriz.GetLength(0))
        {
            int valor = caminhoMaisVantajoso(matriz, linha + 1, coluna + 1, caminhoAtual, ref melhorCaminho, ref melhorValor);
            if (valor > maxValor)
                maxValor = valor;
        }

        // Desmarca a célula atual do caminho
        caminhoAtual[linha, coluna] = matriz[linha, coluna];

        return this.valorCelula(matriz[linha, coluna]) + maxValor;
    }

    private int calcularValorCaminho(string[,] caminho)
    {
        int valorTotal = 0;
        for (int i = 0; i < caminho.GetLength(0); i++)
        {
            for (int j = 0; j < caminho.GetLength(1); j++)
            {
                valorTotal += this.valorCelula(caminho[i, j]);
            }
        }
        return valorTotal;
    }

    private string[,] encontrarMelhorCaminho(string[,] matriz, int linhaJogador, int colunaJogador)
    {
        string[,] caminhoAtual = new string[matriz.GetLength(0), matriz.GetLength(1)];
        string[,] melhorCaminho = new string[matriz.GetLength(0), matriz.GetLength(1)];
        int melhorValor = int.MinValue;

        for ()
        {

            /*
             
                        VER UMA FORMA DE REALIZAR UMA RECURSÃO ATÉ CHEGAR NA ULTIMA LINHA, TODOS OS CAMINHOS POSSIVEIS E PEGAR O QUE MAIS TEM PONTOS
                    

                        A CLASSE QUE SALVA ESSES DADOS JÁ FOI CRIADA, CHAMA-SE MatrizesEValores
             
             
             
             */

        }

        // Inicializa o caminhoAtual com o conteúdo da matriz
        for (int i = 1; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                caminhoAtual[i, j] = matriz[i, j];
            }
        }

        // Inicia a busca pelo melhor caminho
        this.caminhoMaisVantajoso(matriz, linhaJogador, colunaJogador, caminhoAtual, ref melhorCaminho, ref melhorValor);

        return melhorCaminho;
    }

    private void imprimirMatriz(string[,] matriz)
    {
        string saida = "\n************************************* Melhor Caminho Encontrado *************************************\n";

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

    private int caminhoMaisVantajoso(string[,] matriz, int linha, int coluna, string[,] caminhoAtual, ref string[,] melhorCaminho, ref int melhorValor, int somaAtual)
    {
        // Adiciona o valor da célula atual à soma acumulada
        somaAtual += this.valorCelula(matriz[linha, coluna]);

        // Marca a célula atual como parte do caminho
        caminhoAtual[linha, coluna] = "!";

        // Caso base: se chegou na última coluna, verifica se a soma atual é a melhor
        if (coluna == matriz.GetLength(1) - 1)
        {
            if (somaAtual > melhorValor)
            {
                melhorValor = somaAtual;
                melhorCaminho = (string[,])caminhoAtual.Clone(); // Copia o caminho atual para o melhor caminho
            }
            // Restaura o valor original para permitir outras explorações
            caminhoAtual[linha, coluna] = matriz[linha, coluna];
            return somaAtual;
        }

        // Movimentos possíveis: frente, diagonal à esquerda, diagonal à direita
        int maxValor = int.MinValue;

        // Movimento para frente (mesma linha, coluna + 1)
        if (coluna + 1 < matriz.GetLength(1))
        {
            int valor = caminhoMaisVantajoso(matriz, linha, coluna + 1, caminhoAtual, ref melhorCaminho, ref melhorValor, somaAtual);
            if (valor > maxValor)
                maxValor = valor;
        }

        // Movimento diagonal à esquerda (linha - 1, coluna + 1)
        if (linha > 0 && coluna + 1 < matriz.GetLength(1))
        {
            int valor = caminhoMaisVantajoso(matriz, linha - 1, coluna + 1, caminhoAtual, ref melhorCaminho, ref melhorValor, somaAtual);
            if (valor > maxValor)
                maxValor = valor;
        }

        // Movimento diagonal à direita (linha + 1, coluna + 1)
        if (linha < matriz.GetLength(0) - 1 && coluna + 1 < matriz.GetLength(1))
        {
            int valor = caminhoMaisVantajoso(matriz, linha + 1, coluna + 1, caminhoAtual, ref melhorCaminho, ref melhorValor, somaAtual);
            if (valor > maxValor)
                maxValor = valor;
        }

        // Desmarca a célula atual do caminho
        caminhoAtual[linha, coluna] = matriz[linha, coluna];

        return maxValor;
    }

    private int valorCelula(string celula)
    {
        if (celula == "*")
            return 10;  // Valor da moeda
        else if (celula == "+")
            return 30;   // Valor da vida
        else if (celula == "X")
            return -50;  // Penalidade para barreiras
        else
            return 1;   // Células vazias ou sem valor específico
    }

  






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


