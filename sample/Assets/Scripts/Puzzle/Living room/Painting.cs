using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Painting : MonoBehaviour
{
    [SerializeField] private GameObject keyImage; // Image of the key to display
    [SerializeField] private GameObject obtainedKeyMessage; // Message panel
    [SerializeField] private TMP_Text messageText; // Text to display the message
    [SerializeField] private LivingRoomInventory inventory; // Reference to the inventory system
    [SerializeField] private string keyItemName = "A key from the painting."; // Name of the key
    [SerializeField] private Sprite keySprite; // Sprite for the key item in the inventory
    [SerializeField] private TimerManager timerManager;

    private bool hasInteracted = false; // To ensure the mat can only be interacted with once

    void Start()
    {
        if (timerManager == null)
        {
            Debug.LogWarning("TimerManager is not assigned to painting!");
        }

        keyImage.SetActive(false);
        obtainedKeyMessage.SetActive(false);
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
        }
    }

    private void ShowKey()
    {
        keyImage.SetActive(true);
        obtainedKeyMessage.SetActive(true);
        messageText.text = "You have obtained a key!";
        Debug.Log("Key revealed from the painting.");
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
}
