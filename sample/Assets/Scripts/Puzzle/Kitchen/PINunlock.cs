using UnityEngine;

public class PINunlock : MonoBehaviour
{
    public string[] validDigits = { "3", "1", "2", "4" }; // Correct digits
    public GameObject door; // Reference to the door
    public GameObject keypadUI; // UI for keypad entry

    private string enteredDigits = "";

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
}
