using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatInteraction : MonoBehaviour
{
    [SerializeField] private GameObject keyImage; // Image of the key to display
    [SerializeField] private GameObject obtainedKeyMessage; // Message panel
    [SerializeField] private TMP_Text messageText; // Text to display the message
    [SerializeField] private Inventory inventory; // Reference to the inventory system
    [SerializeField] private Sprite keySprite; // Sprite for the key item in the inventory

    private bool hasInteracted = false; // To ensure the mat can only be interacted with once

    void Start()
    {
        keyImage.SetActive(false);
        obtainedKeyMessage.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!hasInteracted)
        {
            hasInteracted = true; // Prevent further interaction
            ShowKey();
            AddKeyToInventory();
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

