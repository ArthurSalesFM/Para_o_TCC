using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Base_Treinamento : MonoBehaviour
{
    //Atributos privados
    private float tempoAtualDoNivel; // Tempo atual restante
    //private TabelaVelocidadeDeMudancaDePosicao tabaleDaVelocidadeDeMudancaDePosicao;

    public GameObject IA;

    //
    private TemposPadroes temposPadroes;
    private TabelaVelocidadeDeMudancaDePosicao tabelaVelocidadeDeMudancaDePosicao = new TabelaVelocidadeDeMudancaDePosicao();
    private CriaAbreArquivoAFMS criarAbrirAquivoAFMS = new CriaAbreArquivoAFMS();

    private int nivelDoJogo = 0; //Nivel do jogo, conforme aumenta, a velocidade dos objetos aumentam tbm
    public List<GameObject> objetosInstanciados = new List<GameObject>(); // Lista de objetos instanciados
    private Vector3 posicaoReferencia; // para pegar a referencia da posição do ponto selecionado
    private float velocidadeDeAcordoComONivel; // variavel para controlar a velocidade dos objetos instanciados
    private GameObject objetoInstanciado;

    //Atributos publicos
    public TMP_Text NivelDoJogo; // Referência ao texto onde nível do jogo será exibido
    public TMP_Text textoDoCronometro; // Referência ao texto onde o cronômetro será exibido
    public TMP_Text moedasDoJogador; // Referencia ao texto de quantidade de moedas do jogador
    public TMP_Text vidasDoJogador; // Quantidade de vida do jogador
    public GameObject[] prefabs; // Referência aos Prefabs existentes
    private int qualObjetoFoiInstanciado;
    public GameObject[] pontosDeInstanciacao; // Referência aos objetos existentes na cena(Onde os prefabs serao instanciados
    private int qualPontoFoiInstanciado;    

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
        //this.tabaleDaVelocidadeDeMudancaDePosicao = new TabelaVelocidadeDeMudancaDePosicao(pontosDeInstanciacao, temposPadroes.);
        this.temposPadroes = new TemposPadroes();
        this.tempoAtualDoNivel = this.temposPadroes.getTempoTotalParaMudancaDeNivel(); // Iniciando com o tempo definido padrão
        this.velocidadeDeAcordoComONivel = 15f; // setando a velocidade inicial dos objetos do jogo
        this.AtualizarTextoCronometro(1); // Atualiza o texto do cronometro inicialmente
        this.atualizaQuantidadesDeMoedasDoJogador();
        this.atualizaQuantidadeDeVidasDoJogador();
        this.tabelaVelocidadeDeMudancaDePosicao.setaValoresNaTabela(pontosDeInstanciacao, 12.5f);
        //this.tabelaVelocidadeDeMudancaDePosicao.printaTabela();
        this.criarAbrirAquivoAFMS.GravarMatriz(this.tabelaVelocidadeDeMudancaDePosicao.getTabelaDeVelocidadeDeMudancaDePosicao(), "MatrizTempoMedioDeMudancaDePonto.AFMS");
        // Ler a matriz do arquivo
        //float[,] matrizDoArquivo = criarAbrirAquivoAFMS.LerMatriz("MatrizTempoMedioDeMudancaDePonto.AFMS");
        
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

        if (comecarOJogo)
        {

            this.tempoAtualDoNivel -= Time.deltaTime; // Inicio do cronometro do jogo

            if (this.tempoAtualDoNivel <= 0.00000f) // Se o tempo acabar
            {
                this.temposPadroes.setTempoParaIniciarOutroNivel(this.temposPadroes.getTempoParaIniciarOutroNivel() - Time.deltaTime);

                this.AtualizarTextoCronometro(2);

                if (this.temposPadroes.getTempoParaIniciarOutroNivel() <= 0.0f)
                {
                    this.nivelDoJogo++; // Aumentando o nivel do jogo
                    this.NivelDoJogo.text = ": " + this.nivelDoJogo; // mudando o texto do nivel do jogo
                    this.aumentaVelocidadeDosObjetos(); // aumentando a velocidade da movimentação do objetos instanciados
                    this.tempoAtualDoNivel = this.temposPadroes.getTempoTotalParaMudancaDeNivel(); // Seta o padrão de tempo do jogo
                    this.atualizaTempoDeInstanciacaoDeObjetos();
                    this.temposPadroes.setTempoParaIniciarOutroNivel(15.0f);
                }
            }
            else
            {

                this.temposPadroes.setTempoParaInstanciarObstaculos(this.temposPadroes.getTempoParaInstanciarObstaculos() - Time.deltaTime);
                this.temposPadroes.setTempoParaInstanciarVida(this.temposPadroes.getTempoParaInstanciarVida() - Time.deltaTime);
                this.temposPadroes.setTempoParaInstamciarMoeda(this.temposPadroes.getTempoParaInstamciarMoeda() - Time.deltaTime);

                if (this.temposPadroes.getTempoParaInstanciarObstaculos() <= 0.0f)
                {
                    this.instanciarObstaculosNaPosicaoDoPonto();
                    this.temposPadroes.setTempoParaInstanciarObstaculos(7.7f);
                    //this.tempoParaInstanciarObstaculos = 7.7f;
                }

                if (this.temposPadroes.getTempoParaInstanciarVida() <= 0.0f)
                {
                    this.instanciarVidaNaPosicaoDoPonto();
                    this.temposPadroes.setTempoParaInstanciarVida(11.5f);
                }

                if (this.temposPadroes.getTempoParaInstamciarMoeda() <= 0.0f)
                {
                    this.instanciarMoedaNaPosicaoDoPonto();
                    this.temposPadroes.setTempoParaInstamciarMoeda(1.3f);
                }
                this.AtualizarTextoCronometro(1);
            }
            this.DestruirObjetosNaPosicaoZ();
            this.atualizaQuantidadesDeMoedasDoJogador();
            this.atualizaQuantidadeDeVidasDoJogador();

            if (objetosInstanciados.Count > 17)
            {
                if (!IA.GetComponent<AgenteAprendizado>().estaAnalisandoOsDados())
                {
                    IA.GetComponent<AgenteAprendizado>().setaMatrizParaAnalise(objetosInstanciados.GetRange(0, 12), pontosDeInstanciacao);
                    //this.criarAbrirAquivoAFMS.GravarMatriz(IA.GetComponent<AgenteAprendizado>().getDadosDaMatriz(), "teste.AFMS");
                }
                              
            }

            /*if(this.objetosInstanciados.Count != 0)
            {
                Debug.Log(this.objetosInstanciados[0].GetComponent<DadosDoObjeto>().getTempoRestanteParaChegar());
            }*/

            
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
            minutos = Mathf.FloorToInt(this.temposPadroes.getTempoParaIniciarOutroNivel() / 60f);
            segundos = Mathf.FloorToInt(this.temposPadroes.getTempoParaIniciarOutroNivel() % 60f);
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
        if (this.prefabs != null && pontosDeInstanciacao != null)
        {
            // Obtendo a posição do objeto de referência
            this.posicaoReferencia = this.EmQualPontoDeveSerInstanciado().transform.position;

            // Instanciando o Obstaculo na posição do objeto de referência e salvando uma referência para o objeto instanciado
            this.objetoInstanciado = Instantiate(this.qualObstaculoInstanciar(), this.posicaoReferencia, Quaternion.identity); //Instanciando objeto
            this.objetoInstanciado.AddComponent<DadosDoObjeto>(); // Adicionando o script ao objeto instanciado
            this.objetoInstanciado.GetComponent<DadosDoObjeto>().instaciarObjetos(this.velocidadeDeAcordoComONivel, this.nivelDoJogo, this.qualPontoFoiInstanciado, this.qualObjetoFoiInstanciado);
            //this.objetoInstanciado.GetComponent<DadosDoObjeto>().setNivelObjeto(this.nivelDoJogo);
            //this.objetoInstanciado.GetComponent<DadosDoObjeto>().setPontoDeOrigem(this.qualPontoFoiInstanciado);
            this.objetosInstanciados.Add(this.objetoInstanciado); // Adiciona o objeto à lista de objetos instanciados
        }
    }

    private void instanciarMoedaNaPosicaoDoPonto()
    {
        if (this.prefabs != null && pontosDeInstanciacao != null)
        {
            // Obtendo a posição do objeto de referência
            this.posicaoReferencia = this.EmQualPontoDeveSerInstanciado().transform.position;

            // Instanciando o Obstaculo na posição do objeto de referência e salvando uma referência para o objeto instanciado
            this.objetoInstanciado = Instantiate(this.prefabs[13], this.posicaoReferencia, Quaternion.identity);
            this.qualObjetoFoiInstanciado = 13;
            this.objetoInstanciado.AddComponent<DadosDoObjeto>(); // Adicionando o script ao objeto instanciado
            this.objetoInstanciado.GetComponent<DadosDoObjeto>().instaciarObjetos(this.velocidadeDeAcordoComONivel, this.nivelDoJogo, this.qualPontoFoiInstanciado, this.qualObjetoFoiInstanciado);
            this.objetosInstanciados.Add(this.objetoInstanciado); // Adiciona o objeto à lista de objetos instanciados
        }
    }

    private void instanciarVidaNaPosicaoDoPonto()
    {
        if (this.prefabs != null && pontosDeInstanciacao != null)
        {
            // Obtendo a posição do objeto de referência
            this.posicaoReferencia = this.EmQualPontoDeveSerInstanciado().transform.position;

            // Instanciando o Obstaculo na posição do objeto de referência e salvando uma referência para o objeto instanciado
            this.objetoInstanciado = Instantiate(this.prefabs[12], this.posicaoReferencia, Quaternion.identity);
            this.qualObjetoFoiInstanciado = 12;
            this.objetoInstanciado.AddComponent<DadosDoObjeto>(); // Adicionando o script ao objeto instanciado
            this.objetoInstanciado.GetComponent<DadosDoObjeto>().instaciarObjetos(this.velocidadeDeAcordoComONivel, this.nivelDoJogo, this.qualPontoFoiInstanciado, this.qualObjetoFoiInstanciado);
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
        this.qualObjetoFoiInstanciado = objeto;
        return this.prefabs[objeto];
    }

    //Função para retornar um ponto de instancia aleatorio
    private GameObject EmQualPontoDeveSerInstanciado()
    {
        int ponto = UnityEngine.Random.Range(0, pontosDeInstanciacao.Length);
        this.qualPontoFoiInstanciado = ponto;
        return pontosDeInstanciacao[ponto];
    }

    private void atualizaTempoDeInstanciacaoDeObjetos()
    {
        float porcentagemRegressiva = 0.1f;

        if (this.temposPadroes.getTempoParaInstanciarObstaculos() >= 0.03f)
        {
            this.temposPadroes.setTempoParaInstanciarObstaculos(this.temposPadroes.getTempoParaInstanciarObstaculos() - (this.temposPadroes.getTempoParaInstanciarObstaculos() * porcentagemRegressiva));
            //this.temposPadroes.getTempoParaInstanciarObstaculos -= this.tempoParaInstanciarObstaculos * porcentagemRegressiva;
        }

        if (this.temposPadroes.getTempoParaInstanciarVida() >= 0.07f)
        {
            this.temposPadroes.setTempoParaInstanciarVida(this.temposPadroes.getTempoParaInstanciarVida() - (this.temposPadroes.getTempoParaInstanciarVida() * porcentagemRegressiva));
            //this.tempoParaInstanciarVida -= ;
        }

        if (this.temposPadroes.getTempoParaInstamciarMoeda() >= 0.01)
        {
            this.temposPadroes.setTempoParaInstamciarMoeda(this.temposPadroes.getTempoParaInstamciarMoeda() - (this.temposPadroes.getTempoParaInstamciarMoeda() * porcentagemRegressiva));
            //this.tempoParaInstamciarMoeda -= this.tempoParaInstamciarMoeda * porcentagemRegressiva;
        }
    }
}