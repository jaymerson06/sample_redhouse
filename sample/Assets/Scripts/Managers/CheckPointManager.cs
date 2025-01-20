using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private GameObject cutscenePanel; // Panel for cutscene
    [SerializeField] private VideoPlayer videoPlayer; // Video player component (optional)
    [SerializeField] private GameObject tapToContinueButton; // Button to continue
    [SerializeField] private Transform checkpointPosition; // Checkpoint position
    [SerializeField] private Transform player; // Player's Transform

    private void Start()
    {
        // Ensure UI elements are hidden initially
        if (cutscenePanel != null) cutscenePanel.SetActive(false);
        if (tapToContinueButton != null) tapToContinueButton.SetActive(false);
    }

    public void TriggerCutscene()
    {
        if (cutscenePanel != null)
        {
            cutscenePanel.SetActive(true); // Show the cutscene panel
        }

        if (videoPlayer != null && videoPlayer.clip != null)
        {
            videoPlayer.Play(); // Play the video
            Invoke(nameof(ShowTapToContinueButton), (float)videoPlayer.clip.length); // Wait for the video to end
        }
        else
        {
            Debug.LogWarning("No video clip assigned; assuming default duration.");
            Invoke(nameof(ShowTapToContinueButton), 2f); // Default cutscene duration
        }
    }

    private void ShowTapToContinueButton()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop(); // Stop the video
        }

        if (tapToContinueButton != null)
        {
            tapToContinueButton.SetActive(true); // Show the "Continue" button
        }
    }

    public void OnTapToContinue()
    {
        // Return the player to the checkpoint position
        if (player != null && checkpointPosition != null)
        {
            player.position = checkpointPosition.position;
            Debug.Log("Player returned to checkpoint.");
        }
        else
        {
            Debug.LogError("Player or checkpoint position is not assigned!");
        }

        // Hide the cutscene panel and button
        if (cutscenePanel != null) cutscenePanel.SetActive(false);
        if (tapToContinueButton != null) tapToContinueButton.SetActive(false);

        // Reset and restart the timer
        TimerManager timer = FindObjectOfType<TimerManager>();
        if (timer != null)
        {
            timer.ResetTimer();
            timer.StartTimer();
            Debug.Log("Timer reset and started.");
        }
        else
        {
            Debug.LogError("TimerManager not found in the scene!");
        }
    }
}
