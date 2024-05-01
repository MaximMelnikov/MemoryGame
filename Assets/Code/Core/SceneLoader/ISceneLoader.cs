using System.Threading.Tasks;

namespace Core.SceneLoader
{
    public interface ISceneLoader
    {
        public void Load(string name);
    }
}