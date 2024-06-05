using Core.StateMachine.StateMachines.States;
using Zenject;

namespace Core.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCardTilesDatabase();
            BindFieldSettings();
            BindFieldCreator();
            BindFieldSizeController();
            BindGameController();
            BindHudController();
            BindStates();
        }

        private void BindFieldSettings()
        {
            Container
                .Bind<CardTilesDatabase>()
                .FromResources("CardTilesDatabase")
                .AsSingle();
        }

        private void BindCardTilesDatabase()
        {
            Container
                .Bind<FieldSettings>()
                .FromResources("FieldSettings")
                .AsSingle();
        }

        private void BindFieldCreator()
        {
            Container
                .Bind<IFieldCreator>()
                .To<FieldCreator>()
                .AsSingle();
        }

        private void BindFieldSizeController()
        {
            Container
                .Bind<FieldSizeController>()
                .AsSingle();
        }

        private void BindGameController()
        {
            Container
                .Bind<GameController>()
                .AsSingle();
        }

        private void BindHudController()
        {
            Container.Bind<HudView>()
                .AsSingle();

            Container
                .Bind<HudViewModel>()
                .AsTransient();
        }

        private void BindStates()
        {
            Container
                .Bind<GameplayPreloadState>()
                .AsSingle();

            Container
                .Bind<GameplayState>()
                .AsSingle();
        }
    }
}