using UnityEngine;

public class ControleMovimentacaoIA : MonoBehaviour
{
    private Animator idle;
    private float velocidadeCorrida = 12.5f;
    private bool irParaEsquerda;
    private bool irParaDireita;
    private bool pular;

    private void Awake()
    {
        this.idle = GetComponent<Animator>();
    }

    public ControleMovimentacaoIA()
    {        
        this.irParaEsquerda = false;
        this.irParaDireita = false;
        this.pular = false;
    }

    public float getVelocidadeCorrida()
    {
        return this.velocidadeCorrida;
    }

    //FALTA FAZER (PARTE IMPORTANTE PARA SABER ONDE A IA IRÁ E A ANIMAÇÂO QUE DEVE EXECULTAR
    private void ativaIntencao()
    {
        /*
        //Pressionar A
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.irParaEsquerda = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            this.irParaEsquerda = false;
        }

        //Pressionar D
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.irParaDireita = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            this.irParaDireita = false;
        }

        //Pressionar Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.pular = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            this.pular = false;
        }

        this.executaAnimacoes();
        */
    }

    private void movimentaIA()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        // Verifica as teclas pressionadas para movimentar o personagem no eixo X
        if (this.irParaEsquerda)
        {
            moveHorizontal = -1f; // Movimento para a esquerda
        }
        else if (this.irParaDireita)
        {
            moveHorizontal = 1f; // Movimento para a direita
        }

        // Movimenta o personagem apenas no eixo X
        Vector3 horizontalMovement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        transform.position += horizontalMovement * Time.deltaTime * velocidadeCorrida;

        // Limita a posição do personagem entre -17 e 17 no eixo X
        float posX = Mathf.Clamp(transform.position.x, -17f, 17f);
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);

        // Verifica se a tecla de espaço foi pressionada para mover o personagem no eixo Y
        if (this.pular)
        {
            moveVertical = 2f; // Movimento para cima (eixo Y)
        }

        // Movimenta o personagem apenas no eixo Y
        Vector3 verticalMovement = new Vector3(0.0f, moveVertical, 0.0f);
        transform.position += verticalMovement * Time.deltaTime * velocidadeCorrida;
    }

    //Executa a animação de acordo com as informações setadas
    private void executaAnimacoes()
    {
        if (!this.irParaEsquerda && !this.irParaDireita)
        {
            this.desativaAnimacoes();
            this.idle.SetBool("CorrerFrente", true);
        }

        //correr para esquerda
        if (this.irParaEsquerda)
        {
            this.desativaAnimacoes();
            this.idle.SetBool("CorrerEsquerda", true);
        }

        //correr para direita
        if (this.irParaDireita)
        {
            this.desativaAnimacoes();
            this.idle.SetBool("CorrerDireita", true);
        }

        //Pulando
        if (this.pular && this.idle.GetBool("CorrerFrente"))
        {
            this.desativaAnimacoes();
            this.idle.SetBool("Pular", true);
        }
    }

    //Para desativar qualquer animação que a IA esteja realizando
    private void desativaAnimacoes()
    {
        this.idle.SetBool("CorrerFrente", false);
        this.idle.SetBool("CorrerEsquerda", false);
        this.idle.SetBool("CorrerDireita", false);
        this.idle.SetBool("Pular", false);
    }

}
