using _Project.CodeBase.Infrastructure.States.Interfaces;
using _Project.CodeBase.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "MainScene";
        
        private readonly SceneLoader _sceneLoader;
        private readonly LazyInject<IGameStateMachine> _gameStateMachine;
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(
            SceneLoader sceneLoader,
            IStaticDataService staticDataService,
            LazyInject<IGameStateMachine> gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            await LoadStaticData();
            await _sceneLoader.Load(InitialScene, OnLoaded);
        }

        public void Exit() { }

        private async UniTask LoadStaticData()
        {
            await _staticDataService.LoadLandConfig();
            await _staticDataService.LoadUIWindowConfig();
        }

        private void OnLoaded() => 
            _gameStateMachine.Value.Enter<InitializeGameSceneState>();
    }
}