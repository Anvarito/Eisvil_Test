namespace Infrastructure.Services.NavMeshBuild
{
    public interface INavMeshRebuildService : IService
    {
        public void RebuildNavMesh();
    }
}