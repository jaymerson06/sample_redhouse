using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GhostAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float speed = 3f; // Speed of the ghost

    public Animator animator; // Animator for movement animations
    [SerializeField] private GameObject cutscenePanel;
    public VideoPlayer cutscenePlayer; // Assign in Inspector for the cutscene
    public GameObject tapToContinueButton; // Assign in Inspector for the "Tap to Continue" UI
    public AudioManager audioManager; // Reference to your AudioManager

    private bool isPlayingCutscene = false; // Tracks whether the cutscene is active

    private void Start()
    {
        cutscenePanel?.SetActive(false);

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (cutscenePlayer != null)
        {
            cutscenePlayer.gameObject.SetActive(false); // Ensure the cutscene is inactive at the start
        }

        if (tapToContinueButton != null)
        {
            tapToContinueButton.SetActive(false); // Ensure the button is inactive at the start
        }
    }

    void Update()
    {
        if (!isPlayingCutscene)
        {
            // Move towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;

            // Update movement animations
            animator.SetBool("isWalking", true);
            animator.SetFloat("InputX", direction.x);
            animator.SetFloat("InputY", direction.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayingCutscene)
        {
            TriggerCutscene();
        }
    }

    private void TriggerCutscene()
    {
        cutscenePanel?.SetActive(true);

        // Stop background music
        if (audioManager != null)
        {
            audioManager.StopMusic();
        }

        // Stop ghost movement and animations
        animator.SetBool("isWalking", false);

        // Play cutscene
        if (cutscenePlayer != null)
        {
            cutscenePlayer.gameObject.SetActive(true);
            cutscenePlayer.Play();
        }

        // Show "Tap to Continue" button
        if (tapToContinueButton != null)
        {
            tapToContinueButton.SetActive(true);
        }
    }
}
