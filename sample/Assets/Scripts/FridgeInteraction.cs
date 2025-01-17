using TMPro;
using UnityEngine;

public class FridgeInteraction : MonoBehaviour
{
    [SerializeField] private GameObject pressEText;      // UI text to prompt "Press E"
    [SerializeField] private GameObject notePanel;       // Panel to display the note
    [SerializeField] private TMP_Text noteText;          // Text component to show the note's content
    [SerializeField] private string noteContent = "A note from the fridge."; // The note's content
    [SerializeField] private Inventory inventory;        // Reference to the player's inventory

    private bool isPlayerNearby = false;                // Track if the player is near the fridge
    private bool hasInteracted = false;                 // Ensure one-time interaction

    void Start()
    {
        // Hide the "Press E" text and the note panel at the start
        pressEText.SetActive(false);
        notePanel.SetActive(false);
    }

    void Update()
    {
        // Check if the player is nearby and presses "E"
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !hasInteracted)
        {
            ShowNote();                // Display the note
            hasInteracted = true;      // Mark interaction as complete
            pressEText.SetActive(false); // Hide the "Press E" prompt
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detect if the player enters the trigger area
        if (other.CompareTag("Player") && !hasInteracted)
        {
            Debug.Log("Player entered the trigger area of the fridge");
            pressEText.SetActive(true); // Show the "Press E" prompt
            isPlayerNearby = true;     // Mark the player as nearby
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Detect if the player leaves the trigger area
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger area of the fridge");
            pressEText.SetActive(false); // Hide the "Press E" prompt
            isPlayerNearby = false;     // Mark the player as no longer nearby
        }
    }

    private void ShowNote()
    {
        // Display the note on the screen and add it to the inventory
        Debug.Log("Displaying note from the fridge");
        notePanel.SetActive(true);       // Show the note panel
        noteText.text = noteContent;     // Set the note's text
        inventory.AddItem(noteContent);  // Add the note to the inventory
    }
}
