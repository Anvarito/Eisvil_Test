using UnityEngine;

namespace Infrastructure.Services.GameProgress
{
    public class PointCountSaver : IPointCountSaver
    {
        private const string POINTS_COUNT = "points count";
        
        public void Save(int points)
        {
            PlayerPrefs.SetInt(POINTS_COUNT, points);
        }

        public int Load()
        {
            return PlayerPrefs.GetInt(POINTS_COUNT);
        }
    }
}