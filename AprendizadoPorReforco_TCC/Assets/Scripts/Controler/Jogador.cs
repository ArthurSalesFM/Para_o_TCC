using UnityEngine;

public class Jogador : MonoBehaviour{

    private Colisao detectaColisao;
    private Controle_AnimacaoPlayer controleGeralJogador;
    private bool inciarAcoes = false;

    public Jogador()
    {
        this.detectaColisao = new Colisao();
        this.controleGeralJogador = new Controle_AnimacaoPlayer();
    }

    void Update()
    {
        if (this.inciarAcoes)
        {
            //this.controleGeralJogador.AtivaDesativaTeclas();
            //this.controleGeralJogador.MovimentaPersonagem();
        }        
    }

    public void setInciarAcoes (bool valor)
    {
        this.inciarAcoes = valor;
    }

   
    

}
