using UnityEngine;
using UnityEngine.UI;

namespace DollhouseCharacter.Character.UI
{
    public class HungerUI : MonoBehaviour
    {
        [SerializeField] private Slider hungerBar;

        public void UpdateHungerBar(int value, int maxValue)
        {
            hungerBar.value = ((float) value) / maxValue;
        }

    }
}
