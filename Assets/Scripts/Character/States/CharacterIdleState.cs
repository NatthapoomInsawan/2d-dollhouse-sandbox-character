using UnityEngine;

namespace DollhouseCharacter.Character
{
    public class CharacterIdleState : CharacterState
    {
        public override void EnterState()
        {
            Debug.Log("Entering Idle State");
        }
    }
}
