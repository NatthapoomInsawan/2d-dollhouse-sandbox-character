using System;
using UnityEngine;

namespace DollhouseCharacter.Character
{
    public class CharacterStateController : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private CharacterColliderController colliderController;

        private CharacterState currentState;

        private void Start()
        {
            colliderController.OnHeadColliderTriggered += () => { SetState(new CharacterReactToStackingState()); };

            SetState(new CharacterIdleState());
        }

        private void SetState(CharacterState state)
        {
            currentState?.ExitState();
            currentState = state;
            currentState.EnterState();
        }
    }
}
