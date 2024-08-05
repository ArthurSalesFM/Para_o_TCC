/*using UnityEngine;

public class AgenteAprendizado : MonoBehaviour
{
    public LineRenderer linhaDoRaio;  // Adicione o LineRenderer via Unity Inspector
    private ControleMovimentacaoIA controleDeAnimacaoIA;
    private TabelaVelocidadeDeMudancaDePosicao tabelaVelocidadeDeMudancaDePosicao;


    private void Start()
    {

        // Verificar se pontosDeInstanciacao está inicializado
        if (Base_Treinamento.pontosDeInstanciacao == null || Base_Treinamento.pontosDeInstanciacao.Length == 0)
        {
            Debug.LogError("Base_Treinamento.pontosDeInstanciacao não está inicializado corretamente.");
            return;
        }

        // Verificar se todos os elementos em pontosDeInstanciacao são diferentes de null
        foreach (var posicao in Base_Treinamento.pontosDeInstanciacao)
        {
            if (posicao == null)
            {
                Debug.LogError("Um dos GameObjects em pontosDeInstanciacao é null.");
                return;
            }
        }
        this.controleDeAnimacaoIA = new ControleMovimentacaoIA();
        this.tabelaVelocidadeDeMudancaDePosicao = new TabelaVelocidadeDeMudancaDePosicao(Base_Treinamento.pontosDeInstanciacao, controleDeAnimacaoIA.getVelocidadeCorrida());
        this.tabelaVelocidadeDeMudancaDePosicao.printaTabela();
    }


    // Função chamada a cada quadro
    void Update()
    {
        // A cada quadro, obtemos observações, tomamos decisões e executamos ações
        bool observacao = ObterObservacao(out _);
        TomarDecisao();
        ExecutarAcao();
        
    }
    //private List

    // Função para obter observações do ambiente
    private bool ObterObservacao(out string objetoDetectado)
    {
        // Lógica para detecção de objetos ao redor
        int numeroDeRaios = 30; // Ajuste conforme necessário
        float anguloInicial = -25f; // Ângulo inicial em graus
        float anguloTotal = 45f; // Ângulo total do círculo em graus
        float distanciaDeVerificacao = 300.0f;

        // Calcula a posição do meio do agente (aqui assumindo que a altura do agente é apropriada)
        Vector3 pontoDoMeio = transform.position + new Vector3(0f, 1.5f, 0f);

        linhaDoRaio.positionCount = numeroDeRaios * 2;
        Vector3[] positions = new Vector3[numeroDeRaios * 2];

        for (int i = 0; i < numeroDeRaios; i++)
        {
            float anguloAtual = anguloInicial + (anguloTotal / (float)(numeroDeRaios - 1)) * i;
            Vector3 direcaoDoRaio = Quaternion.Euler(0f, anguloAtual, 0f) * transform.forward;

            Ray raio = new Ray(pontoDoMeio, direcaoDoRaio);
            RaycastHit hit;

            // Visualização do raio (linhas vermelhas)
            Debug.DrawRay(raio.origin, raio.direction * distanciaDeVerificacao, Color.red);

            // Lógica para verificar colisões com os raios
            if (Physics.Raycast(raio, out hit, distanciaDeVerificacao))
            {
                positions[i * 2] = pontoDoMeio;  // posição inicial do raio
                positions[i * 2 + 1] = hit.point;  // posição final do raio após a colisão

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
                positions[i * 2] = pontoDoMeio;  // posição inicial do raio
                positions[i * 2 + 1] = raio.origin + raio.direction * distanciaDeVerificacao;  // posição final do raio sem colisão
            }
        }

        linhaDoRaio.SetPositions(positions);

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

        // Adicionar verificação para getVelocidadeCorrida()
        float velocidade = controleDeAnimacaoIA.getVelocidadeCorrida();
        if (velocidade <= 0)
        {
            Debug.LogError("Velocidade de corrida é inválida.");
            return;
        }
        //this.tabelaVelocidadeDeMudancaDePosicao.printaTabela();
    }

    // Função chamada a cada quadro
    void Update()
    {
        // A cada quadro, obtemos observações, tomamos decisões e executamos ações
        bool observacao = ObterObservacao(out _);
        TomarDecisao();
        ExecutarAcao();
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


