using UnityEngine;
using DollhouseCharacter.Events;
using DollhouseCharacter.Interfaces;

namespace DollhouseCharacter
{
    public class Spawner : MonoBehaviour, IEventListener<GameObject>
    {
        [SerializeField] private GameObjectGameEvent spawnEvent;
        [SerializeField] private Transform spawnTransform;

        private void Start() => spawnEvent.RegisterListener(this);

        public void OnEventRaised(GameObject value) => SpawnObject(value);

        public void SpawnObject (GameObject gameObject) => Instantiate(gameObject, spawnTransform.position, spawnTransform.rotation);
    }
}
