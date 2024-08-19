using UnityEngine;

public class ControleAnimacaoIA : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator idle;
    private float velocidadeCorrida = 12.5f;
    private bool teclaA_Pressionada;
    private bool teclaD_Pressionada;
    private bool teclaSpace_Pressionada;
    private bool correrFrente;
    //

    private void Start()
    {
        this.idle = GetComponent<Animator>();
        this.teclaA_Pressionada = false;
        this.teclaD_Pressionada = false;
        this.teclaSpace_Pressionada = false;
    }
        
    private void Update()
    {

        if (Base_Treinamento.comecarOJogo)
        {
            ExecutaAnimacoes();
            //MovimentaPersonagem();

        }
    }

    public float getVelocidadeCorrida()
    {
        return this.velocidadeCorrida;
    }

    public void setaTudoFalso()
    {
        this.teclaA_Pressionada = false;
        this.teclaD_Pressionada = false;
        this.teclaSpace_Pressionada = false;
        this.correrFrente = false;
    }

    public void AtivaDesativaTeclas(int valor)
    {
        setaTudoFalso();

        if (valor == 0)
        {
            this.teclaA_Pressionada = true;
        }
        else if (valor == 1)
        {
            this.teclaD_Pressionada = true;
        }
        else if (valor == 2)
        {
            this.teclaSpace_Pressionada = true;
        }
        else
        {
            this.correrFrente = true;
        }

        //this.ExecutaAnimacoes();

        //this.MovimentaPersonagem(); ;
    }

    public void MovimentaPersonagem()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        // Verifica as teclas pressionadas para movimentar o personagem no eixo X
        if (this.teclaA_Pressionada)
        {
            moveHorizontal = -1f; // Movimento para a esquerda
        }
        else if (this.teclaD_Pressionada)
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
        if (this.teclaSpace_Pressionada)
        {
            moveVertical = 2f; // Movimento para cima (eixo Y)
        }

        // Movimenta o personagem apenas no eixo Y
        Vector3 verticalMovement = new Vector3(0.0f, moveVertical, 0.0f);
        transform.position += verticalMovement * Time.deltaTime * velocidadeCorrida;

        
    }

    private void ExecutaAnimacoes()
    {
        if (!this.teclaA_Pressionada && !this.teclaD_Pressionada)
        {
            this.DesativaAnimacoes();
            this.idle.SetBool("CorrerFrente", true);
        }

        //correr para esquerda
        if (this.teclaA_Pressionada)
        {
            this.DesativaAnimacoes();
            this.idle.SetBool("CorrerEsquerda", true);
        }

        //correr para direita
        if (this.teclaD_Pressionada)
        {
            this.DesativaAnimacoes();
            this.idle.SetBool("CorrerDireita", true);
        }

        //Pulando
        if (this.teclaSpace_Pressionada && this.idle.GetBool("CorrerFrente"))
        {
            this.DesativaAnimacoes();
            this.idle.SetBool("Pular", true);
        }

    }

    private void DesativaAnimacoes()
    {
        this.idle.SetBool("CorrerFrente", false);
        this.idle.SetBool("CorrerEsquerda", false);
        this.idle.SetBool("CorrerDireita", false);
        this.idle.SetBool("Pular", false);
    }

}
