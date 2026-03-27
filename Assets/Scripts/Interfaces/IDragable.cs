
using UnityEngine;

namespace DollhouseCharacter.Interfaces
{
    public interface IDragable
    {
        public bool IsDragging { get; }
        public void StartDrag();
        public void UpdateDrag(Vector2 pointerPos);
        public void EndDrag();
    }
}
