using UnityEngine;
using UnityEngine.Tilemaps;

public class Fridge : MonoBehaviour
{
    public GameObject noteUI; // UI to display the note with instructions
    [SerializeField] private GameObject pressEText; // UI Text for "Press E"
    [SerializeField] private string noteContent = "A note from the fridge."; // The content of the note
    [SerializeField] private KitchenInventory inventory; // Reference to the inventory script

    [SerializeField] private AudioClip interactionSFX; // Sound effect for object interaction
    private AudioSource audioSource;

    private bool isPlayerNear = false;
    private bool hasInteracted = false; // Flag to prevent multiple interactions

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if not already present
        }

        pressEText.SetActive(false);
        noteUI.SetActive(false);
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasInteracted)
        {
            pressEText.SetActive(true); // Show "Press E" text
            isPlayerNear = true;

            if (Input.GetKeyDown(KeyCode.E)) // Player presses 'E' to interact
            {
                audioSource.PlayOneShot(interactionSFX);
                OpenFridge();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pressEText.SetActive(false); // Hide "Press E" text when player leaves
            isPlayerNear = false;
        }
    }

    void OpenFridge()
    {
        // Display the note UI
        ShowNote();
        hasInteracted = true; // Mark interaction as completed
        pressEText.SetActive(false); // Hide "Press E" text

        // Start the timer after interaction


    }

    private void ShowNote()
    {
        noteUI.SetActive(true); // Show the note panel
        inventory.AddItem(noteContent); // Add the note to the inventory
        Debug.Log("Note from the grandfather clock added to inventory.");
    }
}
