using UnityEngine.SceneManagement;
using UnityEngine;

public class MainDoor : MonoBehaviour
{
    [SerializeField] private string targetScene; // Name of the scene to load
    [SerializeField] private GameObject pressEText; // UI text for "Press E

    private bool isPlayerNearby = false; // Tracks if the player is near the door


    private void Start()
    {
        pressEText.SetActive(false);
    }
    void Update()
    {
        // Check if the player presses 'E' and is near the door
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
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
        if (other.CompareTag("Player")) // Ensure it's the player
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
