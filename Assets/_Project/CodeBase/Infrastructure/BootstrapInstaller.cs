using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.States.Interfaces;
using _Project.CodeBase.Infrastructure.StaticData;
using _Project.CodeBase.UI.Services;
using _Project.CodeBase.UI.Services.Windows;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            RegisterCompositionRoot();
            RegisterBootstrapState();
            RegisterInitializeSceneState();
        }

        private void RegisterCompositionRoot()
        {
            Container
                .Bind<IGameStateMachine>()
                .To<GameStateMachine>()
                .AsSingle();

            Container.Bind<IExitableState>()
                .To(x => x.AllNonAbstractClasses())
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<BootstrapInstaller>()
                .FromInstance(this)
                .AsSingle();
        }

        private void RegisterBootstrapState()
        {
            Container
                .Bind<SceneLoader>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<Bootstrapper>()
                .AsSingle();
        }

        private void RegisterInitializeSceneState()
        {
            Container
                .Bind<IStaticDataService>()
                .To<StaticDataService>()
                .AsSingle();

            Container
                .Bind<IWindowService>()
                .To<WindowService>()
                .AsSingle();

            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();
            
            Container
                .Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle();

            Container
                .Bind<IAssetProvider>()
                .To<AssetProvider>()
                .AsSingle();
        }
    }
}
