using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    public static PlayerFinder Instance;
    public GameObject player;

    void Awake()
    {
        Instance = this;
    }
}
