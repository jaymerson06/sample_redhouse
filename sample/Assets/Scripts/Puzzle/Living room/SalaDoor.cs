using UnityEngine;
using UnityEngine.SceneManagement;

public class SalaDoor : MonoBehaviour
{
    [SerializeField] private string targetScene; // Name of the scene to load
    [SerializeField] private Vector3 spawnPosition; // Position where the player spawns in the target scene
    [SerializeField] private GameObject pressEText;


    public Key playerKey;
    public bool isLocked = true;
    private bool isPlayerNearby = false; // Tracks if the player is near the door

    private void Start()
    {
        pressEText.SetActive(false);
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
            if (pressEText != null)
            {
                pressEText.SetActive(true); // Show "Press E" text
            }
            Debug.Log("Press 'E' to interact with the door.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (pressEText != null)
            {
                pressEText.SetActive(false); // Hide "Press E" text
            }
            Debug.Log("Player left the door.");
        }
    }


}
