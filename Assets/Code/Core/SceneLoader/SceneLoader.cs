using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask Load(string name)
        {
            if (SceneManager.GetActiveScene().name == name)
                return;

            await SceneManager.LoadSceneAsync(name);
        }
    }
}