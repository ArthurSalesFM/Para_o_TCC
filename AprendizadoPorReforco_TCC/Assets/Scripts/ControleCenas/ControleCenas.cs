using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleCenas : MonoBehaviour
{    

    public ControleCenas(int cena)
    {
        SceneManager.LoadScene(cena);
    }

}
