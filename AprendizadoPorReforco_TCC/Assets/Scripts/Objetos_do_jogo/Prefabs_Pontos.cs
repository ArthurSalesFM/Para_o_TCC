using UnityEngine;

public class Prefabs_Pontos : MonoBehaviour
{
    public GameObject []prefabs;
    public GameObject[] pontos;
    
    public GameObject[] todosOsPrefabs()
    {
        return prefabs;
    }

    public GameObject[] todosOsPontos()
    {
        return pontos;
    }
}
