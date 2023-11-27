using UnityEngine;

public class Colisao : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "moeda":
                BaseDoJogo.moedasColetadasPeloJogador++;
                Destroy(collision.gameObject);
                break;

            case "barreira":
                BaseDoJogo.vidaDoJogador--;
                Destroy(collision.gameObject);
                break;

            case "vida":
                if (BaseDoJogo.vidaDoJogador <= 5)
                {
                    BaseDoJogo.vidaDoJogador++;
                }
                Destroy(collision.gameObject);
                break;
        }
    }
}
