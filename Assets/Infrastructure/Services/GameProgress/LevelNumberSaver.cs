using UnityEngine;

namespace Infrastructure.Services.GameProgress
{
    public class LevelNumberSaver : ILevelNumberSaver
    {
        private const string LEVEL_NUMBER = "level number";
        private const string ORDINAL_LEVEL_NUMBER = "ordinal level number";
        
        public void Save(int level)
        {
            PlayerPrefs.SetInt(LEVEL_NUMBER, level);
        }

        public void SaveOrdinal(int level)
        {
            PlayerPrefs.SetInt(ORDINAL_LEVEL_NUMBER, level);
        }

        public int Load()
        {
           return PlayerPrefs.GetInt(LEVEL_NUMBER);
        }
        
        public int LoadOrdinal()
        {
            return PlayerPrefs.GetInt(ORDINAL_LEVEL_NUMBER);
        }

        public void CleanUp()
        {
            
        }
    }
}