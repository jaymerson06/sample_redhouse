using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DustyCarpet : MonoBehaviour
{
    [SerializeField] private GameObject keyImage; // Image of the key to display
    [SerializeField] private GameObject obtainedKeyMessage; // Message panel
    [SerializeField] private TMP_Text messageText; // Text to display the message
    [SerializeField] private Inventory inventory; // Reference to the inventory system
    [SerializeField] private string keyItemName = "A key from under the mat."; // Name of the key
    [SerializeField] private Sprite keySprite; // Sprite for the key item in the inventory
    [SerializeField] private TimerManager timerManager;

    [SerializeField] private AudioClip interactionSFX; // Sound effect for object interaction
    private AudioSource audioSource;

    private bool hasInteracted = false; // To ensure the mat can only be interacted with once

    void Start()
    {
        if (timerManager == null)
        {
            Debug.LogWarning("TimerManager is not assigned to DustyCarpet!");
        }

        keyImage.SetActive(false);
        obtainedKeyMessage.SetActive(false);

        // Initialize the AudioSource if it's not already attached
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if not already present
        }
    }

    private void OnMouseDown()
    {
        if (!hasInteracted)
        {
            hasInteracted = true; // Prevent further interaction

            if (timerManager != null)
            {
                timerManager.StopTimer(); // Stop the timer upon interaction
            }
            ShowKey();
            AddKeyToInventory();
            audioSource.PlayOneShot(interactionSFX); // Play the interaction sound
        }
    }

    private void ShowKey()
    {
        keyImage.SetActive(true);
        obtainedKeyMessage.SetActive(true);
        messageText.text = "You have obtained a key!";
        Debug.Log("Key revealed from under the mat.");
    }

    private void AddKeyToInventory()
    {
        inventory.AddItem(keyItemName);
        Debug.Log("Key added to inventory.");
    }

    public void CloseMessage()
    {
        // Close the message panel and key image
        keyImage.SetActive(false);
        obtainedKeyMessage.SetActive(false);
    }

    // Removed OnTriggerEnter2D, as the interaction is now solely based on a mouse click
}
