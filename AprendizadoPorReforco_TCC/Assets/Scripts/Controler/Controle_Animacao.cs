using UnityEngine;

public class Controle_Animacao : MonoBehaviour
{
    private Animator idle;
    private float velocidadeCaminhada = 5f;
    private float velocidadeCorrida = 12.5f;
    private bool teclaW_Pressionada;
    private bool teclaA_Pressionada;
    private bool teclaD_Pressionada;
    private bool teclaShift_Pressionada;
    private bool teclaSpace_Pressionada;
    private bool teclaEnterPrecionada;

    void Start()
    {
        this.idle = GetComponent<Animator>();
        this.teclaW_Pressionada = false;
        this.teclaA_Pressionada = false;
        this.teclaD_Pressionada = false;
        this.teclaShift_Pressionada = false;
        this.teclaEnterPrecionada = false;
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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            this.DesativaAnimacoes();
            this.idle.SetBool("idle_2", true);
            this.teclaEnterPrecionada = true;
            Debug.Log("Enter");
        }

        //Pressionar W
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.teclaW_Pressionada = true;
            print("Ativou w");
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            this.teclaW_Pressionada = false;
            print("Desativou w");
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

        //Pressionar Shift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            this.teclaShift_Pressionada = true;
            print("Ativou Shift");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            this.teclaShift_Pressionada = false;
            print("Desativou Shift");
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

        if (this.teclaEnterPrecionada == true)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

            float velocidadeAtual = this.teclaShift_Pressionada ? velocidadeCorrida : velocidadeCaminhada;

            transform.position += movement * Time.deltaTime * velocidadeAtual;
        }
        
    }

    private void ExecutaAnimacoes()
    {

        if (this.teclaEnterPrecionada == true)
        {

            //se nenhuma tecla estiver pressionada, volta para o idle
            if ( !this.teclaW_Pressionada && !this.teclaA_Pressionada && !this.teclaD_Pressionada)
            {
                this.DesativaAnimacoes();
                this.idle.SetBool("idle_2", true);
            }

            else
            {
                /*
                 *  ANIMAÇÕES ANDANDO
                 */
                //andar frente
                if (this.teclaW_Pressionada && !this.teclaShift_Pressionada)
                {
                    this.DesativaAnimacoes();
                    this.idle.SetBool("AndarFrente", true);
                }

                //andar esquerda
                if (this.teclaA_Pressionada && !this.teclaShift_Pressionada)
                {
                    this.DesativaAnimacoes();
                    this.idle.SetBool("AndarEsquerda", true);
                }

                //andar direita
                if (this.teclaD_Pressionada && !this.teclaShift_Pressionada)
                {
                    this.DesativaAnimacoes();
                    this.idle.SetBool("AndarDireita", true);
                }

                /*
                 *  ANIMAÇÕES CORRENDO
                 */
                //Correr para frente
                if (this.teclaW_Pressionada && this.teclaShift_Pressionada)
                {
                    this.DesativaAnimacoes();
                    this.idle.SetBool("CorrerFrente", true);
                }

                //correr para esquerda
                if (this.teclaA_Pressionada && this.teclaShift_Pressionada)
                {
                    this.DesativaAnimacoes();
                    this.idle.SetBool("CorrerEsquerda", true);
                }

                //correr para direita
                if (this.teclaD_Pressionada && this.teclaShift_Pressionada)
                {
                    this.DesativaAnimacoes();
                    this.idle.SetBool("CorrerDireita", true);
                }

                //Pulando
                if (this.teclaSpace_Pressionada && this.teclaW_Pressionada && this.teclaShift_Pressionada)
                {
                    this.DesativaAnimacoes();
                    this.idle.SetBool("Pular", true);
                }
            }
        }       
    }

    private void DesativaAnimacoes()
    {
        this.idle.SetBool("idle_2", false);
        this.idle.SetBool("AndarFrente", false);
        this.idle.SetBool("AndarEsquerda", false);
        this.idle.SetBool("AndarDireita", false);
        this.idle.SetBool("CorrerFrente", false);
        this.idle.SetBool("CorrerEsquerda", false);
        this.idle.SetBool("CorrerDireita", false);
        this.idle.SetBool("Pular", false);
    }
}
