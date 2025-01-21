using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    [SerializeField] private string targetScene; // Name of the scene to load
    [SerializeField] private Vector3 spawnPosition; // Position where the player spawns in the target scene
    [SerializeField] private AudioClip clockTrigger; // Sound effect for failed unlock
    [SerializeField] private AudioClip unlockSFX; // Sound effect for unlocking
    [SerializeField] private AudioClip lockedSFX; // Sound effect for failed unlock
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject pressEText; // UI text for "Press E"
    [SerializeField] private TextMeshProUGUI interactionText;


    private AudioSource audioSource; // Reference to the AudioSource

    public Key playerKey;
    public bool isLocked = true;
    private bool isPlayerNearby = false; // Tracks if the player is near the door
    public bool isInteracting = false;

    private void Start()
    {
        pressEText.SetActive(false); // Ensure "Press E" text is hidden at the start
        isLocked = true;
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {

            Debug.LogError("No AudioSource component found on the door object!");
        }



    }

    void Update()
    {
        // Check if the player presses 'E' and is near the door
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {

            if (interactionText != null)
                interactionText.gameObject.SetActive(false);

            if (playerKey.hasKey)
            {
                UnlockDoor();
            }
            else
            {
                if (audioSource != null && clockTrigger != null)
                {
                    audioSource.clip = clockTrigger;
                    audioSource.loop = true;
                    audioSource.Play();
                }
                PlayLockedSFX();
                Debug.Log("The door is locked. You need the key!");
            }
        }

        if (isInteracting)
        {
            StopLoopingAudio();
        }
    }

    public void InteractWithObject(GameObject obj)
    {
        if (obj == targetObject)
        {
            isInteracting = true; // Set interaction flag
        }
    }

    private void StopLoopingAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            Debug.Log("Stopping looping audio...");
            audioSource.loop = false;
            audioSource.Stop();
            isInteracting = false;
        }
    }


    void UnlockDoor()
    {
        if (isLocked)
        {
            isLocked = false;

            Debug.Log("The door is now unlocked!");

            // Play unlock sound
            if (audioSource && unlockSFX)
            {
                audioSource.PlayOneShot(unlockSFX);
            }

            // Add door opening logic here (e.g., disable the collider or play animation)
            GetComponent<Collider2D>().enabled = false; // Disables the door collider to allow passage

            GameManager.Instance.playerPosition = spawnPosition;
            GameManager.Instance.lastScene = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(targetScene);
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
