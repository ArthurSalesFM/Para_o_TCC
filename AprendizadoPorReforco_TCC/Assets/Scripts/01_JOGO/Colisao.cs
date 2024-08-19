using UnityEngine;

public class Colisao : MonoBehaviour {

    
    private void OnCollisionEnter(Collision collision) {  

        switch (collision.gameObject.tag) {

            case "moeda":
                BaseDoJogo.moedasColetadasPeloJogador++;
                Destroy(collision.gameObject);
                break;

            case "barreira":
                BaseDoJogo.vidaDoJogador--;
                BaseDoJogo.moedasColetadasPeloJogador -= 10;
                Destroy(collision.gameObject);
                break;

            case "vida":
                if (BaseDoJogo.vidaDoJogador <= 4) {
                    BaseDoJogo.vidaDoJogador++;
                }

                else if (BaseDoJogo.vidaDoJogador == 5) {
                    BaseDoJogo.moedasColetadasPeloJogador += 20;
                }

                Destroy(collision.gameObject);
                break;
        }
    }
}
