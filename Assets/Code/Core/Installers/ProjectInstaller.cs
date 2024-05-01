using Core.Factory;
using Core.SceneLoader;
using Core.Services.Input;
using Core.StateMachine;
using Core.StateMachine.StateMachines;
using Zenject;

namespace Core.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindProjectStateMachine();
            BindProjectStarterFactory();
            BindSceneLoader();
            BindInput();
        }
        
        private void BindProjectStateMachine()
        {
            Container
                .Bind<IStateMachine>()
                .To<ProjectStateMachine>()
                .AsSingle();
        }
        private void BindProjectStarterFactory()
        {
            Container
                .Bind<BootstrapperFactory>()
                .AsSingle();
        }
        
        private void BindSceneLoader()
        {
            Container
                .Bind<ISceneLoader>()
                .To<SceneLoader.SceneLoader>()
                .AsSingle();
        }

        private void BindInput()
        {
            Container
                .InstantiatePrefabResource("LeanTouch");

            Container
                .Bind<IInputService>()
                .To<InputService>()
                .AsSingle()
                .NonLazy();
        }

    }
}