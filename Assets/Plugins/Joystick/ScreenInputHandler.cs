using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Plugins.Joystick
{
    public class ScreenInputHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IScreenInputHadnler
    {
        public UnityAction<PointerEventData> OnMoveDrag { get; set; }
        public UnityAction<PointerEventData> OnEndMoveDrag { get; set; }
        public UnityAction<PointerEventData> OnUpPointer { get; set; }
        public UnityAction<PointerEventData> OnDownPointer { get; set; }
        
        public void OnDrag(PointerEventData eventData)
        {
            OnMoveDrag?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnEndMoveDrag?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnUpPointer?.Invoke(eventData);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnDownPointer?.Invoke(eventData);
        }

    }
}
