using System;
using UnityEngine;

namespace DollhouseCharacter.Character
{
    public class CharacterEatState : CharacterState
    {
        public event Action<int> ModifyHunger;

        private Animator characterAnimator;

        private FoodObject foodObject;

        public CharacterEatState(Animator characterAnimator, FoodObject foodObject) 
        {
            this.characterAnimator = characterAnimator;
            this.foodObject = foodObject;
        }
        public override void EnterState()
        {
            characterAnimator.SetTrigger("eat");
            ModifyHunger?.Invoke(foodObject.HungerModifier);
            UnityEngine.Object.Destroy(foodObject.gameObject);
        }
    }
}
