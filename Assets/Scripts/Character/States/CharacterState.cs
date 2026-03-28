using DollhouseCharacter.Interfaces;
using System;
using System.Threading;

namespace DollhouseCharacter.Character
{
    public abstract class CharacterState : IState
    {
        public event Action OnExitState;
        
        public CancellationTokenSource Cts => cts;

        protected CancellationTokenSource cts = new();
        
        public abstract void EnterState();
        public virtual void ExitState()
        {
            cts?.Cancel();
            OnExitState?.Invoke();
        }
        public virtual CharacterState GetNextState() { return null; }
    }
}
