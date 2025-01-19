using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Button[] inventorySlots; // UI slots
    [SerializeField] private Image[] slotImages;
    [SerializeField] private Sprite paperSprite;
    [SerializeField] private GameObject notePanel;
    [SerializeField] private TMP_Text noteText;
    [SerializeField] private Sprite keySprite;

    private string[] itemContents; // Stores item names or descriptions
    private bool hasInteracted; // Tracks if the player has already interacted

    void Start()
    {
        if (slotImages.Length == 0 || inventorySlots.Length == 0)
        {
            Debug.LogError("Inventory slots or slot images are not set up properly!");
        }

        itemContents = new string[inventorySlots.Length];
        hasInteracted = false;

        // Add click listeners for each inventory slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] == null)
            {
                Debug.LogError($"Inventory slot {i} is not assigned!");
                continue;
            }

            int index = i; // Capture index for lambda
            inventorySlots[i].onClick.AddListener(() => DisplayNoteFromSlot(index));
        }


    }


    public void AddItem(string itemName)
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (slotImages[i].sprite == null) // Find an empty slot
            {
                // Assign the appropriate sprite based on the item
                if (itemName == "A key from under the mat.")
                {
                    slotImages[i].sprite = keySprite;
                }
                else if (itemName == "A note from the grandfather clock.")
                {
                    slotImages[i].sprite = paperSprite;
                }
                else
                {
                    Debug.LogError($"Unknown item: {itemName}");
                    return;
                }

                // Ensure the slot's sprite is visible
                slotImages[i].color = Color.white;

                // Store the item's content
                itemContents[i] = itemName;

                Debug.Log($"{itemName} added to inventory slot {i}");
            

                // Add click functionality to the slot
                var button = slotImages[i].GetComponent<Button>();
                if (button != null) // Check if Button exists
                {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() => DisplayNote(itemName));
                }
                else
                {
                    Debug.LogError($"Slot {i} is missing a Button component!");
                }

                Debug.Log(itemName + " added to inventory slot " + i);
                hasInteracted = true; // Mark as interacted to prevent re-interaction
                return;
            }
        }

        Debug.Log("Inventory full!");
    }



    private void DisplayNoteFromSlot(int index)
    {
        if (!string.IsNullOrEmpty(itemContents[index]))
        {
            Debug.Log($"Displaying note: {itemContents[index]}");
            notePanel.SetActive(true);
            noteText.text = itemContents[index];
        }
    }

    private void DisplayNote(string content)
    {
        notePanel.SetActive(true);

        if (content == "A key from under the mat")
        {
            noteText.text = "This is a key you found under the mat. It might open something.";
        }
        else
        {
            noteText.text = content;
        }

        Debug.Log("Displaying note or key content: " + content);
    }

    // New method for interacting with an item
    public void InteractWithItem(string itemName)
    {
        if (hasInteracted)
        {
            Debug.Log("You have already collected this item.");
            return; // Prevent further interactions
        }

        AddItem(itemName);
        Debug.Log($"Item '{itemName}' has been collected.");
    }
}
