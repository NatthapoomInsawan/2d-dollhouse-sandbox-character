using DollhouseCharacter.Interfaces;
using System;
using UnityEngine;

namespace DollhouseCharacter.Character
{
    public class CharacterEatState : CharacterState
    {
        public event Action<int> ModifyHunger;

        private Animator characterAnimator;

        private Transform foodTransform;

        public CharacterEatState(Animator characterAnimator, Transform foodTransform) 
        {
            this.characterAnimator = characterAnimator;
            this.foodTransform = foodTransform;
        }
        public override void EnterState()
        {
            if (foodTransform.TryGetComponent<IDragable>(out var dragable))
            {
                if (dragable.IsDragging)
                {
                    ExitState();
                    return;
                }
            }

            if (foodTransform.TryGetComponent<FoodObject>(out var foodObject))
            {
                characterAnimator.SetTrigger("eat");
                ModifyHunger?.Invoke(foodObject.HungerModifier);
                UnityEngine.Object.Destroy(foodObject.gameObject);
            }
        }
    }
}
