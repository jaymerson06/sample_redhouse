using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    private void Start()
    {
        // Check if GameManager exists and if there's a saved player position
        if (GameManager.Instance != null && GameManager.Instance.lastScene != null)
        {
            GameObject player = GameObject.FindWithTag("Player");

            if (player != null)
            {
                Debug.Log("Setting player's position to: " + GameManager.Instance.playerPosition);
                // Set the player's position to the saved position
                player.transform.position = GameManager.Instance.playerPosition;
            }
            else
            {
                Debug.LogError("Player object not found in the scene!");
            }
        }
        else
        {
            Debug.LogWarning("GameManager instance or lastScene is null.");
        }
    }
}
