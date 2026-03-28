using UnityEngine;
using UnityEngine.UI;

namespace DollhouseCharacter.Character.UI
{
    public class MoodUI : MonoBehaviour
    {
        [SerializeField] private Slider moodBar;

        public void UpdateHungerBar(int value, int maxValue)
        {
            moodBar.value = Mathf.Clamp(((float)value) / maxValue, 0, 1f);
        }
    }
}
