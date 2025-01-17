using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KitchenInventory : MonoBehaviour
{
    [SerializeField] private Button[] inventorySlots; // UI buttons for inventory slots
    [SerializeField] private Image[] slotImages;      // Images for each inventory slot
    [SerializeField] private Sprite paperSprite;      // Sprite for the note
    [SerializeField] private GameObject notePanel;    // Panel to display the note
    [SerializeField] private TMP_Text noteText;       // Text component for the note content

    private string[] itemContents;                   // Stores the content of items in inventory

    void Start()
    {
        // Validate setup
        if (inventorySlots.Length == 0 || slotImages.Length == 0)
        {
            Debug.LogError("Inventory slots or slot images are not set up properly!");
        }

        // Initialize the item contents array
        itemContents = new string[inventorySlots.Length];

        // Add click listeners for each inventory slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] == null)
            {
                Debug.LogError($"Inventory slot {i} is not assigned!");
                continue;
            }

            int index = i; // Capture the index for the lambda function
            inventorySlots[i].onClick.AddListener(() => DisplayNoteFromSlot(index));
        }

        // Ensure the note panel starts hidden
        notePanel.SetActive(false);
    }

    public void AddItem(string itemContent)
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (slotImages[i].sprite == null) // Find the first empty slot
            {
                // Assign the paper sprite to the slot
                slotImages[i].sprite = paperSprite;

                // Make the slot image visible
                slotImages[i].color = Color.white;

                // Store the content of the item
                itemContents[i] = itemContent;

                Debug.Log($"{itemContent} added to inventory slot {i}");
                return;
            }
        }

        Debug.Log("Inventory is full! Cannot add more items.");
    }

    private void DisplayNoteFromSlot(int index)
    {
        if (!string.IsNullOrEmpty(itemContents[index]))
        {
            Debug.Log($"Displaying note: {itemContents[index]}");
            notePanel.SetActive(true);          // Show the note panel
            noteText.text = itemContents[index]; // Set the note's text
        }
        else
        {
            Debug.Log("No note to display in this slot.");
        }
    }

    public void CloseNotePanel()
    {
        notePanel.SetActive(false); // Hide the note panel
    }
}
