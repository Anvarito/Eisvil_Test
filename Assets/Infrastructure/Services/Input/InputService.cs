using UnityEngine;
using UnityEngine.Events;
using Zenject;


namespace Infrastructure.Services.Input
{
    public class InputService : IInputService, ITickable
    {
        public UnityAction<Vector3> OnInputDirection { get; set; }
        
        public void Tick()
        {
            float horizontalInput = UnityEngine.Input.GetAxisRaw("Horizontal");
            float verticalInput = UnityEngine.Input.GetAxisRaw("Vertical");

            Vector3 inputVector = new Vector3(horizontalInput, 0, verticalInput).normalized;
            OnInputDirection?.Invoke(inputVector);
        }

        public void CleanUp()
        {
            
        }

    }
}