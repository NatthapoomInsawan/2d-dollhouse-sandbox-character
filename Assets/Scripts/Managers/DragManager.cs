using DollhouseCharacter.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DollhouseCharacter.Managers
{
    public class DragManager : MonoBehaviour
    {
        private InputSystem_Actions.DragControlActions dragControlActions;

        private IDragable currentDragable;

        private Vector2 pointerPosition;

        public void Init()
        {
            dragControlActions = new InputSystem_Actions().DragControl;

            dragControlActions.Drag.performed += OnStartDrag;
            dragControlActions.Drag.canceled += OnStopDrag;
            dragControlActions.Position.performed += OnPointerPositionChange;

            dragControlActions.Enable();
        }

        private void OnStartDrag(InputAction.CallbackContext context)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pointerPosition), Vector2.zero);

            IDragable dragable = hit.collider?.GetComponent<IDragable>();
            
            if (dragable != null)
            {
                currentDragable = dragable;
                dragable.StartDrag();
            }
        }

        private void OnPointerPositionChange(InputAction.CallbackContext context)
        {
            pointerPosition = context.ReadValue<Vector2>();

            pointerPosition = new Vector2(
                Mathf.Clamp(pointerPosition.x, 0, Screen.width),
                Mathf.Clamp(pointerPosition.y, 0, Screen.height)
            );

            currentDragable?.UpdateDrag(pointerPosition);
        }

        private void OnStopDrag(InputAction.CallbackContext context)
        {
            currentDragable?.EndDrag();
            currentDragable = null;
        }
    }
}
