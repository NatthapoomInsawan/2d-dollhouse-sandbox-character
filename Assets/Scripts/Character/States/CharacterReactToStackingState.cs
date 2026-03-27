using UnityEngine;

namespace DollhouseCharacter.Character
{
    public class CharacterReactToStackingState : CharacterState
    {
        private Animator characterAnimator;

        public CharacterReactToStackingState(Animator characterAnimator)
        {
            this.characterAnimator = characterAnimator;
        }

        public override void EnterState()
        {
            characterAnimator.SetTrigger("stacking");
        }
    }
}
