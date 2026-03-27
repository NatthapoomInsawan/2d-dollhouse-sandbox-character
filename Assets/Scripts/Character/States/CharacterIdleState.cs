using UnityEngine;

namespace DollhouseCharacter.Character
{
    public class CharacterIdleState : CharacterState
    {
        private Animator characterAnimator;

        public CharacterIdleState(Animator characterAnimator)
        {
            this.characterAnimator = characterAnimator;
        }

        public override void EnterState()
        {
            characterAnimator.Play("idle", 0, 0f);
        }
    }
}
