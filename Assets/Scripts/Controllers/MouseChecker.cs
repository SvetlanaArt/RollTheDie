using RollTheDie.Desktop;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RollTheDie.Controllers
{
    /// <summary>
    /// Check mouse Down and Up events to initiate dragging process
    /// </summary>
    public class MouseChecker : MonoBehaviour, IPointerUpHandler, IPointerDownHandler

    {
        [SerializeField] DragDrop dragDrop;

        public void OnPointerDown(PointerEventData eventData)
        {
            dragDrop.StartDragging();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            dragDrop.StopDragging();
        }

    }
}

