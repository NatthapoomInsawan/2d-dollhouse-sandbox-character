using DollhouseCharacter.Interfaces;
using UnityEngine;

namespace DollhouseCharacter
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(TargetJoint2D))]
    public class DragablePhysics : MonoBehaviour, IDragable
    {
        public bool IsDragging { get; private set; }
        public Rigidbody2D Rigidbody2D => rigidBody2D;

        [Header("References")]
        [SerializeField] private Rigidbody2D rigidBody2D;
        [SerializeField] private TargetJoint2D targetJoint;

        private float originalGravityScale;

        private void Awake()
        {
            if (targetJoint == null) targetJoint = GetComponent<TargetJoint2D>();
            targetJoint.enabled = false;
        }

        public void StartDrag()
        {
            targetJoint.enabled = true;
            targetJoint.target = transform.position;

            originalGravityScale = rigidBody2D.gravityScale;

            rigidBody2D.angularVelocity = 0;
            rigidBody2D.linearVelocity = Vector2.zero;
            rigidBody2D.gravityScale = 0f;

            rigidBody2D.bodyType = RigidbodyType2D.Dynamic;

            IsDragging = true;
        }

        public void UpdateDrag(Vector2 pointerPos)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(pointerPos);
            targetJoint.target = new Vector2(worldPos.x, worldPos.y);
        }

        public void EndDrag()
        {
            IsDragging = false;

            targetJoint.enabled = false;
            rigidBody2D.gravityScale = originalGravityScale;

            rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}