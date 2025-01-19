using UnityEngine;

public class PINUnlock : MonoBehaviour
{
    public string[] validDigits = { "3", "1", "2", "4" }; // Correct digits
    public GameObject door; // Reference to the door
    public GameObject keypadUI; // UI for keypad entry

    private string enteredDigits = "";
    private bool isPlayerNear = false; // Track if the player is near the keypad

    void Update()
    {
        // Check if the player is near and presses 'E' to interact
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            OpenKeypadUI();
        }
    }

    void OpenKeypadUI()
    {
        keypadUI.SetActive(true); // Show the keypad UI
        Time.timeScale = 0f; // Pause the game
    }

    public void AddDigit(string digit)
    {
        if (enteredDigits.Length < 4)
        {
            enteredDigits += digit;
        }

        if (enteredDigits.Length == 4)
        {
            CheckPIN();
        }
    }

    void CheckPIN()
    {
        // Check if entered digits contain all validDigits
        foreach (string validDigit in validDigits)
        {
            if (!enteredDigits.Contains(validDigit))
            {
                Debug.Log("Incorrect PIN!");
                enteredDigits = ""; // Reset on failure
                return;
            }
        }

        Debug.Log("Correct PIN! Door unlocked.");
        UnlockDoor();
    }

    void UnlockDoor()
    {
        // Logic to unlock the door
        door.GetComponent<KitchenDoor>().Unlock();
    }

    public void CloseKeypadUI()
    {
        keypadUI.SetActive(false); // Hide the keypad UI
        Time.timeScale = 1f; // Resume the game
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Press 'E' to interact with the keypad.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}

