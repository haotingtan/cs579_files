using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    [Header("Random Offset Settings")]
    public float rangeX = 2f;
    public float rangeY = 0f;
    public float rangeZ = 2f;

    public float moveDelay = 0.2f;

    private Vector3 originalWorldPosition;
    private bool hasMoved = false;

    void Start()
    {
        // Store world-space position at start
        originalWorldPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile") && !hasMoved)
        {
            hasMoved = true;
            Invoke(nameof(MoveToRandomWorldOffset), moveDelay);
        }
    }

    void MoveToRandomWorldOffset()
    {
        // Generate random world offset
        Vector3 offset = new Vector3(
            Random.Range(-rangeX, rangeX),
            Random.Range(-rangeY, rangeY),
            Random.Range(-rangeZ, rangeZ)
        );

        // Apply to the original world position
        transform.position = originalWorldPosition + offset;

        hasMoved = false;
    }
}
