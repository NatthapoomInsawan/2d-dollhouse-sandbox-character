using Cysharp.Threading.Tasks;
using DollhouseCharacter.Interfaces;
using System;
using UnityEngine;

namespace DollhouseCharacter.Character
{
    public class CharacterStateController : MonoBehaviour, INeedInitialize
    {
        public event Action<int> OnHungerUpate;
        
        public bool IsInit { get; private set; }
        public int MaxHunger => maxHunger;

        [Header("Reference")]
        [SerializeField] private CharacterColliderController colliderController;
        [SerializeField] private Animator characterAnimator;

        [Header("Settings")]
        [SerializeField] private float holdTime = 1.5f;

        [Header("Data State")]
        [SerializeField] private int hunger;
        [SerializeField] private int maxHunger = 100;

        private CharacterState currentState;

        private void Start()
        {
            colliderController.OnHeadColliderTriggerEnter += () => { SetTriggerState(new CharacterReactToStackingState(characterAnimator)); };
            colliderController.OnMouthColliderTriggerEnter += OnMouthTriggerEnter;
            colliderController.OnHandColliderTriggerEnter += OnHandColliderEnterStay;

            colliderController.OnHandColliderTriggerStay += OnHandColliderEnterStay;

            colliderController.OnHandColliderTriggerExit += OnHandColliderExit;

            SetState(new CharacterIdleState(characterAnimator));

            InitialDataInit();
        }

        private void InitialDataInit()
        {
            IsInit = true;
            OnHungerUpate?.Invoke(hunger);
        }

        private void SetState(CharacterState state)
        {
            currentState?.ExitState();        
            currentState = state;

            if (currentState.GetNextState() != null)
                currentState.OnExitState += () => HandleExitState(currentState);

            currentState.EnterState();
        }

        private void SetTriggerState(CharacterState state)
        {
            state.EnterState();
            if (state.GetNextState() != null)
                state.OnExitState += () => HandleExitState(state);
        }

        private void HandleExitState(CharacterState currentState)
        {
            CharacterState nextState = currentState.GetNextState();
            currentState = null;
            SetState(nextState);
        }

        private async void OnHandColliderEnterStay(Collider2D collider2D)
        {
            if (currentState is CharacterHoldState || currentState is CharacterThrowState)
                return;

            CharacterHoldState holdState = new CharacterHoldState(characterAnimator, colliderController.HolderTransform, collider2D.transform);

            SetState(holdState);

            await UniTask.WaitUntil(() => holdState != null && holdState.HoldingObject != null);

            if (holdState == null)
                return;

            await UniTask.WaitForSeconds(holdTime);

            SetTriggerState(new CharacterThrowState(characterAnimator, holdState));
        }

        private void OnHandColliderExit()
        {
            if (currentState is CharacterHoldState holdState)
            {
                if (holdState.HoldingObject != null)
                    return;
                else
                    holdState.ExitState();
            }

            SetState(new CharacterIdleState(characterAnimator));
        }

        private void OnMouthTriggerEnter(Collider2D collider2D)
        {
            if (currentState is CharacterHoldState)
                return;

            CharacterEatState eatState = new CharacterEatState(characterAnimator, collider2D.transform);

            eatState.ModifyHunger += (value) =>
            {
                hunger += value;
                OnHungerUpate?.Invoke(hunger);
            };
            
            SetTriggerState(eatState);
        }

    }
}
