using DollhouseCharacter.Interfaces;
using System;

namespace DollhouseCharacter.Character
{
    public abstract class CharacterState : IState
    {
        public event Action OnExitState;
        public abstract void EnterState();
        public virtual void ExitState()
        {
            OnExitState?.Invoke();
        }
    }
}
