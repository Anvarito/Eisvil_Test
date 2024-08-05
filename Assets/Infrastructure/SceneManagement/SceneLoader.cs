using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneManagement
{
    public class SceneLoader
    {
        
        public SceneLoader()
        {
        }
        
        public async UniTask Load(string sceneName, Action onLoaded = null)
        {
            await SceneManager.LoadSceneAsync(sceneName);
            
            onLoaded?.Invoke();
        }

        // public void MoveGameObjectToScene(GameObject gameObject, SceneInstance targetScene) => 
        //     SceneManager.MoveGameObjectToScene(gameObject, targetScene.Scene);
        //
        // public void MoveGameObjectToScene(GameObject gameObject, Scene targetScene) => 
        //     SceneManager.MoveGameObjectToScene(gameObject, targetScene);
    }

}