using System;
using UnityEngine;

namespace DollhouseCharacter
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerBox2D : MonoBehaviour
    {
        public event Action<TriggerBox2D, Collider2D> OnHitBoxColliderEnter;
        public event Action<TriggerBox2D, Collider2D> OnHitBoxColliderStay;
        public event Action<TriggerBox2D, Collider2D> OnHitBoxColliderExit;

        [Header("Collider")]
        [SerializeField] private Collider2D hitBoxCollider;

        private void Start()
        {
            if (hitBoxCollider == null)
            {
                hitBoxCollider = GetComponent<Collider2D>();
                hitBoxCollider.isTrigger = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) => OnHitBoxColliderEnter?.Invoke(this, collision);
        private void OnTriggerStay2D(Collider2D collision) => OnHitBoxColliderStay?.Invoke(this, collision);
        private void OnTriggerExit2D(Collider2D collision) => OnHitBoxColliderExit?.Invoke(this, collision);

    }
}
