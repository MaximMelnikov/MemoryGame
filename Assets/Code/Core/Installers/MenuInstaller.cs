using Zenject;

namespace Core.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMenuController();
            BindOptionsController();
        }

        private void BindMenuController()
        {
            Container
                .Bind<MenuController>()
                .AsSingle();
        }

        private void BindOptionsController()
        {
            Container
                .Bind<OptionsController>()
                .AsSingle();
        }
    }
}