using UserControlSystem.Views;
using Zenject;

namespace UserControlSystem.Installers
{
    public sealed class ViewsInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BottomCenterView>().FromComponentInHierarchy().AsSingle();
        }
    }
}