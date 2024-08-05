using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        public UnityAction<PointerEventData> OnDragHandle { get; set; }
        public UnityAction<PointerEventData> OnEndDragHandle{ get;  set;}
        public UnityAction<PointerEventData> OnPointerDownHandle{ get;  set;}
        public UnityAction<PointerEventData> OnPointerUpHandle{ get;  set;}
    }
}