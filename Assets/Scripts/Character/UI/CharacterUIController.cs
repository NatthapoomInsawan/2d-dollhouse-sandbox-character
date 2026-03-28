using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DollhouseCharacter.Character.UI
{
    public class CharacterUIController : MonoBehaviour
    {
        [Header("State Reference")]
        [SerializeField] private CharacterStateController characterStateController;

        [Header("UI References")]
        [SerializeField] private HungerUI hungerUI;
        [SerializeField] private MoodUI moodUI;

        private async void Start()
        {
            await UniTask.WaitUntil(()=> characterStateController.IsInit);

            BindStateEvent();
        }

        private void BindStateEvent()
        {
            characterStateController.OnHungerUpate += (value) => hungerUI.UpdateHungerBar(value, characterStateController.MaxHunger);
            characterStateController.OnMoodUpdate += (value) => moodUI.UpdateHungerBar(value, characterStateController.MaxMood);
        }

    }
}
