using UnityEngine;

public class SoundOnMovement : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;

    public AudioClip movingSound;

    private bool isMoving;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = true;
        audioSource.clip = movingSound;

        isMoving = false;
    }

    void Update()
    {
        if (rb.velocity.magnitude > 0 && !isMoving)
        {
            audioSource.Play();
            isMoving = true;
        }
        else if (rb.velocity.magnitude == 0 && isMoving)
        {
            audioSource.Stop();
            isMoving = false;
        }
    }
}
