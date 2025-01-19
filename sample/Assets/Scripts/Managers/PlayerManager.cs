using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist this object across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
    }
}
