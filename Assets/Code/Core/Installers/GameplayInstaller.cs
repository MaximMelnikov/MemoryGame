using System;
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
        }

        private void BindFieldSettings()
        {
            Container
                .Bind<CardTilesDatabase>()
                .FromResources("CardTilesDatabase");
        }

        private void BindCardTilesDatabase()
        {
            Container
                .Bind<FieldSettings>()
                .FromResources("FieldSettings");
        }

        private void BindFieldCreator()
        {
            Container
                .Bind<IFieldCreator>()
                .To<FieldCreator>()
                .AsSingle()
                .NonLazy();
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