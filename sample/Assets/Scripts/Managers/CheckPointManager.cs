using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private GameObject cutscenePanel; // Panel to hold the video
    [SerializeField] private VideoPlayer videoPlayer; // Video player component
    [SerializeField] private GameObject tapToContinueButton; // Button to continue
    [SerializeField] private Transform checkpointPosition; // Player's checkpoint position
    [SerializeField] private Transform player; // Player's Transform

    private void OnEnable()
    {
        TimerManager.OnTimerExpired += TriggerCutscene;
    }

    private void OnDisable()
    {
        TimerManager.OnTimerExpired -= TriggerCutscene;
    }

    private void TriggerCutscene()
    {
        cutscenePanel.SetActive(true);
        videoPlayer.Play(); // Start the cutscene video
        Invoke(nameof(ShowTapToContinueButton), (float)videoPlayer.clip.length); // Show button after video ends
    }

    private void ShowTapToContinueButton()
    {
        videoPlayer.Stop(); // Stop the video after it ends
        tapToContinueButton.SetActive(true); // Show the continue button
    }

    public void OnTapToContinue()
    {
        // Return player to checkpoint
        player.position = checkpointPosition.position;

        // Hide cutscene and button
        cutscenePanel.SetActive(false);
        tapToContinueButton.SetActive(false);

        // Reset the timer
        TimerManager timer = Object.FindFirstObjectByType<TimerManager>();
        if (timer != null)
        {
            timer.ResetTimer();
        }
    }
}
