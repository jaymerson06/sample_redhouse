using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public AudioSource unlockSound; // Reference to the AudioSource

    private bool isUnlocked = false;

    void Start()
    {
        if (unlockSound == null)
        {
            unlockSound = GetComponent<AudioSource>();
        }
    }

    public void UnlockDoor()
    {
        if (!isUnlocked)
        {
            isUnlocked = true; // Prevent multiple unlocks
            unlockSound.Play(); // Play the unlock sound effect
            Debug.Log("Door unlocked!");
            // Add additional logic to open the door here
        }
    }
}
