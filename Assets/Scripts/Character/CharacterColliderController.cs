using System;
using System.Collections.Generic;
using UnityEngine;

namespace DollhouseCharacter.Character
{
    public class CharacterColliderController : MonoBehaviour
    {
        public event Action OnHeadColliderTriggerEnter;

        public event Action<Collider2D> OnMouthColliderTriggerEnter;

        public event Action<Collider2D> OnHandColliderTriggerEnter;
        public event Action<Collider2D> OnHandColliderTriggerStay;
        public event Action OnHandColliderTriggerExit;
        public Transform HolderTransform => holderTransform;

        [Header("Colliders")]
        [SerializeField] private TriggerBox2D headTrigger;
        [SerializeField] private TriggerBox2D mouthTrigger;

        [SerializeField] private List<TriggerBox2D> handTriggers;

        [Header("Transform")]
        [SerializeField] private Transform holderTransform;

        private void Start()
        {
            headTrigger.OnHitBoxColliderEnter += TriggerEnter;
            handTriggers.ForEach(handTrigger => handTrigger.OnHitBoxColliderEnter += TriggerEnter);

            headTrigger.OnHitBoxColliderExit += TriggerExit;
            handTriggers.ForEach(handTrigger => handTrigger.OnHitBoxColliderStay += TriggerStay);

            handTriggers.ForEach(handTrigger => handTrigger.OnHitBoxColliderExit += TriggerExit);
        }

        private void TriggerEnter(TriggerBox2D triggerBox, Collider2D collider2D)
        {
            switch (triggerBox)
            {
                case TriggerBox2D head when head == headTrigger:
                    OnHeadColliderTriggerEnter?.Invoke();
                    break;
                case TriggerBox2D mouth when mouth == mouthTrigger:
                    OnMouthColliderTriggerEnter?.Invoke(collider2D);
                    break;
                case TriggerBox2D hand when handTriggers.Contains(hand):
                    OnHandColliderTriggerEnter?.Invoke(collider2D);
                    break;
            }
        }

        private void TriggerStay(TriggerBox2D triggerBox, Collider2D collider2D)
        {
            switch (triggerBox)
            {
                case TriggerBox2D hand when handTriggers.Contains(hand):
                    OnHandColliderTriggerStay?.Invoke(collider2D);
                    break;
            }
        }

        private void TriggerExit(TriggerBox2D triggerBox, Collider2D collider2D)
        {
            switch (triggerBox)
            {
                case TriggerBox2D hand when handTriggers.Contains(hand):
                    OnHandColliderTriggerExit?.Invoke();
                    break;
            }
        }

    }
}
