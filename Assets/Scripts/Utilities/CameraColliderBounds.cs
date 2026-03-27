using UnityEngine;

namespace DollhouseCharacter.Utilities
{
    public class CameraColliderBounds : MonoBehaviour
    {
        [SerializeField] private Camera _camera; 

        void Start()
        {
            GenerateCollidersAcrossScreen();
        }

        void GenerateCollidersAcrossScreen()
        {
            Vector2 bottomLeft = _camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
            Vector2 topLeft = _camera.ViewportToWorldPoint(new Vector3(0, 1, 0));
            Vector2 bottomRight = _camera.ViewportToWorldPoint(new Vector3(1, 0, 0));
            Vector2 topRight = _camera.ViewportToWorldPoint(new Vector3(1, 1, 0));

            EdgeCollider2D edge = GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : GetComponent<EdgeCollider2D>();
            var edgePoints = new[] { bottomLeft, topLeft, topRight, bottomRight, bottomLeft };
            edge.points = edgePoints;
        }

    }
}
