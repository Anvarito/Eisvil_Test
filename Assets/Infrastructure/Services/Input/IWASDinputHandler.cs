using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Services.Input
{
    public interface IWASDinputHandler
    {
        public UnityAction<Vector2> OnInputHandle { get; set; }
    }
}