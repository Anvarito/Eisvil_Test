using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        public UnityAction<Vector3> OnInputDirection { get; set; }
    }
}