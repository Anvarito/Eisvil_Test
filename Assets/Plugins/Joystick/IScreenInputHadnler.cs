using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Plugins.Joystick
{
    public interface IScreenInputHadnler
    {
        public UnityAction<PointerEventData> OnMoveDrag { get; set; }
        public UnityAction<PointerEventData> OnEndMoveDrag { get; set; }
        public UnityAction<PointerEventData> OnUpPointer { get; set; }
        public UnityAction<PointerEventData> OnDownPointer { get; set; }
    }
}