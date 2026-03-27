using UnityEngine;

namespace DollhouseCharacter
{
    public class FoodObject : MonoBehaviour
    {
        public int HungerModifier => hungerModifier;

        [SerializeField] private int hungerModifier = 10;
    }
}
