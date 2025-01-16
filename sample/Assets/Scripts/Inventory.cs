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
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (slotImages[i].sprite == null) // Empty slot found
            {
                // Assign the correct sprite based on the itemName
                if (itemName == "A key from under the mat")
                {
                    slotImages[i].sprite = keySprite;
                }
                else if (itemName == "A note from the grandfather clock.")
                {
                    slotImages[i].sprite = paperSprite;
                }

                slotImages[i].color = Color.white; // Ensure the sprite is visible
                itemContents[i] = itemName; // Save the item's content for this slot

                // Add a click listener to display the item's details
                slotImages[i].GetComponent<Button>().onClick.RemoveAllListeners();
                slotImages[i].GetComponent<Button>().onClick.AddListener(() => DisplayNote(itemName));

                Debug.Log(itemName + " added to slot " + i);
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
}
