using System;
using Zenject;

namespace Core.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCardTilesDatabase();
            BindHudController();
            BindFieldSettings();
            BindFieldCreator();
            BindFieldSizeController();
            BindGameController();
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

        private void BindHudController()
        {
            Container
                .Bind<HudController>()
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
    }
}