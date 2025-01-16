using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrandfatherClockInteraction : MonoBehaviour
{
    [SerializeField] private GameObject pressEText;
    [SerializeField] private GameObject notePanel;
    [SerializeField] private TMP_Text noteText;
    [SerializeField] private string noteContent = "A note from the grandfather clock.";
    [SerializeField] private Inventory inventory;

    private bool isPlayerNearby = false;
    private bool hasInteracted = false;

    void Start()
    {
        pressEText.SetActive(false);
        notePanel.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !hasInteracted)
        {
            ShowNote();
            hasInteracted = true; // Prevent further interactions
            pressEText.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasInteracted)
        {
            Debug.Log("Player entered the trigger area of the Grandfather Clock");
            pressEText.SetActive(true);
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger area of the Grandfather Clock");
            pressEText.SetActive(false);
            isPlayerNearby = false;
        }
    }

    private void ShowNote()
    {
        Debug.Log("Displaying note from the Grandfather Clock");
        notePanel.SetActive(true);
        noteText.text = noteContent;
        inventory.AddItem(noteContent);
    }
}
