using UnityEngine;

public class Controle_AnimacaoPlayer_2 : MonoBehaviour
{
    private Animator idle;
    private float velocidadeCorrida = 12.5f;
    private bool tecla4Esquer_Pressionada;
    private bool tecla6Direit_Pressionada;
    private bool tecla8Espaco_Pressionada;
    //

    private void Start()
    {
        this.idle = GetComponent<Animator>();
        this.tecla4Esquer_Pressionada = false;
        this.tecla6Direit_Pressionada = false;
        this.tecla8Espaco_Pressionada = false;
    }

    private void Update()
    {

        if (BaseDoJogo.comecarOJogo)
        {
            this.AtivaDesativaTeclas();
            this.MovimentaPersonagem();
        }
    }


    private void AtivaDesativaTeclas()
    {

        //Pressionar A
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            this.tecla4Esquer_Pressionada = true;
        }
        else if (Input.GetKeyUp(KeyCode.Keypad4))
        {
            this.tecla4Esquer_Pressionada = false;
        }

        //Pressionar D
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            this.tecla6Direit_Pressionada = true;
        }
        else if (Input.GetKeyUp(KeyCode.Keypad6))
        {
            this.tecla6Direit_Pressionada = false;
        }

        //Pressionar Space
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            this.tecla8Espaco_Pressionada = true;
        }
        else if (Input.GetKeyUp(KeyCode.Keypad8))
        {
            this.tecla8Espaco_Pressionada = false;
        }

        this.ExecutaAnimacoes();
    }

    private void MovimentaPersonagem()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        // Verifica as teclas pressionadas para movimentar o personagem no eixo X
        if (this.tecla4Esquer_Pressionada)
        {
            moveHorizontal = -1f; // Movimento para a esquerda
        }
        else if (this.tecla6Direit_Pressionada)
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
        if (this.tecla8Espaco_Pressionada)
        {
            moveVertical = 2f; // Movimento para cima (eixo Y)
        }

        // Movimenta o personagem apenas no eixo Y
        Vector3 verticalMovement = new Vector3(0.0f, moveVertical, 0.0f);
        transform.position += verticalMovement * Time.deltaTime * velocidadeCorrida;
    }

    private void ExecutaAnimacoes()
    {
        if (!this.tecla4Esquer_Pressionada && !this.tecla6Direit_Pressionada)
        {
            this.DesativaAnimacoes();
            this.idle.SetBool("CorrerFrente", true);
        }

        //correr para esquerda
        if (this.tecla4Esquer_Pressionada)
        {
            this.DesativaAnimacoes();
            this.idle.SetBool("CorrerEsquerda", true);
        }

        //correr para direita
        if (this.tecla6Direit_Pressionada)
        {
            this.DesativaAnimacoes();
            this.idle.SetBool("CorrerDireita", true);
        }

        //Pulando
        if (this.tecla8Espaco_Pressionada && this.idle.GetBool("CorrerFrente"))
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
