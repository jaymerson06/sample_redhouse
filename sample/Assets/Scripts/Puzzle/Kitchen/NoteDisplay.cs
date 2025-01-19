using UnityEngine;

public class NoteDisplay : MonoBehaviour
{
    public GameObject noteUI;

    public void ShowNote()
    {
        noteUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void CloseNote()
    {
        noteUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
}
