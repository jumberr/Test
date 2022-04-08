using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.States.Interfaces;
using _Project.CodeBase.UI.Services;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure.States
{
    public class InitializeGameSceneState : IState
    {     
        private const string MainScene = "MainScene";

        private readonly LazyInject<IGameStateMachine> _stateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IWindowService _windowService;

        public InitializeGameSceneState(
            LazyInject<IGameStateMachine> stateMachine,
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            IWindowService windowService)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _windowService = windowService;
        }

        public async void Enter()
        {
            await InitializeGame();
        }
        
        private async UniTask InitializeGame()
        {
            var player = await GenerateWorld();
            await InitializeUI(player);
        }

        private async UniTask<GameObject> GenerateWorld()
        {
            await _gameFactory.GenerateLand();
            var player = await _gameFactory.CreatePlayer();
            await _gameFactory.GenerateCollectable(_windowService, player);
            return player;
        }

        private async UniTask InitializeUI(GameObject player)
        {
            await InitializeUIRoot();
            await InitializeHud(player);
            InitializeWindows(player);
        }

        private async UniTask InitializeUIRoot() =>
            await _uiFactory.CreateUIRoot();

        private async UniTask InitializeHud(GameObject player) =>
            await _uiFactory.CreateHud(player);
        
        private void InitializeWindows(GameObject player)
        {
            _uiFactory.CreateWinWindow(_stateMachine, player);
            _uiFactory.CreateLoseWindow(_stateMachine, player);
        }

        public void Exit() { }
    }
}