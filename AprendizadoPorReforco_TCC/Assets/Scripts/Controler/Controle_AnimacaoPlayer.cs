using UnityEngine;

public class Controle_AnimacaoPlayer : MonoBehaviour
{
    private Animator idle;
    private float velocidadeCorrida = 12.5f;
    private bool teclaA_Pressionada;
    private bool teclaD_Pressionada;
    private bool teclaSpace_Pressionada;

    void Start()
    {
        this.idle = GetComponent<Animator>();
        this.teclaA_Pressionada = false;
        this.teclaD_Pressionada = false;
        this.teclaSpace_Pressionada = false;
    }

    void Update()
    {
        this.AtivaDesativaTeclas();
        this.MovimentaPersonagem();
    }

    private void AtivaDesativaTeclas()
    {       
        //Inicio - Pressionar Enter
        if (BaseDoJogo.IniciarJogo)
        {
            this.DesativaAnimacoes();
            this.idle.SetBool("CorrerFrente", true);            
            Debug.Log("Enter");
        }

        //Pressionar A
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.teclaA_Pressionada = true;
            print("Ativou A");
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            this.teclaA_Pressionada = false;
            print("Desativou A");
        }

        //Pressionar D
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.teclaD_Pressionada = true;
            print("Ativou D");
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            this.teclaD_Pressionada = false;
            print("Desativou D");
        }

        //Pressionar Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.teclaSpace_Pressionada = true;
            print("Ativou o Pulo");
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            this.teclaSpace_Pressionada = false;
            print("Desativou o Pulo");
        }

        this.ExecutaAnimacoes();
    }

    private void MovimentaPersonagem()
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
            moveVertical = 3f; // Movimento para cima (eixo Y)
        }

        // Movimenta o personagem apenas no eixo Y
        Vector3 verticalMovement = new Vector3(0.0f, moveVertical, 0.0f);
        transform.position += verticalMovement * Time.deltaTime * velocidadeCorrida;
    }


    private void ExecutaAnimacoes()
    {

        if (BaseDoJogo.IniciarJogo)
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
            if (this.teclaD_Pressionada )
            {
                this.DesativaAnimacoes();
                this.idle.SetBool("CorrerDireita", true);
            }

            //Pulando
            if ( this.teclaSpace_Pressionada && this.idle.GetBool("CorrerFrente"))
            {
                this.DesativaAnimacoes();
                this.idle.SetBool("Pular", true);
            }
            
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