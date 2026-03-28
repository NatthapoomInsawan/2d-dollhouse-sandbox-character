using DollhouseCharacter.Interfaces;
using System;
using UnityEngine;

namespace DollhouseCharacter.Character
{
    public class CharacterStateController : MonoBehaviour, INeedInitialize
    {
        public event Action<int> OnHungerUpate;
        public event Action<int> OnMoodUpdate;

        public bool IsInit { get; private set; }
        public int MaxHunger => maxHunger;
        public int MaxMood => maxMood;

        [Header("Reference")]
        [SerializeField] private CharacterColliderController colliderController;
        [SerializeField] private Animator characterAnimator;

        [Header("Data State")]
        [SerializeField] private int hunger;
        [SerializeField] private int mood;
        [SerializeField] private int maxHunger = 100;
        [SerializeField] private int maxMood = 100;

        private CharacterState currentState;

        private void Start()
        {
            colliderController.OnHeadColliderTriggerEnter += OnHeadColliderEnter;

            colliderController.OnHandColliderTriggerStay += OnHandColliderStay;
            colliderController.OnMouthColliderTriggerStay += OnMouthColliderStay;

            colliderController.OnHandColliderTriggerExit += OnHandColliderExit;

            SetState(new CharacterIdleState(characterAnimator));

            InitialDataInit();
        }

        private void InitialDataInit()
        {
            IsInit = true;
            OnHungerUpate?.Invoke(hunger);
            OnMoodUpdate?.Invoke(mood);
        }

        private void SetState(CharacterState state)
        {
            if (currentState != null && !currentState.Cts.IsCancellationRequested)
                currentState.ExitState();        

            currentState = state;

            currentState.OnExitState += () => 
            {
                if (currentState?.GetNextState() != null)
                    HandleExitState(currentState);
            };

            currentState?.EnterState();
        }

        private void SetTriggerState(CharacterState state)
        {
            state.EnterState();

            state.OnExitState += () =>
            {
                if (state?.GetNextState() != null)
                    HandleExitState(state);
            };

        }

        private void HandleExitState(CharacterState currentState)
        {
            CharacterState nextState = currentState.GetNextState();
            currentState = null;
            SetState(nextState);
        }

        private void OnHeadColliderEnter(Collider2D collider2D)
        {
            if (collider2D.TryGetComponent<IDragable>(out var dragable) && dragable.IsDragging)
                return;

            characterAnimator.SetTrigger("stacking");
        }

        private void OnHandColliderStay(Collider2D collider2D)
        {
            if (currentState is CharacterHoldState || currentState is CharacterThrowState)
                return;

            CharacterHoldState holdState = new CharacterHoldState(characterAnimator, colliderController.HolderTransform, collider2D.transform);

            SetState(holdState);
        }

        private void OnHandColliderExit()
        {
            if (currentState is CharacterThrowState)
                return;
            if (currentState is CharacterHoldState holdState)
            {
                if (holdState.HoldingObject != null)
                    return;
                else
                    holdState.ExitState();
            }

            SetState(new CharacterIdleState(characterAnimator));
        }

        private void OnMouthColliderStay(Collider2D collider2D)
        {
            if (currentState is CharacterHoldState)
                return;

            if (collider2D.TryGetComponent<IDragable>(out var dragable) && dragable.IsDragging)
                return;

            if (!collider2D.TryGetComponent<FoodObject>(out var foodObject))
                return;

            CharacterEatState eatState = new CharacterEatState(characterAnimator, foodObject);

            eatState.ModifyHunger += (value) =>
            {
                hunger += value;
                OnHungerUpate?.Invoke(hunger);
            };
            
            SetTriggerState(eatState);
        }

        public void ModifyMood(int value)
        {
            mood += value;
            OnMoodUpdate?.Invoke(mood);
        }

    }
}
