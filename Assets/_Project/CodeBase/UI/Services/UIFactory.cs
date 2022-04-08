using _Project.CodeBase.Infrastructure;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.StaticData;
using _Project.CodeBase.Logic.Player;
using _Project.CodeBase.UI.Elements;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;

        private Transform _uiRoot;
        private FinishWindow _winWindow;
        private FinishWindow _loseWindow;

        public UIFactory(IAssetProvider assetProvider,
            IStaticDataService staticDataService,
            IGameFactory gameFactory)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
        }

        public async UniTask CreateUIRoot()
        {
            var uiRoot = await _assetProvider.InstantiateAsync(AssetPath.UIRoot);
            _uiRoot = uiRoot.transform;
        }
        
        public async UniTask CreateHud(GameObject player)
        {
            var hud = await _assetProvider.InstantiateAsync(AssetPath.Hud, _uiRoot);
            var behaviour = player.GetComponent<HeroStickmanBehaviour>();
            hud.GetComponent<HeroLevelUI>().Construct(behaviour);
        }

        public void CreateWinWindow(LazyInject<IGameStateMachine> stateMachine, GameObject player)
        {
            var cfg = _staticDataService.ForWindow(WindowId.Win);
            _winWindow = (FinishWindow) Object.Instantiate(cfg.Prefab, _uiRoot);
            _winWindow.Construct(stateMachine, _gameFactory, player.GetComponent<HeroStickmanBehaviour>());
            _winWindow.gameObject.SetActive(false);
        }

        public void CreateLoseWindow(LazyInject<IGameStateMachine> stateMachine, GameObject player)
        {
            var cfg = _staticDataService.ForWindow(WindowId.Lose);
            _loseWindow = (FinishWindow) Object.Instantiate(cfg.Prefab, _uiRoot);
            _loseWindow.Construct(stateMachine, _gameFactory, player.GetComponent<HeroStickmanBehaviour>());
            _loseWindow.gameObject.SetActive(false);
        }

        public void OpenWinWindow() => 
            _winWindow.gameObject.SetActive(true);

        public void OpenLoseWindow() => 
            _loseWindow.gameObject.SetActive(true);
    }
}