using UnityEngine;
using TMPro;

public class BaseDoJogo : MonoBehaviour
{
    private float tempoTotal = 10.0f; // Tempo total em segundos
    private float tempoAtual; // Tempo atual restante
    private int nivelDoJogo = 0;
    public TMP_Text textoNivelDoJogo;
    public TMP_Text textoDoCronometro; // Referência ao texto onde o cronômetro será exibido
    public static bool IniciarJogo = false; // Inicio do jogo

    void Start()
    {
        tempoAtual = tempoTotal;
        AtualizarTextoCronometro(); // Atualiza o texto inicialmente
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            IniciarJogo = true;
        }

        if (IniciarJogo)
        {
            tempoAtual -= Time.deltaTime;

            if (tempoAtual <= 0f) // Se o tempo acabar
            {
                this.nivelDoJogo++;
                textoNivelDoJogo.text = "Nível: " + this.nivelDoJogo;
                tempoAtual = tempoTotal;                
            }

            AtualizarTextoCronometro();
        }
    }

    private void AtualizarTextoCronometro()
    {
        int minutos = Mathf.FloorToInt(tempoAtual / 60f);
        int segundos = Mathf.FloorToInt(tempoAtual % 60f);
        string textoTempo = string.Format("{0:00}:{1:00}", minutos, segundos);

        if (textoDoCronometro != null)
        {
            textoDoCronometro.text = "Tempo: " + textoTempo;
        }
    }
}
