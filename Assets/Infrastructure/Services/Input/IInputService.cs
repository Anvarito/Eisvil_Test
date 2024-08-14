using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        public UnityAction<Vector2> OnInputDirection { get; set; }
    }
}