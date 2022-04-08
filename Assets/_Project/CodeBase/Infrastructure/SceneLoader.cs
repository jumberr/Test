using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Project.CodeBase.Infrastructure
{
    public class SceneLoader
    {
        public async UniTask Load(string name, Action onLoaded = null) => 
            await LoadScene(name, onLoaded);

        public async UniTask UnloadSceneAsync(string name) => 
            await SceneManager.UnloadSceneAsync(name);
        
        public async UniTask UnloadCurrentSceneAsync() => 
            await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);

        public IEnumerator ReloadScene(Action onLoaded = null)
        {
            var waitNextScene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            
            while (!waitNextScene.isDone)
                yield return null;
            
            onLoaded?.Invoke();
        }

        private async UniTask LoadScene(string nextScene, Action onLoaded = null)
        {
            await SceneManager.LoadSceneAsync(nextScene);
            onLoaded?.Invoke();
        }
    }
}