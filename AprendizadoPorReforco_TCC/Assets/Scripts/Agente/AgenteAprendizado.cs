using System.Collections.Generic;
using UnityEngine;

public class AgenteAprendizado : MonoBehaviour
{
    //public LineRenderer linhaDoRaio;  // Adicione o LineRenderer via Unity Inspector
    private ControleMovimentacaoIA controleDeAnimacaoIA;
    private bool analisandoDados = false;
    private string[,] matrizDeObjetos;

    void Update()
    {
        // A cada quadro, obtemos observa��es, tomamos decis�es e executamos a��es        
        bool observacao = ObterObservacao(out _);
        TomarDecisao();
        ExecutarAcao();
    }

    public string[,] getDadosDaMatriz()
    {
        return this.matrizDeObjetos;
    }

    private void Start()
    {
       this.controleDeAnimacaoIA = new ControleMovimentacaoIA();

        // Adicionar verifica��o para getVelocidadeCorrida()
        float velocidade = controleDeAnimacaoIA.getVelocidadeCorrida();
        if (velocidade <= 0)
        {
            Debug.LogError("Velocidade de corrida � inv�lida.");
            return;
        }
        //this.tabelaVelocidadeDeMudancaDePosicao.printaTabela();
    }

    // Fun��o chamada a cada quadro
    
    public bool estaAnalisandoOsDados()
    {
        return this.analisandoDados;
    }

    public void setaMatrizParaAnalise(List<GameObject> objeto)
    {

        this.analisandoDados = true;
        this.matrizDeObjetos = new string[objeto.Count, 8];

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
        
    }    

    private void setaObstaculoNaMatriz(int indiceLinha, int colunaInicial, int quantidadeDePontosOcupados)
    {
        int pontosUsados = 0;        
        for (int comeca = colunaInicial; comeca < 8; comeca++)
        {
            if(quantidadeDePontosOcupados == 3)
            {
                if (pontosUsados < 2)
                {
                    this.matrizDeObjetos[indiceLinha, comeca] = "X";
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
                    this.matrizDeObjetos[indiceLinha, comeca] = "X";
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
        this.matrizDeObjetos[indiceLinha, colunaInicial] = "$";
    }

    private void setaVidaNaMatriz(int indiceLinha, int colunaInicial)
    {
        this.matrizDeObjetos[indiceLinha, colunaInicial] = "+";
    }

    // Fun��o para obter observa��es do ambiente
    private bool ObterObservacao(out string objetoDetectado)
    {
        // L�gica para detec��o de objetos ao redor
        // ...
        objetoDetectado = null;
        return false;
    }

    // Fun��o para executar a��es com base nas observa��es
    private void ExecutarAcao()
    {
        // L�gica para executar a��es com base nas observa��es
        string objetoDetectado;
        if (ObterObservacao(out objetoDetectado))
        {
            Debug.Log($"Objeto detectado: {objetoDetectado}. Executando a��o...");
        }
    }

    // Fun��o para tomar decis�es com base nas observa��es
    private void TomarDecisao()
    {
        // L�gica para tomar decis�es com base nas observa��es
        string objetoDetectado;
        if (ObterObservacao(out objetoDetectado))
        {
            switch (objetoDetectado)
            {
                case "moeda":
                    MoverEmDirecaoAoAlvo();  // Implemente essa fun��o
                    break;
                case "barreira":
                    EvitarObstaculo();  // Implemente essa fun��o
                    break;
                case "vida":
                    // L�gica para a��o espec�fica ao detectar um objeto "vida"
                    break;
                default:
                    // L�gica padr�o ou nenhuma a��o se nenhum objeto detectado
                    break;
            }
        }
    }

    private void MoverEmDirecaoAoAlvo()
    {
        // Exemplo: Movimenta o agente na dire��o do alvo (moeda)
        //transform.Translate(Vector3.forward * velocidadeMovimento * Time.deltaTime);
    }

    private void EvitarObstaculo()
    {
        // Exemplo: Gira o agente para evitar a barreira
        //transform.Rotate(Vector3.up * velocidadeRotacao * Time.deltaTime);
    }

}


