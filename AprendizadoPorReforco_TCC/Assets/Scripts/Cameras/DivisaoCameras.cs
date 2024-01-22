using UnityEngine;

public class DivisaoCameras : MonoBehaviour
{
    public Camera Cjogador;
    public Camera CIA;

    void Start()
    {
        Cjogador = GameObject.Find("CameraJogador").GetComponent<Camera>();
        CIA = GameObject.Find("CameraIA").GetComponent<Camera>(); // Aqui deve ser "CameraPlayer2"

        Rect jogador = new Rect(0f, 0f, 0.5f, 1f); // Divis�o � esquerda
        Rect IA = new Rect(0.5f, 0f, 0.5f, 1f); // Divis�o � direita

        Cjogador.rect = jogador;
        CIA.rect = IA;
    }
}

