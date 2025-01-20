using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private GameObject cutscenePanel; // Panel to hold the video or animation
    [SerializeField] private VideoPlayer videoPlayer; // Video player component (optional)
    [SerializeField] private GameObject tapToContinueButton; // Button to continue
    [SerializeField] private Transform checkpointPosition; // Player's checkpoint position
    [SerializeField] private Transform player; // Player's Transform

    private void Start()
    {
        // Ensure cutscenePanel and tapToContinueButton are initially hidden
        if (cutscenePanel != null) cutscenePanel.SetActive(false);
        if (tapToContinueButton != null) tapToContinueButton.SetActive(false);
    }

    public void TriggerCutscene()
    {
        if (cutscenePanel != null)
        {
            cutscenePanel.SetActive(true);
        }

        if (videoPlayer != null)
        {
            videoPlayer.Play(); // Start the cutscene video
            Invoke(nameof(ShowTapToContinueButton), (float)videoPlayer.clip.length); // Show button after video ends
        }
        else
        {
            // Assume cutscene duration is 2 seconds if no video is used
            Invoke(nameof(ShowTapToContinueButton), 2f);
        }
    }

    private void ShowTapToContinueButton()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop(); // Stop the video after it ends
        }

        if (tapToContinueButton != null)
        {
            tapToContinueButton.SetActive(true); // Show the continue button
        }
    }

    public void OnTapToContinue()
    {
        // Return player to checkpoint
        if (player != null && checkpointPosition != null)
        {
            player.position = checkpointPosition.position;
        }

        // Hide cutscene and button
        if (cutscenePanel != null)
        {
            cutscenePanel.SetActive(false);
        }

        if (tapToContinueButton != null)
        {
            tapToContinueButton.SetActive(false);
        }

        // Reset the timer
        TimerManager timer = Object.FindFirstObjectByType<TimerManager>();
        if (timer != null)
        {
            timer.ResetTimer();
        }
    }
}
