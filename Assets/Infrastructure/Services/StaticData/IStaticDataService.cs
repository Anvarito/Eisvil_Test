using System.Collections.Generic;
using Infrastructure.Services.StaticData.Level;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadAllStaticData();
        LevelConfig ForLevel(string id);
        PlayerMoveConfig ForPlayer{ get; }
        Dictionary<string, LevelConfig> Levels { get; }
    }
}