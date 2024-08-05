/*using UnityEngine;

public class AgenteAprendizado : MonoBehaviour
{
    public LineRenderer linhaDoRaio;  // Adicione o LineRenderer via Unity Inspector
    private ControleMovimentacaoIA controleDeAnimacaoIA;
    private TabelaVelocidadeDeMudancaDePosicao tabelaVelocidadeDeMudancaDePosicao;


    private void Start()
    {

        // Verificar se pontosDeInstanciacao est� inicializado
        if (Base_Treinamento.pontosDeInstanciacao == null || Base_Treinamento.pontosDeInstanciacao.Length == 0)
        {
            Debug.LogError("Base_Treinamento.pontosDeInstanciacao n�o est� inicializado corretamente.");
            return;
        }

        // Verificar se todos os elementos em pontosDeInstanciacao s�o diferentes de null
        foreach (var posicao in Base_Treinamento.pontosDeInstanciacao)
        {
            if (posicao == null)
            {
                Debug.LogError("Um dos GameObjects em pontosDeInstanciacao � null.");
                return;
            }
        }
        this.controleDeAnimacaoIA = new ControleMovimentacaoIA();
        this.tabelaVelocidadeDeMudancaDePosicao = new TabelaVelocidadeDeMudancaDePosicao(Base_Treinamento.pontosDeInstanciacao, controleDeAnimacaoIA.getVelocidadeCorrida());
        this.tabelaVelocidadeDeMudancaDePosicao.printaTabela();
    }


    // Fun��o chamada a cada quadro
    void Update()
    {
        // A cada quadro, obtemos observa��es, tomamos decis�es e executamos a��es
        bool observacao = ObterObservacao(out _);
        TomarDecisao();
        ExecutarAcao();
        
    }
    //private List

    // Fun��o para obter observa��es do ambiente
    private bool ObterObservacao(out string objetoDetectado)
    {
        // L�gica para detec��o de objetos ao redor
        int numeroDeRaios = 30; // Ajuste conforme necess�rio
        float anguloInicial = -25f; // �ngulo inicial em graus
        float anguloTotal = 45f; // �ngulo total do c�rculo em graus
        float distanciaDeVerificacao = 300.0f;

        // Calcula a posi��o do meio do agente (aqui assumindo que a altura do agente � apropriada)
        Vector3 pontoDoMeio = transform.position + new Vector3(0f, 1.5f, 0f);

        linhaDoRaio.positionCount = numeroDeRaios * 2;
        Vector3[] positions = new Vector3[numeroDeRaios * 2];

        for (int i = 0; i < numeroDeRaios; i++)
        {
            float anguloAtual = anguloInicial + (anguloTotal / (float)(numeroDeRaios - 1)) * i;
            Vector3 direcaoDoRaio = Quaternion.Euler(0f, anguloAtual, 0f) * transform.forward;

            Ray raio = new Ray(pontoDoMeio, direcaoDoRaio);
            RaycastHit hit;

            // Visualiza��o do raio (linhas vermelhas)
            Debug.DrawRay(raio.origin, raio.direction * distanciaDeVerificacao, Color.red);

            // L�gica para verificar colis�es com os raios
            if (Physics.Raycast(raio, out hit, distanciaDeVerificacao))
            {
                positions[i * 2] = pontoDoMeio;  // posi��o inicial do raio
                positions[i * 2 + 1] = hit.point;  // posi��o final do raio ap�s a colis�o

                // Verifica se o objeto atingido tem a tag "barreira", "moeda" ou "vida"
                if (hit.collider.CompareTag("barreira"))
                {
                    objetoDetectado = "barreira";
                    return true;
                }
                else if (hit.collider.CompareTag("moeda"))
                {
                    objetoDetectado = "moeda";
                    return true;
                }
                else if (hit.collider.CompareTag("vida"))
                {
                    objetoDetectado = "vida";
                    return true;
                }
            }
            else
            {
                positions[i * 2] = pontoDoMeio;  // posi��o inicial do raio
                positions[i * 2 + 1] = raio.origin + raio.direction * distanciaDeVerificacao;  // posi��o final do raio sem colis�o
            }
        }

        linhaDoRaio.SetPositions(positions);

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

  

}*/

using UnityEngine;

public class AgenteAprendizado : MonoBehaviour
{
    public LineRenderer linhaDoRaio;  // Adicione o LineRenderer via Unity Inspector
    private ControleMovimentacaoIA controleDeAnimacaoIA;
    //private TabelaVelocidadeDeMudancaDePosicao tabelaVelocidadeDeMudancaDePosicao = new TabelaVelocidadeDeMudancaDePosicao();

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
    void Update()
    {
        // A cada quadro, obtemos observa��es, tomamos decis�es e executamos a��es
        bool observacao = ObterObservacao(out _);
        TomarDecisao();
        ExecutarAcao();
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


