using System.Collections.Generic;
using System.Linq;

public class NoPrincipalDaArvore 
{
    private int pontuacaoMaximaDoCaminho;
    private List<TestaTodosOsCaminhos> shapes = new List<TestaTodosOsCaminhos>();
    // Refer�ncia para o filho � esquerda
    private NoPrincipalDaArvore filhoEsquerdo;

    // Refer�ncia para o filho � direita
    private NoPrincipalDaArvore filhoDireito;

    // Construtor que inicializa o n� com um valor
    public NoPrincipalDaArvore(int valor, string[,] shape)
    {
        this.pontuacaoMaximaDoCaminho = valor; // Armazena o valor no n�
        AdicionarMatrizSeNaoExistir(shape);
        
        this.filhoEsquerdo = null; // Inicialmente, o filho � esquerda � nulo
        this.filhoDireito = null; // Inicialmente, o filho � direita � nulo 
    }
    
    
    public void AdicionarMatrizSeNaoExistir(string[,] novaMatriz)
    {
        // Verifica se a matriz j� existe na lista
        TestaTodosOsCaminhos shape;
        //shape.setaValores(novaMatriz);

        if(this.shapes.Count == 0)
        {
            shape = new TestaTodosOsCaminhos();
            shape.setaValores(novaMatriz);
            this.shapes.Add(shape);
        }


        //bool matrizExiste = MatrizEhIgual();

        // Se a matriz n�o existir, adiciona � lista

    }

    // M�todo auxiliar para comparar duas matrizes
    private bool MatrizEhIgual(string[,] matriz1, string[,] matriz2)
    {
        // Verifica se as dimens�es s�o diferentes
        if (matriz1.GetLength(0) != matriz2.GetLength(0) || matriz1.GetLength(1) != matriz2.GetLength(1))
        {
            return false;
        }

        // Verifica se os valores das matrizes s�o diferentes
        for (int i = 0; i < matriz1.GetLength(0); i++)
        {
            for (int j = 0; j < matriz1.GetLength(1); j++)
            {
                // Aqui voc� precisa implementar a compara��o dos elementos do tipo TestaTodosOsCaminhos
                // A compara��o abaixo � apenas um exemplo. Ajuste conforme necess�rio.
                if (matriz1[i,j] != matriz2[i,j])
                {
                    return false;
                }
            }
        }

        return true; // As matrizes s�o iguais
    }

    // Getter para filhoEsquerdo
    public NoPrincipalDaArvore getFilhoEsquerdo()
    {
        return filhoEsquerdo;
    }

    // Getter para filhoDireito
    public NoPrincipalDaArvore getFilhoDireito()
    {
        return filhoDireito;
    }

    // M�todos para definir os filhos, se necess�rio
    public void setFilhoEsquerdo(NoPrincipalDaArvore filho)
    {
        this.filhoEsquerdo = filho;
    }

    public void setFilhoDireito(NoPrincipalDaArvore filho)
    {
        this.filhoDireito = filho;
    }

    // Getter para pontuacaoMaximaDoCaminho
    public int getPontuacaoMaximaDoCaminho()
    {
        return this.pontuacaoMaximaDoCaminho;
    }

    // M�todo auxiliar para comparar dois elementos do tipo TestaTodosOsCaminhos
    private bool MatrizElementoEhIgual(TestaTodosOsCaminhos elemento1, string[,] elemento2)
    {
        // Aqui voc� deve definir como comparar dois elementos do tipo TestaTodosOsCaminhos
        // Exemplo de compara��o: se eles forem iguais ou se tiverem alguma propriedade espec�fica que pode ser comparada
        return elemento1.Equals(elemento2);
    }

}
