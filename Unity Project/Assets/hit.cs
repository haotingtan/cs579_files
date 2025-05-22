using UnityEngine;

public class ToolImpact : MonoBehaviour
{
    [Header("Effect Settings")]
    public GameObject crackEffectPrefab; // Assign a particle or visual effect prefab
    public float effectDuration = 1f;    // Duration the effect lasts

    [Header("Sound Settings")]
    public AudioClip crackSound;         // Optional crack sound
    private AudioSource audioSource;

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && crackSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object hit has the tag "CrackableRock"
        if (collision.gameObject.CompareTag("CrackableRock"))
        {
            // Get the first point of contact
            ContactPoint contact = collision.contacts[0];

            // Instantiate crack effect at the contact point
            if (crackEffectPrefab != null)
            {
                GameObject effect = Instantiate(crackEffectPrefab, contact.point, Quaternion.LookRotation(contact.normal));
                Destroy(effect, effectDuration); // Destroy effect after duration
            }

            // Play and limit sound to 1 second
            if (crackSound != null && audioSource != null)
            {
                audioSource.clip = crackSound;
                audioSource.Play();
                Invoke(nameof(StopSound), 1f);
            }

            // Optional: Add damage logic or destroy the rock here
            // Destroy(collision.gameObject);
        }
    }

    private void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
