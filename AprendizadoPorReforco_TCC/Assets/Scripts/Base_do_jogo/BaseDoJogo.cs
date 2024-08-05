using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BaseDoJogo : MonoBehaviour {

    //Atributos privados
    private Controle_AnimacaoPlayer testesttststs;

    // Atributos que controlam o tempo do jogo
    private float tempoTotalParaMudancaDeNivel = 40.0f; // Tempo total em segundos
    private float tempoParaIniciarOutroNivel = 10.0f; // Tempo de espera para iniciar novamente instancias de objetos
    private float tempoAtualDoNivel; // Tempo atual restante
    private float tempoParaInstanciarObstaculos = 2.3f;
    private float tempoParaInstanciarVida = 7.0f;
    private float tempoParaInstamciarMoeda = 1.5f;

    private int nivelDoJogo = 0; //Nivel do jogo, conforme aumenta, a velocidade dos objetos aumentam tbm
    private List<GameObject> objetosInstanciados = new List<GameObject>(); // Lista de objetos instanciados
    private Vector3 posicaoReferencia; // para pegar a referencia da posição do ponto selecionado
    private float velocidadeDeAcordoComONivel; // variavel para controlar a velocidade dos objetos instanciados
    private GameObject objetoInstanciado;

    //Atributos publicos
    public TMP_Text NivelDoJogo; // Referência ao texto onde nível do jogo será exibido
    public TMP_Text textoDoCronometro; // Referência ao texto onde o cronômetro será exibido
    public TMP_Text moedasDoJogador; // Referencia ao texto de quantidade de moedas do jogador
    public TMP_Text vidasDoJogador; // Quantidade de vida do jogador
    public GameObject[] prefabs; // Referência aos Prefabs existentes
    public GameObject[] pontosDeInstanciacao; // Referência aos objetos existentes na cena(Onde os prefabs serao instanciados

    //Atributos publicos e estáticos
    //public static bool IniciarJogo = false; // Inicio do jogo
    public static bool comecarOJogo;
    public static int moedasColetadasPeloJogador = 0;
    public static int vidaDoJogador = 5;


    //Função padrão dos scripts que herdam da classe MonoBehaviour
    void Start()
    {
        //this.jogadorP = new Jogador();
        comecarOJogo = false;
        this.tempoAtualDoNivel = this.tempoTotalParaMudancaDeNivel; // Iniciando com o tempo definido padrão
        this.velocidadeDeAcordoComONivel = 15f; // setando a velocidade inicial dos objetos do jogo
        this.AtualizarTextoCronometro(1); // Atualiza o texto do cronometro inicialmente
        this.atualizaQuantidadesDeMoedasDoJogador();
        this.atualizaQuantidadeDeVidasDoJogador();
    }

    //Função padrão dos scripts que herdam da classe MonoBehaviour
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return)) // O jogo só começará quando o enter for pressionado
        {
            //IniciarJogo = true; // Jogo iniciado
            comecarOJogo = true;
            //this.jogadorP.setInciarAcoes(this.comecarOJogo);
        }

        if (comecarOJogo) {
            
            this.tempoAtualDoNivel -= Time.deltaTime; // Inicio do cronometro do jogo

            if (this.tempoAtualDoNivel <= 0.00000f) // Se o tempo acabar
            {
                this.tempoParaIniciarOutroNivel -= Time.deltaTime;

                this.AtualizarTextoCronometro(2);

                if (this.tempoParaIniciarOutroNivel <= 0.0f)
                {
                    this.nivelDoJogo++; // Aumentando o nivel do jogo
                    this.NivelDoJogo.text = ": " + this.nivelDoJogo; // mudando o texto do nivel do jogo
                    this.aumentaVelocidadeDosObjetos(); // aumentando a velocidade da movimentação do objetos instanciados
                    this.tempoAtualDoNivel = this.tempoTotalParaMudancaDeNivel; // Seta o padrão de tempo do jogo
                    this.atualizaTempoDeInstanciacaoDeObjetos();
                    this.tempoParaIniciarOutroNivel = 15.0f;
                }                
            }
            else
            {

                this.tempoParaInstanciarObstaculos -= Time.deltaTime;
                this.tempoParaInstanciarVida -= Time.deltaTime;
                this.tempoParaInstamciarMoeda -= Time.deltaTime;
                
                if (this.tempoParaInstanciarObstaculos <= 0.0f)
                {
                    this.instanciarObstaculosNaPosicaoDoPonto();
                    this.tempoParaInstanciarObstaculos = 7.7f;
                }
                
                if (this.tempoParaInstanciarVida <= 0)
                {
                    this.instanciarVidaNaPosicaoDoPonto();
                    this.tempoParaInstanciarVida = 11.5f;
                }            

                if (this.tempoParaInstamciarMoeda <= 0)
                {
                    this.instanciarMoedaNaPosicaoDoPonto();                    
                    this.tempoParaInstamciarMoeda = 1.3f;
                }
                this.AtualizarTextoCronometro(1);                
            }
            this.DestruirObjetosNaPosicaoZ();
            this.atualizaQuantidadesDeMoedasDoJogador();
            this.atualizaQuantidadeDeVidasDoJogador();
        }
    }

    private void AtualizarTextoCronometro(int info)
    {
        int minutos;
        int segundos;
        string textoTempo;

        if (info == 1)
        {
            //Dividindo o tempo em minutos e segundos
            minutos = Mathf.FloorToInt(this.tempoAtualDoNivel / 60f);
            segundos = Mathf.FloorToInt(this.tempoAtualDoNivel % 60f);
            textoTempo = string.Format("{0:00}:{1:00}", minutos, segundos);

            //Atualizando as informações de tempo no jogo
            if (textoDoCronometro != null)
            {
                textoDoCronometro.text = ": " + textoTempo;
            }
        }
        else
        {
            minutos = Mathf.FloorToInt(this.tempoParaIniciarOutroNivel / 60f);
            segundos = Mathf.FloorToInt(this.tempoParaIniciarOutroNivel % 60f);
            textoTempo = string.Format("{0:00}:{1:00}", minutos, segundos);

            if (textoDoCronometro != null)
            {
                textoDoCronometro.text = ": " + textoTempo;
            }
        }

    }

    private void atualizaQuantidadesDeMoedasDoJogador()
    {
        if (moedasDoJogador != null)
        {
            moedasDoJogador.text = ": " + moedasColetadasPeloJogador;
        }
    }

    private void atualizaQuantidadeDeVidasDoJogador()
    {
        if (vidasDoJogador != null)
        {
            vidasDoJogador.text = ": " + vidaDoJogador;
        }
    }

    //Função para aumentar a velocidade de movimentação dos objetos instanciados sempre em 20%
    private void aumentaVelocidadeDosObjetos()
    {
        this.velocidadeDeAcordoComONivel += this.velocidadeDeAcordoComONivel * 0.2f;
    }

    /**
     * Funções de instanciações de objetos em cena
     */
    private void instanciarObstaculosNaPosicaoDoPonto()
    {
        if (this.prefabs != null && this.pontosDeInstanciacao != null)
        {
            // Obtendo a posição do objeto de referência
            this.posicaoReferencia = this.EmQualPontoDeveSerInstanciado().transform.position;

            // Instanciando o Obstaculo na posição do objeto de referência e salvando uma referência para o objeto instanciado
            this.objetoInstanciado = Instantiate(this.qualObstaculoInstanciar(), this.posicaoReferencia, Quaternion.identity); //Instanciando objeto
            this.objetoInstanciado.AddComponent<MovimentacaoObjeto>(); // Adicionando o script ao objeto instanciado
            this.objetoInstanciado.GetComponent<MovimentacaoObjeto>().setaVelocidadeDosObjetos(this.velocidadeDeAcordoComONivel);
            this.objetosInstanciados.Add(this.objetoInstanciado); // Adiciona o objeto à lista de objetos instanciados
        }
    }

    private void instanciarMoedaNaPosicaoDoPonto()
    {
        if (this.prefabs != null && this.pontosDeInstanciacao != null)
        {
            // Obtendo a posição do objeto de referência
            this.posicaoReferencia = this.EmQualPontoDeveSerInstanciado().transform.position;

            // Instanciando o Obstaculo na posição do objeto de referência e salvando uma referência para o objeto instanciado
            this.objetoInstanciado = Instantiate(this.prefabs[13], this.posicaoReferencia, Quaternion.identity);
            this.objetoInstanciado.AddComponent<MovimentacaoObjeto>(); // Adicionando o script ao objeto instanciado
            this.objetoInstanciado.GetComponent<MovimentacaoObjeto>().setaVelocidadeDosObjetos(this.velocidadeDeAcordoComONivel);
            this.objetosInstanciados.Add(this.objetoInstanciado); // Adiciona o objeto à lista de objetos instanciados
        }
    }

    private void instanciarVidaNaPosicaoDoPonto()
    {
        if (this.prefabs != null && this.pontosDeInstanciacao != null)
        {
            // Obtendo a posição do objeto de referência
            this.posicaoReferencia = this.EmQualPontoDeveSerInstanciado().transform.position;

            // Instanciando o Obstaculo na posição do objeto de referência e salvando uma referência para o objeto instanciado
            this.objetoInstanciado = Instantiate(this.prefabs[12], this.posicaoReferencia, Quaternion.identity);
            this.objetoInstanciado.AddComponent<MovimentacaoObjeto>(); // Adicionando o script ao objeto instanciado
            this.objetoInstanciado.GetComponent<MovimentacaoObjeto>().setaVelocidadeDosObjetos(this.velocidadeDeAcordoComONivel);
            this.objetosInstanciados.Add(this.objetoInstanciado); // Adiciona o objeto à lista de objetos instanciados
        }
    }

    private void DestruirObjetosNaPosicaoZ()
    {
        for (int i = 0; i < objetosInstanciados.Count; i++)
        {
            GameObject objetoAtual = objetosInstanciados[i];
            // Verifica se o objeto instanciado existe e se sua posição Z é menor que -12
            if (objetoAtual != null && objetoAtual.transform.position.z < -11f)
            {
                Destroy(objetoAtual); // Destroi o objeto instanciado
                objetosInstanciados.RemoveAt(i); // Remove o objeto da lista de objetos instanciados
                i--; // Decrementa o contador para compensar a remoção do objeto da lista
            }
        }
    }

    //Função para retornar um objetos aleatorio
    private GameObject qualObstaculoInstanciar()
    {
        int objeto = UnityEngine.Random.Range(0, 12);
        return prefabs[objeto];
    }

    //Função para retornar um ponto de instancia aleatorio
    private GameObject EmQualPontoDeveSerInstanciado()
    {
        int ponto = UnityEngine.Random.Range(0, this.pontosDeInstanciacao.Length);
        return pontosDeInstanciacao[ponto];
    }

    private void atualizaTempoDeInstanciacaoDeObjetos()
    {
        float porcentagemRegressiva = 0.1f;

        if (this.tempoParaInstanciarObstaculos >= 0.03f)
        {
            this.tempoParaInstanciarObstaculos -= this.tempoParaInstanciarObstaculos * porcentagemRegressiva;
        }

        if (this.tempoParaInstanciarVida >= 0.07f)
        {
            this.tempoParaInstanciarVida -= this.tempoParaInstanciarVida * porcentagemRegressiva;
        }

        if (this.tempoParaInstamciarMoeda >= 0.025)
        {
            this.tempoParaInstamciarMoeda -= this.tempoParaInstamciarMoeda * porcentagemRegressiva;
        }
    }
}