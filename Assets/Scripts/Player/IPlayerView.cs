using Infrastructure.Services.StaticData;
using Infrastructure.Services.StaticData.PlayerConfigs;
using UnityEngine;

namespace Player
{
    public interface IPlayerView
    {
        public void SetAnimationSpeed(float speed);

        public void Move(Vector3 direction);

        public void RotatingTo(Vector3 direction);
        public void LookAtPoint(Vector3 point);
    }
}