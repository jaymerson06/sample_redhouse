using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float timeLimit = 60f; // Time limit in seconds
    [SerializeField] private TMP_Text timerText;

    private bool isTimerRunning = false;
    private float currentTime;

    public delegate void TimerExpired();
    public static event TimerExpired OnTimerExpired;

    void Start()
    {
        timerText.gameObject.SetActive(false); // Hide the timer initially
        currentTime = timeLimit;
        UpdateTimerUI();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                isTimerRunning = false;
                OnTimeUp(); // Call when the timer reaches 0
            }

            UpdateTimerUI(); // Update the UI every frame
        }

        // Change timer text color when time is running low
        if (currentTime <= 10)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.white;
        }
    }

    private void UpdateTimerUI()
    {
        // Update the text to display the remaining time
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = $"Time Left: {minutes:00}:{seconds:00}"; // Format as MM:SS
    }

    private void OnTimeUp()
    {
        Debug.Log("Time is up!");
        // Invoke the event for other parts of the game to respond
        OnTimerExpired?.Invoke();
        // Additional functionality like starting a cutscene can be triggered here
    }

    // Start the timer
    public void StartTimer()
    {
        currentTime = timeLimit;
        isTimerRunning = true;
        timerText.gameObject.SetActive(true);
        UpdateTimerUI();
        Debug.Log("Timer started!");
    }

    // Stop the timer
    public void StopTimer()
    {
        isTimerRunning = false;
        timerText.gameObject.SetActive(false);
        Debug.Log("Timer stopped!");
    }

    // Reset the timer
    public void ResetTimer()
    {
        currentTime = timeLimit;
        isTimerRunning = false;
        UpdateTimerUI(); // Optionally, reset the timer UI to reflect the starting time
    }
}
