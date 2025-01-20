using TMPro;
using UnityEngine;

public class GrandfatherClock : MonoBehaviour
{
    [SerializeField] private GameObject pressEText; // UI Text for "Press E"
    [SerializeField] private GameObject notePanel; // Panel for displaying the note
    [SerializeField] private TMP_Text noteText; // Text for the note content
    [SerializeField] private string noteContent = "A note from the grandfather clock."; // The content of the note
    [SerializeField] private Inventory inventory; // Reference to the inventory script
    [SerializeField] private TimerManager timerManager;
    Door door;

    [SerializeField] private AudioClip interactionSFX; // Sound effect for object interaction
    private AudioSource audioSource;

    private bool isPlayerNearby = false;
    private bool hasInteracted = false; // Flag to prevent multiple interactions

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        pressEText.SetActive(false);
        notePanel.SetActive(false);

        // Assign the Door script reference dynamically if not set in the Inspector
        if (door == null)
        {
            door = FindObjectOfType<Door>();
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasInteracted && audioSource)
        {
            pressEText.SetActive(true); // Show "Press E" text
            isPlayerNearby = true;

            if (Input.GetKeyDown(KeyCode.E)) // Player presses 'E' to interact
            {
                door.InteractWithObject(this.gameObject); // Trigger interaction in the Door script
                door.isInteracting = true; // Ensure Door interaction flag is set
                audioSource.PlayOneShot(interactionSFX);
                InteractWithClock();
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pressEText.SetActive(false); // Hide "Press E" text when player leaves
            isPlayerNearby = false;
        }
    }

    private void InteractWithClock()
    {
        ShowNote(); // Display the note
        hasInteracted = true; // Mark interaction as completed
        pressEText.SetActive(false); // Hide "Press E" text

        // Start the timer after interaction
        if (timerManager != null)
        {
            timerManager.StartTimer();
        }
    }

    private void ShowNote()
    {
        notePanel.SetActive(true); // Show the note panel
        noteText.text = noteContent; // Set the note content
        inventory.AddItem(noteContent); // Add the note to the inventory
        Debug.Log("Note from the grandfather clock added to inventory.");
    }
}
