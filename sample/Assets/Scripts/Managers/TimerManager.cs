using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float timeLimit = 60f; // Time limit in seconds
    [SerializeField] private TMP_Text timerText;    // Timer UI text element

    private float currentTime;
    private bool isTimerRunning = false;

    // Delegate and event for notifying when the timer expires
    public delegate void TimerExpired();
    public static event TimerExpired OnTimerExpired;

    private void Awake()
    {
        // Initialize timer and hide the timer UI
        currentTime = timeLimit;
        timerText?.gameObject.SetActive(false);
        UpdateTimerUI();
    }

    private void Update()
    {
        if (!isTimerRunning) return;

        currentTime -= Time.deltaTime;

        // Handle timer expiration
        if (currentTime <= 0)
        {
            currentTime = 0;
            isTimerRunning = false;
            OnTimeUp();
        }

        // Update the timer UI every frame
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        if (timerText == null) return;

        // Update the timer text (format MM:SS)
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";

        // Change the text color when time is running low
        timerText.color = currentTime <= 10 ? Color.red : Color.white;
    }

    private void OnTimeUp()
    {
        Debug.Log("Time is up!");

        // Invoke the event to notify other scripts
        OnTimerExpired?.Invoke();

        // Optionally, you can trigger additional logic here (e.g., cutscenes).
    }

    // Public Methods for Timer Control
    public void StartTimer()
    {
        currentTime = timeLimit;
        isTimerRunning = true;
        timerText?.gameObject.SetActive(true);
        Debug.Log("Timer started!");
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        timerText?.gameObject.SetActive(false);
        Debug.Log("Timer stopped!");
    }

    public void ResetTimer()
    {
        currentTime = timeLimit;
        isTimerRunning = false;
        UpdateTimerUI();
        Debug.Log("Timer reset!");
    }

    public float GetCurrentTime() => currentTime;
}
