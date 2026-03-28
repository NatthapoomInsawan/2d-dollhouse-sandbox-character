using Cysharp.Threading.Tasks;
using DollhouseCharacter.Interfaces;
using UnityEngine;

namespace DollhouseCharacter.Character
{
    public class CharacterHoldState : CharacterState
    {
        public Transform HoldingObject => holdingObject;

        private Animator characterAnimator;

        private Transform holderTransform;
        private Transform holdTarget;
        private Transform holdingObject;

        private float holdTime = 1.5f;

        public CharacterHoldState(Animator characterAnimator, Transform holderTransform, Transform holdTarget) 
        {
            this.characterAnimator = characterAnimator;
            this.holderTransform = holderTransform;
            this.holdTarget = holdTarget;
        }

        public override async void EnterState()
        {
            FlipToFaceHoldingObject();

            characterAnimator.SetBool("hold", true);

            if (holdTarget.TryGetComponent<IDragable>(out var dragable))
            {
                if (!dragable.IsDragging)
                {
                    if (dragable is DragablePhysics dragablePhysics)
                    {
                        dragablePhysics.Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                        dragablePhysics.Rigidbody2D.simulated = false;
                        holdTarget.position = holderTransform.position;
                        holdTarget.SetParent(holderTransform);
                        holdingObject = holdTarget;
                    }
                }
            }

            if (await UniTask.WaitForSeconds(holdTime, cancellationToken: cts.Token).SuppressCancellationThrow())
                return;

            ExitState();
        }

        public override void ExitState()
        {
            characterAnimator.SetBool("hold", false);
            base.ExitState();
        }

        public override CharacterState GetNextState() => holdingObject == null ? null : new CharacterThrowState(characterAnimator, this);

        private void FlipToFaceHoldingObject()
        {
            if (holdTarget == null) return;

            Vector2 directionToHoldingObject = holdTarget.position - characterAnimator.transform.position;

            Transform rootTransform = characterAnimator.transform.Find("Skeletal");

            if (directionToHoldingObject.x > 0)
                rootTransform.transform.localScale = new Vector2(1, 1);
            else
                rootTransform.transform.localScale = new Vector2(-1, 1);
        }
    }
}
