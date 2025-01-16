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

    private string[] itemContents; // Stores item names or descriptions

    void Start()
    {
        itemContents = new string[inventorySlots.Length];

        // Add click listeners for each inventory slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            int index = i; // Capture index for lambda
            inventorySlots[i].onClick.AddListener(() => DisplayNoteFromSlot(index));
        }
    }

    public void AddItem(string itemName)
    {
        Debug.Log("AddItem called with: " + itemName);

        for (int i = 0; i < slotImages.Length; i++)
        {
            Debug.Log("Checking slot " + i);

            if (slotImages[i].sprite == null) // Empty slot found
            {
                Debug.Log("Empty slot found at index: " + i);
                slotImages[i].sprite = paperSprite;
                slotImages[i].color = Color.white; // Make the slot visible
                itemContents[i] = itemName; // Save the item's description or name

                Debug.Log($"Paper sprite assigned to slot {i} with item: {itemName}");
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
}
