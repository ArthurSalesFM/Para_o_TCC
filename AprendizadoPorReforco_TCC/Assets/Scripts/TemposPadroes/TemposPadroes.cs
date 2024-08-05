
public class TemposPadroes
{
    // Atributos que controlam o tempo do jogo
    private float tempoTotalParaMudancaDeNivel = 40.0f; // Tempo total em segundos
    private float tempoParaIniciarOutroNivel = 10.0f; // Tempo de espera para iniciar novamente instancias de objetos
    private float tempoParaInstanciarObstaculos = 2.3f;
    private float tempoParaInstanciarVida = 7.0f;
    private float tempoParaInstamciarMoeda = 1.5f;

    public float getTempoTotalParaMudancaDeNivel()
    {
        return this.tempoTotalParaMudancaDeNivel;
    }

    public float getTempoParaIniciarOutroNivel()
    {
        return this.tempoParaIniciarOutroNivel;
    }
    public void setTempoParaIniciarOutroNivel(float valor)
    {
        this.tempoParaIniciarOutroNivel = valor;
    }

    public float getTempoParaInstanciarObstaculos()
    {
        return this.tempoParaInstanciarObstaculos;
    }

    public void setTempoParaInstanciarObstaculos(float valor)
    {
        this.tempoParaInstanciarObstaculos = valor;
    }

    public float getTempoParaInstanciarVida()
    {
        return this.tempoParaInstanciarVida;
    }

    public void setTempoParaInstanciarVida(float valor)
    {
        this.tempoParaInstanciarVida = valor;
    }

    public float getTempoParaInstamciarMoeda()
    {
        return this.tempoParaInstamciarMoeda;
    }

    public void setTempoParaInstamciarMoeda(float valor)
    {
        this.tempoParaInstamciarMoeda = valor;
    }
}
