using Plugins.Joystick;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;


namespace Infrastructure.Services.Input
{
    public class InputService : IInputService, ITickable
    {
        private readonly IScreenInputHadnler _screenInputHadnler;
        private readonly IWASDinputHandler _wasdInputHandler;
        private Vector2 _mouseInputVector;
        private Vector2 _startMousePosition;
        private Vector2 _wasdVector;
        public UnityAction<Vector2> OnInputDirection { get; set; }

        public InputService(IScreenInputHadnler screenInputHadnler, IWASDinputHandler wasdInputHandler)
        {
            _screenInputHadnler = screenInputHadnler;
            _wasdInputHandler = wasdInputHandler;

            _wasdInputHandler.OnInputHandle += WasdInputHandle;
            
            _screenInputHadnler.OnMoveDrag += OnDrag;
            _screenInputHadnler.OnEndMoveDrag += OnEndDrag;
            _screenInputHadnler.OnUpPointer += OnPointerUp;
            _screenInputHadnler.OnDownPointer += OnPointerDown;
        }

        private void WasdInputHandle(Vector2 direction)
        {
            _wasdVector = direction;
        }

        public void Tick()
        {
            Vector2 movementDirection = new Vector2(_mouseInputVector.x - _startMousePosition.x, _mouseInputVector.y - _startMousePosition.y);
            movementDirection += _wasdVector;
            movementDirection = Vector2.ClampMagnitude(movementDirection, 1);
            OnInputDirection?.Invoke(movementDirection);
        }
        
        private void OnDrag(PointerEventData eventData)
        {
            _mouseInputVector = eventData.position;
        }
        
        private void OnEndDrag(PointerEventData eventData)
        {
            _mouseInputVector = Vector2.zero;
        }

        private void OnPointerUp(PointerEventData eventData)
        {
            _mouseInputVector = Vector2.zero;
            _startMousePosition = Vector2.zero;
        }

        private void OnPointerDown(PointerEventData eventData)
        {
            _startMousePosition = eventData.position;
        }

        public void CleanUp()
        {
            _wasdInputHandler.OnInputHandle -= WasdInputHandle;
            _screenInputHadnler.OnMoveDrag -= OnDrag;
            _screenInputHadnler.OnEndMoveDrag -= OnEndDrag;
            _screenInputHadnler.OnUpPointer -= OnPointerUp;
            _screenInputHadnler.OnDownPointer -= OnPointerDown;
        }

    }
}