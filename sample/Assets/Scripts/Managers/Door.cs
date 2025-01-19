using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.Cinemachine;


public class Door : MonoBehaviour
{
    public string targetScene; // Name of the scene to load
    public Vector3 spawnPosition; // Position where the player spawns in the target scene
 

    public Key playerKey;
    public bool isLocked = true;
    private bool isPlayerNearby = false; // Tracks if the player is near the door

    private void Start()
    {
        isLocked = true;
        
    }

    void Update()
    {
        // Check if the player presses 'E' and is near the door
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (playerKey.hasKey)
            {
                UnlockDoor();
            }
            else
            {
                Debug.Log("The door is locked. You need the key!");
            }
        }
    }


    void UnlockDoor()
    {
        if (isLocked)
        {
            isLocked = false;

            Debug.Log("The door is now unlocked!");
            // Add door opening logic here (e.g., disable the collider or play animation)
            GetComponent<Collider2D>().enabled = false; // Disables the door collider to allow passage

            GameManager.Instance.playerPosition = spawnPosition;
            GameManager.Instance.lastScene = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(targetScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure it's the player
        {
            isPlayerNearby = true;
            Debug.Log("Press 'E' to interact with the door.");
        }
    }


}
