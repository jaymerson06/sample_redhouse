using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PINUnlock : MonoBehaviour
{
    public string correctPIN = "3124"; // Correct PIN
    [SerializeField] private GameObject pressFText; // "Press E" UI Text
    [SerializeField] private GameObject keypadUI; // Keypad UI
    [SerializeField] private TMP_Text enteredDigitsText; // Text to display entered digits
    [SerializeField] private TMP_Text messageText; // Text to display messages
    [SerializeField] private TimerManager timerManager;
    public GameObject door; // Reference to the door

    [SerializeField] private AudioClip interactionSFX; // Sound effect for object interaction
    private AudioSource audioSource;

    private string enteredDigits = ""; // To store entered digits
    private bool isPlayerNear = false; // Check if the player is near the keypad

    void Start()
    {
        pressFText.SetActive(false);
        keypadUI.SetActive(false);
        messageText.text = "";
        enteredDigitsText.text = ""; // Clear UI text initially

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if not already present
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            audioSource.PlayOneShot(interactionSFX);
            OpenKeypadUI();
        }
    }

    void OpenKeypadUI()
    {
        Debug.Log("Opening Keypad UI.");
        keypadUI.SetActive(true); // Show the keypad UI
    }

    public void AddDigit(string digit)
    {
        if (enteredDigits.Length < 4) // Allow only up to 4 digits
        {
            enteredDigits += digit;
            enteredDigitsText.text = enteredDigits; // Update the displayed digits
        }
    }

    public void CheckPIN()
    {
        if (enteredDigits == correctPIN)
        {
            Debug.Log("Correct PIN! Door unlocked.");
            pressFText.SetActive(false);
            messageText.text = "Door Unlocked!";
            timerManager.StopTimer();
            UnlockDoor();
        }
        else
        {
            Debug.Log("Incorrect PIN!");
            messageText.text = "Incorrect PIN";
        }

        enteredDigits = ""; // Reset the entered digits after checking
        enteredDigitsText.text = ""; // Clear the displayed digits
    }

    void UnlockDoor()
    {
        if (door != null)
        {
            door.GetComponent<KitchenDoor>()?.Unlock(); // Unlock the door
        }
        else
        {
            Debug.LogError("Door reference is missing!");
        }

        CloseKeypadUI(); // Close the keypad UI after unlocking
    }

    public void CloseKeypadUI()
    {
        keypadUI.SetActive(false); // Hide the keypad UI
        messageText.text = ""; // Clear the message text
   
    }

    private void OnTriggerEnter2D(Collider2D other) // Updated to 2D
    {
        if (other.CompareTag("Player"))
        {
            pressFText.SetActive(true); // Show "Press E" text
            isPlayerNear = true;
            Debug.Log("Player entered the keypad zone. Press 'F' to interact.");
        }
    }

    private void OnTriggerExit2D(Collider2D other) // Updated to 2D
    {
        if (other.CompareTag("Player"))
        {
            pressFText.SetActive(false); // Hide "Press E" text
            isPlayerNear = false;
            Debug.Log("Player left the keypad zone.");
        }
    }
}

