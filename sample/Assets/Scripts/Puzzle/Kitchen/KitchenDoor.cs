using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class KitchenDoor : MonoBehaviour
{
    public string targetScene; // Name of the scene to load
    public Vector3 spawnPosition; // Position where the player spawns in the target scene
    [SerializeField] private GameObject pressEText; // UI text for "Press E"

    private bool isUnlocked = false; // Tracks if the door is unlocked
    private bool isPlayerNearby = false; // Tracks if the player is near the door

    [SerializeField] private AudioClip unlockSFX; // Sound effect for unlocking
    [SerializeField] private AudioClip lockedSFX; // Sound effect for failed unlock
    private AudioSource audioSource; // Reference to the AudioSource


    void Start()
    {
        pressEText.SetActive(false); // Ensure "Press E" text is hidden at the start
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on the door object!");
        }
    }

    public void Unlock()
    {
        isUnlocked = true;
        Debug.Log("Kitchen Door is now unlocked!");
        // Add any animation or visual feedback here (e.g., door opening)
    }

    void Update()
    {
        if (isUnlocked && isPlayerNearby && Input.GetKeyDown(KeyCode.E)) 
        {
            // Logic to transition to the next scene
            GetComponent<Collider2D>().enabled = false; // Disable the door collider to allow passage

            GameManager.Instance.playerPosition = spawnPosition; // Save player position in the target scene
            GameManager.Instance.lastScene = SceneManager.GetActiveScene().name; // Save the current scene name

            SceneManager.LoadScene(targetScene); // Load the target scene
        }
        else if (!isUnlocked && isPlayerNearby && Input.GetKeyDown(KeyCode.E) && audioSource)
        {
            PlayLockedSFX();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is near the door."); // Check if this logs in the console
            isPlayerNearby = true;
            if (pressEText != null)
            {
                pressEText.SetActive(true); // Show "Press E" text
            }
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

    private void PlayLockedSFX()
    {
        // Play locked sound
        if (audioSource && lockedSFX)
        {
            audioSource.PlayOneShot(lockedSFX);
        }
    }
}
