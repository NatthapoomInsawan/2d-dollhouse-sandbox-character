using DollhouseCharacter.Interfaces;
using UnityEngine;

namespace DollhouseCharacter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DragablePhysics : MonoBehaviour, IDragable
    {
        public bool IsDragging { get; private set; }

        public Rigidbody2D Rigidbody2D => rigidBody2D;

        [Header("References")]
        [SerializeField] private Rigidbody2D rigidBody2D;

        [Header("Settings")]
        [SerializeField] private float dragPhysicsScale = 1f;

        private Vector2 pointerPos;

        private float originalGravityScale;

        public void StartDrag()
        {
            originalGravityScale = rigidBody2D.gravityScale;

            rigidBody2D.angularVelocity = 0;
            rigidBody2D.linearVelocityY = 0;
            rigidBody2D.gravityScale = 0f;

            rigidBody2D.bodyType = RigidbodyType2D.Kinematic;

            IsDragging = true;
        }

        public void UpdateDrag(Vector2 pointerPos)
        {
            this.pointerPos = pointerPos;
            Vector3 newWorldPos = Camera.main.ScreenToWorldPoint(pointerPos);

            rigidBody2D.MovePosition(new Vector3(newWorldPos.x, newWorldPos.y, transform.position.z));
        }

        public void EndDrag()
        {
            Vector3 moveVector = (Camera.main.ScreenToWorldPoint(pointerPos) - transform.position) * dragPhysicsScale;

            rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
            rigidBody2D.AddForce(moveVector, ForceMode2D.Impulse);
            rigidBody2D.gravityScale = originalGravityScale;

            IsDragging = false;
        }
    }
}
