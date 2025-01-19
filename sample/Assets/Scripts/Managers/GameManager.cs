using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    public Vector3 playerPosition; // To store the player's position
    public string lastScene; // To store the last scene the player was in

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
    }

}

