using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CheckpointManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject cutscenePanel;       // Panel for cutscene
    [SerializeField] private GameObject tapToContinueButton; // Button to continue
    [SerializeField] private GameObject gameplayUI;          // Gameplay UI

    [Header("Cutscene Elements")]
    [SerializeField] private VideoPlayer videoPlayer;        // Video player component (optional)
    [SerializeField] private float defaultCutsceneDuration = 2f; // Default duration if no video clip

    [Header("Player and Checkpoint")]
    [SerializeField] private Transform checkpointPosition;   // Checkpoint position
    [SerializeField] private Transform player;               // Player's Transform

    private TimerManager timerManager; // Reference to TimerManager

    private void Awake()
    {
        // Ensure UI elements are hidden initially
        cutscenePanel?.SetActive(false);
        tapToContinueButton?.SetActive(false);

        // Cache the TimerManager reference
        timerManager = FindObjectOfType<TimerManager>();
        if (timerManager == null)
        {
            Debug.LogError("TimerManager not found in the scene!");
        }
    }

    public void TriggerCutscene()
    {
        // Disable gameplay UI
        if (gameplayUI != null)
        {
            gameplayUI.SetActive(false);
            Debug.Log("Gameplay UI disabled.");
        }

        // Show the cutscene panel
        cutscenePanel?.SetActive(true);

        if (videoPlayer != null && videoPlayer.clip != null)
        {
            videoPlayer.Play();
            Invoke(nameof(ShowTapToContinueButton), (float)videoPlayer.clip.length);
        }
        else
        {
            Debug.LogWarning("No video clip assigned; using default cutscene duration.");
            Invoke(nameof(ShowTapToContinueButton), defaultCutsceneDuration);
        }
    }

    private void ShowTapToContinueButton()
    {
        videoPlayer?.Stop(); // Stop the video if playing
        tapToContinueButton?.SetActive(true); // Show the "Tap to Continue" button
    }

    public void OnTapToContinue()
    {
        // Move player to checkpoint
        if (player != null && checkpointPosition != null)
        {
            player.position = checkpointPosition.position;
            Debug.Log("Player returned to checkpoint.");
        }
        else
        {
            Debug.LogError("Player or checkpoint position is not assigned!");
        }

        // Hide cutscene panel and button
        cutscenePanel?.SetActive(false);
        tapToContinueButton?.SetActive(false);

        // Enable gameplay UI
        if (gameplayUI != null)
        {
            gameplayUI.SetActive(true);
            Debug.Log("Gameplay UI re-enabled.");
        }

        // Reset and restart the timer
        if (timerManager != null)
        {
            timerManager.ResetTimer();
            timerManager.StartTimer();
            Debug.Log("Timer reset and started.");
        }
    }

    private void OnEnable()
    {
        TimerManager.OnTimerExpired += TriggerCutscene;
    }

    private void OnDisable()
    {
        TimerManager.OnTimerExpired -= TriggerCutscene;
    }
}
