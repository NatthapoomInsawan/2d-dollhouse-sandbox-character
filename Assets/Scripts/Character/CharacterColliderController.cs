using System;
using UnityEngine;

namespace DollhouseCharacter.Character
{
    public class CharacterColliderController : MonoBehaviour
    {
        public event Action OnHeadColliderTriggered;

        [Header("Colliders")]
        [SerializeField] private TriggerBox2D headTrigger;

        private void Start()
        {
            headTrigger.OnHitBoxColliderTriggered += OnTrigger;
        }

        private void OnTrigger(TriggerBox2D triggerBox)
        {
            switch (triggerBox)
            {
                case TriggerBox2D head when head == headTrigger:
                    OnHeadColliderTriggered?.Invoke();
                    break;
            }
        }

    }
}
