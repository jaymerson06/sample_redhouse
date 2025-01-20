using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float detectionRange = 10f;

    public Animator animator;

    private bool isNearPlayer = false;
    private CheckpointManager checkpointManager;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        checkpointManager = FindObjectOfType<CheckpointManager>();
        if (checkpointManager == null)
        {
            Debug.LogError("CheckpointManager not found in the scene!");
        }
    }

    private void Update()
    {
        if (!isNearPlayer)
        {
            // Chase the player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;

            // Update walking animation
            animator.SetBool("isWalking", true);
            animator.SetFloat("InputX", direction.x);
            animator.SetFloat("InputY", direction.y);
        }
        else
        {
            // Stop movement near the player
            animator.SetBool("isWalking", false);

            // Trigger the cutscene
            if (checkpointManager != null)
            {
                checkpointManager.TriggerCutscene();
            }

            isNearPlayer = false; // Allow the ghost to chase again after the cutscene
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }
}
