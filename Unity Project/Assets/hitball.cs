using UnityEngine;

public class BallCollisionDetector : MonoBehaviour
{
    [Header("User-Assigned")]
    public AudioClip hitSound;
    public GameObject hitEffectPrefab;         // Optional particle burst
    public GameObject crackedOverlayPrefab;    // Cracked mesh prefab
    public float overlayLifetime = 3f;         // How long the cracked overlay stays (seconds)

    private AudioSource audioSource;
    private bool hasHit = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1.0f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;
        hasHit = true;

        ContactPoint contact = collision.contacts[0];
        Vector3 hitPosition = contact.point;
        Quaternion hitRotation = Quaternion.LookRotation(contact.normal);

        // 1. Spawn visual hit effect (if any)
        if (hitEffectPrefab != null)
        {
            GameObject effect = Instantiate(hitEffectPrefab, hitPosition, hitRotation);
            Destroy(effect, overlayLifetime); // Optional: destroy hit effect too
        }

        // 2. Play sound
        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        // 3. Add cracked overlay
        if (crackedOverlayPrefab != null)
        {
            GameObject overlay = Instantiate(crackedOverlayPrefab, transform.position, transform.rotation, transform);
            Destroy(overlay, overlayLifetime); // Remove overlay after specified time
        }
    }
}
