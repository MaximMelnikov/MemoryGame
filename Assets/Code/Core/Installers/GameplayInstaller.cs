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
            BindLooseController();
            BindWinController();
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
                .AsSingle()
                .IfNotBound();
        }

        private void BindHudController()
        {
            Container.Bind<HudView>()
                .AsSingle();

            Container
                .Bind<HudViewModel>()
                .AsTransient();
        }

        private void BindLooseController()
        {
            Container.Bind<LooseView>()
                .AsSingle();

            Container
                .Bind<LooseViewModel>()
                .AsTransient();
        }

        private void BindWinController()
        {
            Container.Bind<WinView>()
                .AsSingle();

            Container
                .Bind<WinViewModel>()
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