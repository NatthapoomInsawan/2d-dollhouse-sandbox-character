using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DollhouseCharacter.Character
{
    public class CharacterThrowState : CharacterState
    {
        private Animator characterAnimator;
        private CharacterHoldState characterHoldState;

        private float throwMultiplier = 5f;
        private float throwDelay = 0.25f;

        public CharacterThrowState(Animator characterAnimator, CharacterHoldState characterHoldState)
        {
            this.characterAnimator = characterAnimator;
            this.characterHoldState = characterHoldState;
        }
        public override async void EnterState()
        {
            characterHoldState.ExitState();
            characterHoldState.HoldingObject.SetParent(null);
            if (characterHoldState.HoldingObject.TryGetComponent<Rigidbody2D>(out var rigidbody2D))
            {
                characterAnimator.SetTrigger("throw");

                await UniTask.WaitForSeconds(characterAnimator.GetCurrentAnimatorClipInfo(0).Length/3);

                rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                rigidbody2D.simulated = true;
                rigidbody2D.AddForce(new Vector2(characterAnimator.transform.localScale.x, 1) * throwMultiplier, ForceMode2D.Impulse);

                await UniTask.WaitForSeconds(throwDelay);

                ExitState();
            }
        }

        public override CharacterState GetNextState() => new CharacterIdleState(characterAnimator);
    }
}
