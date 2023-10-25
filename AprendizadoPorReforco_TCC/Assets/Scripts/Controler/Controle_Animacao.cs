using UnityEngine;

public class Controle_Animacao : MonoBehaviour
{
    private Animator idle;
    private bool teclaW_Pressionanda;
    private bool teclaA_Pressionanda;
    private bool teclaD_Pressionanda;
    private bool teclaShift_Pressionanda;

    void Start()
    {
        this.idle = GetComponent<Animator>();
        this.teclaW_Pressionanda = false;
        this.teclaA_Pressionanda = false;
        this.teclaD_Pressionanda = false;
        this.teclaShift_Pressionanda = false;
    }

    void Update()
    {
        AtivaDesativaTeclas();
    }

    private void AtivaDesativaTeclas()
    {
        //Inicio - Pressionar Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            this.desativaAnimacoes();
            this.idle.SetBool("Vai_Para_idle2", true);
        }

        //Pressionar W
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.teclaW_Pressionanda = true;
            print("Ativou w");
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            this.teclaW_Pressionanda = false;
            print("Desativou w");
        }

        //Pressionar A
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.teclaA_Pressionanda = true;
            print("Ativou A");
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            this.teclaA_Pressionanda = false;
            print("Desativou A");
        }

        //Pressionar D
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.teclaD_Pressionanda = true;
            print("Ativou D");
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            this.teclaD_Pressionanda = false;
            print("Desativou D");
        }

        //Pressionar Shift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            this.teclaShift_Pressionanda = true;
            print("Ativou Shift");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            this.teclaShift_Pressionanda = false;
            print("Desativou Shift");
        }

        this.executaAnimacoes();

    }

    private void executaAnimacoes()
    {

        //
        //ANDAR =====================================================================
        //

        //Andar para Frente
        if (this.idle.GetBool("Vai_Para_idle2") && this.teclaW_Pressionanda)
        {
            this.desativaAnimacoes();
            this.idle.SetBool("Vai_Para_walking_1", true);
        }
        // Parar de Andar para Frente
        else if (this.idle.GetBool("Vai_Para_walking_1") && !this.teclaW_Pressionanda)
        {
            this.desativaAnimacoes();
            this.idle.SetBool("Vai_Para_idle2", true);
        }


        //Andar para Esquerda
        if (this.idle.GetBool("Vai_Para_idle2") && this.teclaA_Pressionanda)
        {
            this.desativaAnimacoes();
            this.idle.SetBool("Vai_Para_left_strafe_walking", true);
        }
        // Parar de Andar para Esquerda
        else if (this.idle.GetBool("Vai_Para_left_strafe_walking") && !this.teclaA_Pressionanda)
        {
            this.desativaAnimacoes();
            this.idle.SetBool("Vai_Para_idle2", true);
        }


        //Andar para Direita
        if (this.idle.GetBool("Vai_Para_idle2") && this.teclaD_Pressionanda)
        {
            this.desativaAnimacoes();
            this.idle.SetBool("Vai_Para_right_strafe_walking", true);
        }
        // Parar de Andar para Direita
        else if (this.idle.GetBool("Vai_Para_right_strafe_walking") && !this.teclaD_Pressionanda)
        {
            this.desativaAnimacoes();
            this.idle.SetBool("Vai_Para_idle2", true);
        }


        //
        //Correndo =====================================================================
        //

        //Correr para Frente
        if (this.idle.GetBool("Vai_Para_walking_1") &&  this.teclaShift_Pressionanda)
        {
            this.desativaAnimacoes();
            this.idle.SetBool("Vai_Para_running", true);
        }
        else if (this.idle.GetBool("Vai_Para_running") && !this.teclaShift_Pressionanda)
        {
            this.desativaAnimacoes();
            this.idle.SetBool("Vai_Para_walking_1", true);
        }


        //Correr para Esquerda
        if (this.idle.GetBool("Vai_Para_left_strafe_walking") && this.teclaShift_Pressionanda)
        {
            this.desativaAnimacoes();
            this.idle.SetBool("Vai_Para_left_strafe", true);
        }
        else if (this.idle.GetBool("Vai_Para_left_strafe") && !this.teclaShift_Pressionanda)
        {
            this.desativaAnimacoes();
            this.idle.SetBool("Vai_Para_left_strafe_walking", true);
        }


        //Correr para Direita
        if (this.idle.GetBool("Vai_Para_right_strafe_walking") && this.teclaShift_Pressionanda)
        {
            this.desativaAnimacoes();
            this.idle.SetBool("Vai_Para_right_strafe", true);
        }
        else if (this.idle.GetBool("Vai_Para_right_strafe") && !this.teclaShift_Pressionanda)
        {
            this.desativaAnimacoes();
            this.idle.SetBool("Vai_Para_right_strafe_walking", true);
        }

    }  


    private void desativaAnimacoes()
    {
        this.idle.SetBool("Ativa_acknowledging", false);
        this.idle.SetBool("Ativa_happy_hand_gesture", false);
        this.idle.SetBool("Ativa_head_nod_yes", false);
        this.idle.SetBool("Ativa_shaking_head_no", false);
        this.idle.SetBool("Ativa_dismissing_gesture", false);
        this.idle.SetBool("Ativa_being_cocky", false);
        this.idle.SetBool("Vai_Para_idle2", false);
        this.idle.SetBool("Volta_idle", false);
        this.idle.SetBool("Vai_Para_idle3", false);
        this.idle.SetBool("Vai_Para_idle4", false);
        this.idle.SetBool("Vai_Para_left_strafe_walking", false);
        this.idle.SetBool("Vai_Para_right_strafe_walking", false);
        this.idle.SetBool("Vai_Para_left_strafe", false);
        this.idle.SetBool("Vai_Para_right_strafe", false);
        this.idle.SetBool("Vai_Para_running", false);
        this.idle.SetBool("Vai_para_jump", false);
        this.idle.SetBool("Vai_Para_walking_1", false);
        this.idle.SetBool("Vai_Para_run_to_stop", false);
        this.idle.SetBool("Vai_para_jumping_up", false);
        this.idle.SetBool("Vai_Para_falling_idle", false);
        this.idle.SetBool("Vai_para_falling_to_roll", false);
        this.idle.SetBool("Vai_para_hard_landing", false);

    }
}