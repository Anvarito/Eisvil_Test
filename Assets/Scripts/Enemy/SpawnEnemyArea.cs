using UnityEngine;

namespace Enemy
{
    public class SpawnEnemyArea : MonoBehaviour
    {
        [SerializeField] private Collider _collider;

        public Vector3 GetSpawnPoint()
        {
            Bounds bounds = _collider.bounds;

            float spawnX = Random.Range(bounds.min.x, bounds.max.x);
            float spawnZ = Random.Range(bounds.min.z, bounds.max.z);

            Vector3 spawnPoint = new Vector3(spawnX, 0, spawnZ);
            return spawnPoint;
        }
    }
}
