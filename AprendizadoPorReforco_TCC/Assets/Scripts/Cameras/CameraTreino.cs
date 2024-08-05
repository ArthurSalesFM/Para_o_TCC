using UnityEngine;

public class CameraTreino : MonoBehaviour
{
    public Camera cameraIATreinamento;

    void Start()
    {
        cameraIATreinamento = GameObject.Find("CameraIA").GetComponent<Camera>(); // Aqui deve ser "CameraPlayer2"
        Rect IA = new Rect(0f, 0f, 1f, 1f); // Divisão à direita
        cameraIATreinamento.rect = IA;
    }
}
