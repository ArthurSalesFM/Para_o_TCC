using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseDoJogo : MonoBehaviour {

    //Atributos privados
    //private Controle_AnimacaoPlayer testesttststs;

    // Atributos que controlam o tempo do jogo
    private float tempoTotalParaMudancaDeNivel = 40.0f; // Tempo total em segundos
    private float tempoParaIniciarOutroNivel = 10.0f; // Tempo de espera para iniciar novamente instancias de objetos
    //private float tempoAtualDoNivel; // Tempo atual restante
    private float tempoParaInstanciarObjetos = 1.0f;

    private int pontoJogador;
    private int pontoIA;

    private int nivelDoJogo = 0; //Nivel do jogo, conforme aumenta, a velocidade dos objetos aumentam tbm
    private List<GameObject> objetosInstanciadosJogador = new List<GameObject>(); // Lista de objetos instanciados
    private List<GameObject> objetosInstanciadosIA = new List<GameObject>(); // Lista de objetos instanciados
    private Vector3 posicaoReferenciaJogador; // para pegar a referencia da posição do ponto selecionado
    private Vector3 posicaoReferenciaIA;
    private float velocidadeDeAcordoComONivel; // variavel para controlar a velocidade dos objetos instanciados
    private GameObject objetoInstanciadoJogador;
    private GameObject objetoInstanciadoIA;

    //Atributos publicos
    public TMP_Text NivelDoJogo; // Referência ao texto onde nível do jogo será exibido
    public TMP_Text textoDoCronometro; // Referência ao texto onde o cronômetro será exibido
    public TMP_Text moedasDoJogador; // Referencia ao texto de quantidade de moedas do jogador
    public TMP_Text vidasDoJogador; // Quantidade de vida do jogador
    public TMP_Text moedasDaIA; // Referencia ao texto de quantidade de moedas do jogador
    public TMP_Text vidasDaIA; // Quantidade de vida do jogador

    public GameObject[] prefabs; // Referência aos Prefabs existentes
    public GameObject[] pontosDeInstanciacaoJogador; // Referência aos objetos existentes na cena(Onde os prefabs serao instanciados
    public GameObject[] pontosDeInstanciacaoIA;

    //Atributos publicos e estáticos
    //public static bool IniciarJogo = false; // Inicio do jogo
    public static bool comecarOJogo;
    public static int moedasColetadasPeloJogador = 0;
    public static int vidaDoJogador = 5;
    public static int moedasColetadasPelaIA = 0;
    public static int vidaDaIA = 5;


    //Função padrão dos scripts que herdam da classe MonoBehaviour
    void Start()
    {
        //this.jogadorP = new Jogador();
        comecarOJogo = false;
        //this.tempoAtualDoNivel = this.tempoTotalParaMudancaDeNivel; // Iniciando com o tempo definido padrão
        this.velocidadeDeAcordoComONivel = 15f; // setando a velocidade inicial dos objetos do jogo
        this.AtualizarTextoCronometro(1 , 0.0f); // Atualiza o texto do cronometro inicialmente
        this.atualizaQuantidadesDeMoedas();
        this.atualizaQuantidadesDeVidas();
    }

    //Função padrão dos scripts que herdam da classe MonoBehaviour
    /*
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return)) // O jogo só começará quando o enter for pressionado
        {
            //IniciarJogo = true; // Jogo iniciado
            comecarOJogo = true;
        }

        if (comecarOJogo) {
            
            this.tempoTotalParaMudancaDeNivel -= Time.deltaTime; // Inicio do cronometro do jogo
            this.AtualizarTextoCronometro(1, this.tempoTotalParaMudancaDeNivel);


            if (this.tempoTotalParaMudancaDeNivel > 0.00000f)
            {
                this.tempoParaInstanciarObjetos -= Time.deltaTime;
                //this.AtualizarTextoCronometro(2);

                if (this.tempoParaInstanciarObjetos <= 0.0f)
                {

                    int objeto = UnityEngine.Random.Range(0, 14);
                    int posicao = UnityEngine.Random.Range(0, 8);

                    if (vidaDoJogador > 0 && vidaDaIA > 0)
                    {
                        // ESCOLHER O QUE DEVE FAZER/MOSTRAR 
                        // SE AMBOS ESTIVEREM VIVOS
                        if (objeto == 12)
                        {
                            this.instanciarVidaNaPosicaoDoPontoJogador(objeto, posicao);
                            this.instanciarVidaNaPosicaoDoPontoIA(objeto, posicao);
                        }
                        else if (objeto == 13)
                        {
                            this.instanciarMoedaNaPosicaoDoPontoJogador(objeto, posicao);
                            this.instanciarMoedaNaPosicaoDoPontoIA(objeto, posicao);
                        }
                        else
                        {
                            this.instanciarObstaculosNaPosicaoDoPontoJogador(objeto, posicao);
                            this.instanciarObstaculosNaPosicaoDoPontoIA(objeto, posicao);
                        }
                    }
                    else if (vidaDoJogador > 0 && vidaDaIA <= 0)
                    {
                        if (this.objetosInstanciadosIA != null)
                        {
                            for (int i = 0; i < this.objetosInstanciadosIA.Count; i++)
                            {
                                GameObject objetoAtualIA = this.objetosInstanciadosIA[i];
                                Destroy(objetoAtualIA); // Destroi o objeto instanciado
                                this.objetosInstanciadosIA.RemoveAt(i);
                            }
                        }
                        // ESCOLHER O QUE DEVE FAZER/MOSTRAR 
                        // SE APENAS O JOGADOR ESTIVER VIVO
                        if (moedasColetadasPeloJogador < moedasColetadasPelaIA)
                        {
                            if (objeto == 12)
                            {
                                this.instanciarVidaNaPosicaoDoPontoJogador(objeto, posicao);
                            }
                            else if (objeto == 13)
                            {
                                this.instanciarMoedaNaPosicaoDoPontoJogador(objeto, posicao);
                            }
                            else
                            {
                                this.instanciarObstaculosNaPosicaoDoPontoJogador(objeto, posicao);
                            }
                        }
                        else
                        {
                            //Mostrar resultado em uma tela e resetar os dados iniciais
                        }
                    }
                    else if (vidaDoJogador <= 0 && vidaDaIA > 0)
                    {
                        // ESCOLHER O QUE DEVE FAZER/MOSTRAR 
                        // SE APENAS A IA ESTIVER VIVA
                        if (this.objetosInstanciadosJogador != null)
                        {
                            for (int i = 0; i < this.objetosInstanciadosJogador.Count; i++)
                            {
                                GameObject objetoAtualJogador = this.objetosInstanciadosJogador[i];
                                Destroy(objetoAtualJogador); // Destroi o objeto instanciado
                                this.objetosInstanciadosJogador.RemoveAt(i);
                            }
                        }


                        if (moedasColetadasPelaIA < moedasColetadasPeloJogador)
                        {
                            if (objeto == 12)
                            {
                                this.instanciarVidaNaPosicaoDoPontoIA(objeto, posicao);
                            }
                            else if (objeto == 13)
                            {
                                this.instanciarMoedaNaPosicaoDoPontoIA(objeto, posicao);
                            }
                            else
                            {
                                this.instanciarObstaculosNaPosicaoDoPontoIA(objeto, posicao);
                            }
                        }
                        else
                        {
                            //Mostrar resultado em uma tela e resetar os dados iniciais
                        }
                    }
                    else
                    {

                        if (this.objetosInstanciadosJogador != null)
                        {
                            for (int i = 0; i < this.objetosInstanciadosJogador.Count; i++)
                            {
                                GameObject objetoAtualJogador = this.objetosInstanciadosJogador[i];
                                Destroy(objetoAtualJogador); // Destroi o objeto instanciado
                                this.objetosInstanciadosJogador.RemoveAt(i);
                            }
                        }

                        if (this.objetosInstanciadosIA != null)
                        {
                            for (int i = 0; i < this.objetosInstanciadosIA.Count; i++)
                            {
                                GameObject objetoAtualIA = this.objetosInstanciadosIA[i];
                                Destroy(objetoAtualIA); // Destroi o objeto instanciado
                                this.objetosInstanciadosIA.RemoveAt(i);
                            }
                        }
                        // ESCOLHER O QUE DEVE FAZER/MOSTRAR 
                        // SE AMBOS ESTIVEREM MORTOS
                    }
                    this.tempoParaInstanciarObjetos = 3.0f;
                }               
            }
            else
            {
                this.tempoParaIniciarOutroNivel -= Time.deltaTime;

                if (this.tempoParaIniciarOutroNivel < 0.0f)
                {
                    this.nivelDoJogo++; // Aumentando o nivel do jogo
                    this.NivelDoJogo.text = "- : " + this.nivelDoJogo; // mudando o texto do nivel do jogo
                    this.tempoTotalParaMudancaDeNivel = 40.0f;                    
                    this.tempoParaInstanciarObjetos = 3.0f;
                    this.tempoParaIniciarOutroNivel = 10.0f;
                }
                 
            }
            
            //this.AtualizarTextoCronometro(1);
            this.DestruirObjetosNaPosicaoZ();
            this.atualizaQuantidadesDeMoedas();
            this.atualizaQuantidadesDeVidas();
        }
    }
    */

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // O jogo só começará quando o Enter for pressionado
        {
            comecarOJogo = true;
        }

        if (comecarOJogo)
        {

            Debug.Log("1 = " + this.tempoTotalParaMudancaDeNivel);
            if (this.tempoTotalParaMudancaDeNivel >= 0.0f)
            {
                this.tempoTotalParaMudancaDeNivel -= Time.deltaTime; // Atualiza o cronômetro do jogo
                this.AtualizarTextoCronometro(1, this.tempoTotalParaMudancaDeNivel);

                this.tempoParaInstanciarObjetos -= Time.deltaTime;
                Debug.Log("2 = " + this.tempoParaInstanciarObjetos);
                if (this.tempoParaInstanciarObjetos <= 0.0f)
                {
                    InstanciarObjetos();
                    this.tempoParaInstanciarObjetos = 3.0f; // Reinicia o tempo para a próxima instância                    
                }
            }
            else
            {
                this.tempoParaIniciarOutroNivel -= Time.deltaTime;

                Debug.Log("3 = " + this.tempoParaIniciarOutroNivel);
                if (this.tempoParaIniciarOutroNivel <= 0.0f)
                {
                    // Reinicia o nível
                    this.nivelDoJogo++;
                    this.NivelDoJogo.text = "- : " + this.nivelDoJogo;
                    this.tempoTotalParaMudancaDeNivel = 40.0f; // Reinicia o tempo total do nível
                    this.tempoParaIniciarOutroNivel = 10.0f; // Reinicia o tempo de espera para o próximo nível
                    this.tempoParaInstanciarObjetos = 0.0f; // Garante a imediata instância dos objetos

                    Debug.Log("\n\n\n1 :  " + this.tempoTotalParaMudancaDeNivel);
                    Debug.Log("\n\n\n2 :  " + this.tempoParaInstanciarObjetos);
                    Debug.Log("\n\n\n3 :  " + this.tempoParaIniciarOutroNivel);
                }
            }

            // Outras operações
            this.DestruirObjetosNaPosicaoZ();
            this.atualizaQuantidadesDeMoedas();
            this.atualizaQuantidadesDeVidas();
        }
    }

    void InstanciarObjetos()
    {
        int objeto = UnityEngine.Random.Range(0, 14);
        int posicao = UnityEngine.Random.Range(0, 8);

        if (vidaDoJogador > 0 && vidaDaIA > 0)
        {
            if (objeto == 12)
            {
                this.instanciarVidaNaPosicaoDoPontoJogador(objeto, posicao);
                this.instanciarVidaNaPosicaoDoPontoIA(objeto, posicao);
            }
            else if (objeto == 13)
            {
                this.instanciarMoedaNaPosicaoDoPontoJogador(objeto, posicao);
                this.instanciarMoedaNaPosicaoDoPontoIA(objeto, posicao);
            }
            else
            {
                this.instanciarObstaculosNaPosicaoDoPontoJogador(objeto, posicao);
                this.instanciarObstaculosNaPosicaoDoPontoIA(objeto, posicao);
            }
        }
        
        else if (vidaDoJogador > 0 && vidaDaIA <= 0)
        {
            LimparObjetosIA();

            if (moedasColetadasPeloJogador <= moedasColetadasPelaIA)
            {
                if (objeto == 12)
                {
                    this.instanciarVidaNaPosicaoDoPontoJogador(objeto, posicao);
                }
                else if (objeto == 13)
                {
                    this.instanciarMoedaNaPosicaoDoPontoJogador(objeto, posicao);
                }
                else
                {
                    this.instanciarObstaculosNaPosicaoDoPontoJogador(objeto, posicao);
                }
            }
            else
            {
                LimparObjetosJogador();
                // Mostrar resultado em uma tela e resetar os dados iniciais
            }
        }

        else if (vidaDoJogador <= 0 && vidaDaIA > 0)
        {
            LimparObjetosJogador();

            if (moedasColetadasPelaIA <= moedasColetadasPeloJogador)
            {
                if (objeto == 12)
                {
                    this.instanciarVidaNaPosicaoDoPontoIA(objeto, posicao);
                }
                else if (objeto == 13)
                {
                    this.instanciarMoedaNaPosicaoDoPontoIA(objeto, posicao);
                }
                else
                {
                    this.instanciarObstaculosNaPosicaoDoPontoIA(objeto, posicao);
                }
            }
            else
            {
                LimparObjetosIA();
                // Mostrar resultado em uma tela e resetar os dados iniciais
            }
        }
        
        else
        {
            LimparObjetosJogador();
            LimparObjetosIA();
            // Mostrar resultado em uma tela e resetar os dados iniciais
        }
    }

    void LimparObjetosJogador()
    {
        if (this.objetosInstanciadosJogador != null)
        {
            for (int i = 0; i < this.objetosInstanciadosJogador.Count; i++)
            {
                Destroy(this.objetosInstanciadosJogador[i]);
            }
            this.objetosInstanciadosJogador.Clear();
        }
    }

    void LimparObjetosIA()
    {
        if (this.objetosInstanciadosIA != null)
        {
            for (int i = 0; i < this.objetosInstanciadosIA.Count; i++)
            {
                Destroy(this.objetosInstanciadosIA[i]);
            }
            this.objetosInstanciadosIA.Clear();
        }
    }

    private void AtualizarTextoCronometro(int info, float tempo)
    {
        int minutos;
        int segundos;
        string textoTempo;

        if (info == 1)
        {
            //Dividindo o tempo em minutos e segundos
            minutos = Mathf.FloorToInt(tempo / 60f);
            segundos = Mathf.FloorToInt(tempo % 60f);
            textoTempo = string.Format("{0:00}:{1:00}", minutos, segundos);

            //Atualizando as informações de tempo no jogo
            if (textoDoCronometro != null)
            {
                textoDoCronometro.text = ": " + textoTempo;
            }
        }
        else
        {
            minutos = Mathf.FloorToInt(tempo / 60f);
            segundos = Mathf.FloorToInt(tempo % 60f);
            textoTempo = string.Format("{0:00}:{1:00}", minutos, segundos);

            if (textoDoCronometro != null)
            {
                textoDoCronometro.text = ": " + textoTempo;
            }
        }

    }

    private void atualizaQuantidadesDeMoedas()
    {
        if (moedasDoJogador != null)
        {
            moedasDoJogador.text = ": " + moedasColetadasPeloJogador;
        }
        if (moedasDaIA != null)
        {
            moedasDaIA.text = ": " + moedasColetadasPelaIA;
        }
    }

    private void atualizaQuantidadesDeVidas()
    {
        if (vidaDoJogador > 0)
        {
            vidasDoJogador.text = ": " + vidaDoJogador;
        }

        if (vidaDaIA > 0)
        {
            vidasDaIA.text = ": " + vidaDaIA;
        }
    }

    /**
     * Funções de instanciações de objetos em cena
     */
    private void instanciarObstaculosNaPosicaoDoPontoJogador(int objeto, int posicao)
    {
        if (this.prefabs != null && this.pontosDeInstanciacaoJogador != null)
        {
            this.posicaoReferenciaJogador = this.pontosDeInstanciacaoJogador[posicao].transform.position;
            // Instanciando o Obstaculo na posição do objeto de referência e salvando uma referência para o objeto instanciado
            this.objetoInstanciadoJogador = Instantiate(this.qualObjetoInstanciar(objeto), this.posicaoReferenciaJogador, Quaternion.identity); //Instanciando objeto
            this.objetoInstanciadoJogador.AddComponent<DadosDoObjeto>().instaciarObjetos(this.velocidadeDeAcordoComONivel, this.nivelDoJogo, this.pontoJogador, objeto);
            this.objetosInstanciadosJogador.Add(this.objetoInstanciadoJogador); // Adiciona o objeto à lista de objetos instanciados
        }
    }

    private void instanciarObstaculosNaPosicaoDoPontoIA(int objeto, int posicao)
    {
        if (this.prefabs != null && this.pontosDeInstanciacaoIA != null)
        {
            this.posicaoReferenciaIA = this.pontosDeInstanciacaoIA[posicao].transform.position;
            this.objetoInstanciadoIA = Instantiate(this.qualObjetoInstanciar(objeto), this.posicaoReferenciaIA, Quaternion.identity);
            this.objetoInstanciadoIA.AddComponent<DadosDoObjeto>().instaciarObjetos(this.velocidadeDeAcordoComONivel, this.nivelDoJogo, this.pontoIA, objeto);
            this.objetosInstanciadosIA.Add(objetoInstanciadoIA);
        }            
    }

    private void instanciarMoedaNaPosicaoDoPontoJogador(int objeto, int posicao)
    {
        if (this.prefabs != null && this.pontosDeInstanciacaoJogador != null)
        {
            this.posicaoReferenciaJogador = this.pontosDeInstanciacaoJogador[posicao].transform.position;
            this.objetoInstanciadoJogador = Instantiate(this.qualObjetoInstanciar(objeto), this.posicaoReferenciaJogador, Quaternion.identity);
            this.objetoInstanciadoJogador.AddComponent<DadosDoObjeto>().instaciarObjetos(this.velocidadeDeAcordoComONivel, this.nivelDoJogo, this.pontoJogador, objeto);
            this.objetosInstanciadosJogador.Add(this.objetoInstanciadoJogador); // Adiciona o objeto à lista de objetos instanciados
        }
    }

    private void instanciarMoedaNaPosicaoDoPontoIA(int objeto, int posicao)
    {
        if(this.prefabs != null && this.pontosDeInstanciacaoIA != null)
        {
            this.posicaoReferenciaIA = this.pontosDeInstanciacaoIA[posicao].transform.position;
            this.objetoInstanciadoIA = Instantiate(this.qualObjetoInstanciar(objeto), this.posicaoReferenciaIA, Quaternion.identity);
            this.objetoInstanciadoIA.AddComponent<DadosDoObjeto>().instaciarObjetos(this.velocidadeDeAcordoComONivel, this.nivelDoJogo, this.pontoIA, objeto);
            this.objetosInstanciadosIA.Add(objetoInstanciadoIA);
        }    
    }

    private void instanciarVidaNaPosicaoDoPontoJogador(int objeto, int posicao)
    {
        if (this.prefabs != null && this.pontosDeInstanciacaoJogador != null)
        {            
            this.posicaoReferenciaJogador = this.pontosDeInstanciacaoJogador[posicao].transform.position;
            this.objetoInstanciadoJogador = Instantiate(this.qualObjetoInstanciar(objeto), this.posicaoReferenciaJogador, Quaternion.identity);
            this.objetoInstanciadoJogador.AddComponent<DadosDoObjeto>().instaciarObjetos(this.velocidadeDeAcordoComONivel, this.nivelDoJogo, this.pontoJogador, 12); // Adicionando o script ao objeto instanciado
            this.objetosInstanciadosJogador.Add(this.objetoInstanciadoJogador); // Adiciona o objeto à lista de objetos instanciados
        }
    }

    private void instanciarVidaNaPosicaoDoPontoIA(int objeto, int posicao)
    {
        if (this.prefabs != null && this.pontosDeInstanciacaoIA != null)
        {
            this.posicaoReferenciaIA = this.pontosDeInstanciacaoIA[posicao].transform.position;
            this.objetoInstanciadoIA = Instantiate(this.qualObjetoInstanciar(objeto), this.posicaoReferenciaIA, Quaternion.identity);
            this.objetoInstanciadoIA.AddComponent<DadosDoObjeto>().instaciarObjetos(this.velocidadeDeAcordoComONivel, this.nivelDoJogo, this.pontoIA, objeto);
            this.objetosInstanciadosIA.Add(objetoInstanciadoIA);
        }
    }

    private void DestruirObjetosNaPosicaoZ()
    {
        for (int i = 0; i < this.objetosInstanciadosJogador.Count; i++)
        {
            GameObject objetoAtualJogador = this.objetosInstanciadosJogador[i];
            
            // Verifica se o objeto instanciado existe e se sua posição Z é menor que -12
            if (objetoAtualJogador != null && objetoAtualJogador.transform.position.z < 20.0f)
            {
                Destroy(objetoAtualJogador); // Destroi o objeto instanciado               
                this.objetosInstanciadosJogador.RemoveAt(i); // Remove o objeto da lista de objetos instanciados                
                i--; // Decrementa o contador para compensar a remoção do objeto da lista
            }           
        }

        for(int i = 0; i < this.objetosInstanciadosIA.Count; i++)
        {
            GameObject objetoAtualIA = this.objetosInstanciadosIA[i];
            if (objetoAtualIA != null)
            {

                if (objetoAtualIA.transform.position.z < 20.0f)
                {
                    Destroy(objetoAtualIA); // Destroi o objeto instanciado
                    this.objetosInstanciadosIA.RemoveAt(i);
                    
                }

                if (objetoAtualIA.transform.position.y < 473.4)
                {
                    Destroy(objetoAtualIA); // Destroi o objeto instanciado
                    this.objetosInstanciadosIA.RemoveAt(i);
                }

                
            }           
        }
    }

    //Função para retornar um objetos aleatorio

    private void atualizaTempoDeAcordoComONivel(int nv)
    {

    }

    private GameObject qualObjetoInstanciar(int posicao)
    {
        return prefabs[posicao];
    }
   
}