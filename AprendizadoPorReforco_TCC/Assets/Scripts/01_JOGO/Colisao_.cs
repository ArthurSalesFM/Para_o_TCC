using UnityEngine;

public class Colisao_: MonoBehaviour {

    
    private void OnCollisionEnter(Collision collision) {  

        switch (collision.gameObject.tag) {

            case "moeda":
                BaseDoJogo.moedasColetadasPelaIA++;
                Destroy(collision.gameObject);
                break;

            case "barreira":
                BaseDoJogo.vidaDaIA--;
                BaseDoJogo.moedasColetadasPelaIA -= 10;
                Destroy(collision.gameObject);
                break;

            case "vida":
                if (BaseDoJogo.vidaDaIA <= 4) {
                    BaseDoJogo.vidaDaIA++;
                }

                else if (BaseDoJogo.vidaDaIA == 5) {
                    BaseDoJogo.moedasColetadasPelaIA += 20;
                }

                Destroy(collision.gameObject);
                break;
        }
    }
}
