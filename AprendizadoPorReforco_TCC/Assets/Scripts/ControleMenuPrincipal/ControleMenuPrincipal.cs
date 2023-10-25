using UnityEngine;

public class ControleMenuPrincipal : MonoBehaviour
{
    private ControleCenas cena;
    
    public void goTo()
    {
        this.cena = new ControleCenas(1);
    }

}
