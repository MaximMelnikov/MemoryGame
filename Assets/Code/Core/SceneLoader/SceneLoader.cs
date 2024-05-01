using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Core.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public void Load(string name)
        {
            if (SceneManager.GetActiveScene().name == name)
                return;
      
            SceneManager.LoadScene(name);
        }
    }
}