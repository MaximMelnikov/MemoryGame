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
                .AsSingle();
        }
    }
}