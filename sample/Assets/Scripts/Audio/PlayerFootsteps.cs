using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip footstepsClip; // Assign your footsteps SFX here
    public float stepInterval = 0.5f; // Time between steps

    private AudioSource audioSource;
    private Rigidbody2D rb2D;
    private float nextStepTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb2D.linearVelocity.magnitude > 0.1f)
        { // Player is moving
            if (Time.time >= nextStepTime)
            {
                PlayFootstep();
                nextStepTime = Time.time + stepInterval;
            }
        }
    }

    private void PlayFootstep()
    {
        audioSource.PlayOneShot(footstepsClip); // Play the footstep sound
    }
}
