using UnityEngine;

namespace Player
{
    public interface IPlayerPosition
    {
        Transform PlayerTransform { get; }
    }
}