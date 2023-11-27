using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BaseDoJogo : MonoBehaviour
{
    //Atributos privados
    private float tempoTotalParaMudancaDeNivel = 20.0f; // Tempo total em segundos
    //private float tempoParaIniciarOutroNivel = 15.0f; // Tempo de espera para iniciar novamente instancias de objetos
    private float tempoAtualDoNivel; // Tempo atual restante
    private float tempoParaInstanciarObstaculos = 6.3f;
    private float tempoParaInstanciarVida = 12.5f;
    private float tempoParaInstamciarMoeda = 1.1f;
    private int nivelDoJogo = 0; //Nivel do jogo, conforme aumenta, a velocidade dos objetos aumentam tbm
    private List<GameObject> objetosInstanciados = new List<GameObject>(); // Lista de objetos instanciados
    private Vector3 posicaoReferencia; // para pegar a referencia da posi��o do ponto selecionado
    private float velocidadeDeAcordoComONivel; // variavel para controlar a velocidade dos objetos instanciados
    private GameObject objetoInstanciado;

    //Atributos publicos
    public TMP_Text NivelDoJogo; // Refer�ncia ao texto onde n�vel do jogo ser� exibido
    public TMP_Text textoDoCronometro; // Refer�ncia ao texto onde o cron�metro ser� exibido
    public TMP_Text moedasDoJogador; // Referencia ao texto de quantidade de moedas do jogador
    public TMP_Text vidasDoJogador;
    public GameObject[] prefabs; // Refer�ncia aos Prefabs
    public GameObject[] pontosDeInstanciacao; // Refer�ncia aos objetos existentes na cena

    //Atributos publicos e est�ticos
    public static bool IniciarJogo = false; // Inicio do jogo
    public static int moedasColetadasPeloJogador = 0;
    public static int vidaDoJogador = 5;

    //Fun��o padr�o dos scripts que herdam da classe MonoBehaviour
    void Start()
    {
        this.tempoAtualDoNivel = this.tempoTotalParaMudancaDeNivel; // Iniciando com o tempo definido padr�o
        this.velocidadeDeAcordoComONivel = 15f; // setando a velocidade inicial dos objetos do jogo
        this.AtualizarTextoCronometro(1); // Atualiza o texto do cronometro inicialmente
        this.atualizaQuantidadesDeMoedasDoJogador();
        this.atualizaQuantidadeDeVidasDoJogador();
    }

    //Fun��o padr�o dos scripts que herdam da classe MonoBehaviour
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return)) // O jogo s� come�ar� quando o enter for pressionado
        {
            IniciarJogo = true; // Jogo iniciado
        }

        if (IniciarJogo)
        {
            
            this.tempoAtualDoNivel -= Time.deltaTime; // Inicio do cronometro do jogo

            if (this.tempoAtualDoNivel <= 0f) // Se o tempo acabar
            {
                this.nivelDoJogo++; // Aumentando o nivel do jogo
                this.NivelDoJogo.text = "N�vel: " + this.nivelDoJogo; // mudando o texto do nivel do jogo
                this.aumentaVelocidadeDosObjetos(); // aumentando a velocidade da movimenta��o do objetos instanciados
                this.tempoAtualDoNivel = this.tempoTotalParaMudancaDeNivel; // Seta o padr�o de tempo do jogo                              
            }
            else
            {
                this.tempoParaInstanciarObstaculos -= Time.deltaTime;
                this.tempoParaInstanciarVida -= Time.deltaTime;
                this.tempoParaInstamciarMoeda -= Time.deltaTime;
                
                if (this.tempoParaInstanciarObstaculos <= 0)
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
            }
            this.AtualizarTextoCronometro(1);
            this.DestruirObjetosNaPosicaoZ();
            this.atualizaQuantidadesDeMoedasDoJogador();
            this.atualizaQuantidadeDeVidasDoJogador();
        }
    }

    private void AtualizarTextoCronometro(int info)
    {
        if (info == 1)
        {
            //Dividindo o tempo em minutos e segundos
            int minutos = Mathf.FloorToInt(this.tempoAtualDoNivel / 60f);
            int segundos = Mathf.FloorToInt(this.tempoAtualDoNivel % 60f);
            string textoTempo = string.Format("{0:00}:{1:00}", minutos, segundos);

            //Atualizando as informa��es de tempo no jogo
            if (textoDoCronometro != null)
            {
                textoDoCronometro.text = "Tempo: " + textoTempo;
            }
        }
        else
        {
            //informa��o do tempo de espera para come�ar outro n�vel
        }

    }

    private void atualizaQuantidadesDeMoedasDoJogador()
    {
        if (moedasDoJogador != null)
        {
            moedasDoJogador.text = "Moedas: " + moedasColetadasPeloJogador;
        }
    }

    private void atualizaQuantidadeDeVidasDoJogador()
    {
        if (vidasDoJogador != null)
        {
            vidasDoJogador.text = "Vida: " + vidaDoJogador;
        }
    }

    //Fun��o para aumentar a velocidade de movimenta��o dos objetos instanciados sempre em 20%
    private void aumentaVelocidadeDosObjetos()
    {
        this.velocidadeDeAcordoComONivel = this.velocidadeDeAcordoComONivel + (this.velocidadeDeAcordoComONivel * 0.2f);
    }

    /**
     * Fun��es de instancia��es de objetos em cena
     */
    private void instanciarObstaculosNaPosicaoDoPonto()
    {
        if (this.prefabs != null && this.pontosDeInstanciacao != null)
        {
            // Obtendo a posi��o do objeto de refer�ncia
            this.posicaoReferencia = this.EmQualPontoDeveSerInstanciado().transform.position;

            // Instanciando o Obstaculo na posi��o do objeto de refer�ncia e salvando uma refer�ncia para o objeto instanciado
            this.objetoInstanciado = Instantiate(this.qualObstaculoInstanciar(), this.posicaoReferencia, Quaternion.identity); //Instanciando objeto
            this.objetoInstanciado.AddComponent<MovimentacaoObjeto>(); // Adicionando o script ao objeto instanciado
            this.objetoInstanciado.GetComponent<MovimentacaoObjeto>().setaVelocidadeDosObjetos(this.velocidadeDeAcordoComONivel);
            this.objetosInstanciados.Add(this.objetoInstanciado); // Adiciona o objeto � lista de objetos instanciados
        }
    }

    private void instanciarMoedaNaPosicaoDoPonto()
    {
        if (this.prefabs != null && this.pontosDeInstanciacao != null)
        {
            // Obtendo a posi��o do objeto de refer�ncia
            this.posicaoReferencia = this.EmQualPontoDeveSerInstanciado().transform.position;

            // Instanciando o Obstaculo na posi��o do objeto de refer�ncia e salvando uma refer�ncia para o objeto instanciado
            this.objetoInstanciado = Instantiate(this.prefabs[13], this.posicaoReferencia, Quaternion.identity);
            this.objetoInstanciado.AddComponent<MovimentacaoObjeto>(); // Adicionando o script ao objeto instanciado
            this.objetoInstanciado.GetComponent<MovimentacaoObjeto>().setaVelocidadeDosObjetos(this.velocidadeDeAcordoComONivel);
            this.objetosInstanciados.Add(this.objetoInstanciado); // Adiciona o objeto � lista de objetos instanciados
        }
    }

    private void instanciarVidaNaPosicaoDoPonto()
    {
        if (this.prefabs != null && this.pontosDeInstanciacao != null)
        {
            // Obtendo a posi��o do objeto de refer�ncia
            this.posicaoReferencia = this.EmQualPontoDeveSerInstanciado().transform.position;

            // Instanciando o Obstaculo na posi��o do objeto de refer�ncia e salvando uma refer�ncia para o objeto instanciado
            this.objetoInstanciado = Instantiate(this.prefabs[12], this.posicaoReferencia, Quaternion.identity);
            this.objetoInstanciado.AddComponent<MovimentacaoObjeto>(); // Adicionando o script ao objeto instanciado
            this.objetoInstanciado.GetComponent<MovimentacaoObjeto>().setaVelocidadeDosObjetos(this.velocidadeDeAcordoComONivel);
            this.objetosInstanciados.Add(this.objetoInstanciado); // Adiciona o objeto � lista de objetos instanciados
        }
    }

    private void DestruirObjetosNaPosicaoZ()
    {
        for (int i = 0; i < objetosInstanciados.Count; i++)
        {
            GameObject objetoAtual = objetosInstanciados[i];
            // Verifica se o objeto instanciado existe e se sua posi��o Z � menor que -12
            if (objetoAtual != null && objetoAtual.transform.position.z < -11f)
            {
                Destroy(objetoAtual); // Destroi o objeto instanciado
                objetosInstanciados.RemoveAt(i); // Remove o objeto da lista de objetos instanciados
                i--; // Decrementa o contador para compensar a remo��o do objeto da lista
            }
        }
    }

    //Fun��o para retornar um objetos aleatorio
    private GameObject qualObstaculoInstanciar()
    {
        int objeto = UnityEngine.Random.Range(0, 12);
        return prefabs[objeto];
    }

    //Fun��o para retornar um ponto de instancia aleatorio
    private GameObject EmQualPontoDeveSerInstanciado()
    {
        int ponto = UnityEngine.Random.Range(0, this.pontosDeInstanciacao.Length);
        return pontosDeInstanciacao[ponto];
    }

}