using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiveRabbitsDemo
{
    public class AnimatorParamatersChange : MonoBehaviour
    {
        private Animator m_animator;
        public Transform target; // 🟢 Assign your sphere here in the Inspector
        public float moveSpeed = 0.3f;
        public float turnSpeed = 5f;  // Lowered for smooth rotation
        public float raycastHeight = 1.0f;
        public float groundOffset = 0.05f;
        public LayerMask groundLayer;

        void Start()
        {
            m_animator = GetComponent<Animator>();
            m_animator.SetInteger("AnimIndex", 1);  // Run
            m_animator.SetTrigger("Next");
        }

        void Update()
        {
            if (target == null) return;

            // 🧭 Direction to target
            Vector3 direction = (target.position - transform.position);
            direction.y = 0f; // Ignore height differences

            if (direction.magnitude > 0.1f)
            {
                // 🔄 Rotate toward target
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

                // 🏃 Move forward
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }

            // Stick to terrain
            Vector3 rayOrigin = transform.position + Vector3.up * raycastHeight;
            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, raycastHeight * 2f, groundLayer))
            {
                Vector3 groundPos = hit.point + Vector3.up * groundOffset;
                transform.position = new Vector3(transform.position.x, groundPos.y, transform.position.z);

                Quaternion slopeRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation, Time.deltaTime * 5f);
            }
        }
    }
}
