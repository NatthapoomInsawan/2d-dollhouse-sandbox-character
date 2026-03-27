using UnityEngine;

namespace DollhouseCharacter.Managers
{
    public class GameplayManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] private DragManager dragManager;

        private void Start() => InitializeManager();

        private void InitializeManager()
        {
            dragManager.Init();
        }
    }
}
