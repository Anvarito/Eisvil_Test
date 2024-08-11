using Infrastructure.Services.StaticData;
using Unity.AI.Navigation;
using UnityEngine;

namespace Infrastructure.Services.NavMeshBuild
{
    public class NavMeshRebuildService : INavMeshRebuildService
    {
        private readonly ICurrentLevelConfig _currentLevelConfig;

        public NavMeshRebuildService(ICurrentLevelConfig currentLevelConfig)
        {
            _currentLevelConfig = currentLevelConfig;
        }

        public void RebuildNavMesh()
        {
            Object.Instantiate(_currentLevelConfig.CurrentLevelConfig.Obstacles);
            NavMeshSurface navMeshSurface = GameObject.Find("NavmeshGrond").GetComponent<NavMeshSurface>();
            navMeshSurface.BuildNavMesh();
        }
        public void CleanUp()
        {
            
        }
    }
}