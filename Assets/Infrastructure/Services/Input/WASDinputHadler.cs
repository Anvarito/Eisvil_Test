using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Infrastructure.Services.Input
{
    public class WASDinputHadler : IWASDinputHandler, ITickable, IService
    {
        public UnityAction<Vector2> OnInputHandle { get; set; }
        
        public void Tick()
        {
            float horizontalInput = UnityEngine.Input.GetAxisRaw("Horizontal");
            float verticalInput = UnityEngine.Input.GetAxisRaw("Vertical");
            OnInputHandle?.Invoke(new Vector2(horizontalInput,verticalInput));
        }

        public void CleanUp()
        {
            
        }
    }
}