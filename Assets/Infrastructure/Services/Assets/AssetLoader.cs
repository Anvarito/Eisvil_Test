using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.Assets
{
    public class AssetLoader : IAssetLoader
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        public void CleanUp()
        {
            cts.Cancel();
        }

        // public GameObject Load(string path, Vector3 at)
        // {
        //     GameObject playerPrefab = Resources.Load<GameObject>(path);
        //     return Load(playerPrefab, at);
        // }
        //
        // public GameObject Load(string path)
        // {
        //     GameObject hudPrefab = Resources.Load<GameObject>(path);
        //     return Load(hudPrefab);
        // }
        //
        // public GameObject Load(GameObject gameObject)
        // {
        //     return Object.Load(gameObject);
        // }
        //
        //
        //
        // public TComponent Load<TComponent>(string path) where TComponent : MonoBehaviour
        // {
        //     TComponent hudPrefab = Resources.Load<TComponent>(path);
        //     return Object.Load<TComponent>(hudPrefab);
        // }
        //
        // public TComponent Load<TComponent>(string path, Vector3 at) where TComponent : MonoBehaviour
        // {
        //     TComponent hudPrefab = Resources.Load<TComponent>(path);
        //     return Object.Load<TComponent>(hudPrefab, at, Quaternion.identity);
        // }
        //
        //
        //
        // public ResourceRequest InstantiateAsync(string path)
        // {
        //     ResourceRequest asset = Resources.LoadAsync(path);
        //     return asset;
        // }
        public GameObject Load(string path)
        {
            var go = Resources.Load(path);
            return go as GameObject;
        }
        public GameObject Instantiate(GameObject gameObject)
        {
            return Object.Instantiate(gameObject);
        }
        public T Instantiate<T>(string path) where T : MonoBehaviour
        {
            T hudPrefab = Resources.Load<T>(path);
            return Object.Instantiate(hudPrefab);
        }
        public async UniTask LoadAsset(string path, UnityAction<float> progress = null,
            UnityAction<GameObject> onComplete = null)
        {
            var asset = await Resources.LoadAsync(path).ToUniTask(Progress.Create<float>(x => progress?.Invoke(x)));
            onComplete?.Invoke(asset as GameObject);
        }

        public async UniTask<T> LoadAsset<T>(string path) where T : MonoBehaviour
        {
            var asset= await Resources.LoadAsync<T>(path);
            return asset as T;
        }
        public async UniTask<T> LoadAndInstantiateAsync<T>(string path) where T : MonoBehaviour
        {
            var asset= await Resources.LoadAsync<T>(path);
            return Object.Instantiate(asset as T);
        }

        public T LoadAndInstantiate<T>(string path) where T : MonoBehaviour
        {
            var asset =  Resources.Load<T>(path);
            return Object.Instantiate(asset);
        }
    }
}