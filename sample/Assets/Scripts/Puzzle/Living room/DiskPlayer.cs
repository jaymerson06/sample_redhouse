using TMPro;
using UnityEngine;

public class DiskPlayer : MonoBehaviour
{
    [SerializeField] private GameObject pressEText; // UI Text for "Press E"
    [SerializeField] private GameObject notePanel; // Panel for displaying the note
    [SerializeField] private TMP_Text noteText; // Text for the note content
    [SerializeField] private string noteContent = "A note from the diskplayer."; // The content of the note
    [SerializeField] private LivingRoomInventory inventory; // Reference to the inventory script
    [SerializeField] private TimerManager timerManager;

    private bool isPlayerNearby = false;
    private bool hasInteracted = false; // Flag to prevent multiple interactions

    void Start()
    {
        pressEText.SetActive(false);
        notePanel.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasInteracted)
        {
            pressEText.SetActive(true); // Show "Press E" text
            isPlayerNearby = true;

            if (Input.GetKeyDown(KeyCode.E)) // Player presses 'E' to interact
            {
                InteractWithDisk();
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

    private void InteractWithDisk()
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
        Debug.Log("Note from the disk added to inventory.");
    }
}
