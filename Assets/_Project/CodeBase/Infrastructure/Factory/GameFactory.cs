using System;
using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.StaticData;
using _Project.CodeBase.Logic;
using _Project.CodeBase.Logic.Player;
using _Project.CodeBase.Logic.Stickman;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;

        private GameObject _player;
        private List<GameObject> _platforms = new List<GameObject>();
        
        private CollectableStickman _stickman;
        private ColorSwitcher _colorSwitcher;
        private Finish _finish;

        public GameFactory(
            IAssetProvider assetProvider,
            IStaticDataService staticData)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
        }

        public async UniTask GenerateLand()
        {
            var level = _staticData.ForLevel();
            var startPosition = Vector3.zero;
            for (var i = 0; i < level.AmountOfPlatforms; i++)
            {
                var land = await CreateLand(startPosition);
                land.transform.localScale = new Vector3(level.Width, 1, level.Length);
                startPosition.z += level.Length;
                _platforms.Add(land);
            }
        }

        public async UniTask<GameObject> CreatePlayer()
        {
            var startPos = new Vector3(0, 0.5f, 0);
            _player = await _assetProvider.InstantiateAsync(AssetPath.Player, startPos);
            return _player;
        }

        public async UniTask GenerateCollectable(IWindowService windowService, GameObject player)
        {
            var level = _staticData.ForLevel();
            await LoadPrefabs();

            var behaviour = player.GetComponent<HeroStickmanBehaviour>();
            var movement = player.GetComponent<HeroMovement>();
            var collectableRoot = new GameObject("CollectableRoot");
            var startPos = _platforms[1].transform.position;
            startPos.y += 1.5f;
            var step = 5;
            var stickmanTypes = Enum.GetValues(typeof(StickmanType)).Cast<StickmanType>();
            for (var i = 0; i < _platforms.Count; i++)
            {
                if (i != 0 && i != _platforms.Count - 1)
                {
                    for (var j = 0; j < level.Length; j += step)
                    {
                        if (j == 0)
                        {
                            var switchers = CreateCollectable(level, startPos, collectableRoot, _colorSwitcher);
                            InitializeCollectable(behaviour, switchers, stickmanTypes);
                        }
                        else if (j > step)
                        {
                            var stickmans = CreateCollectable(level, startPos, collectableRoot, _stickman);
                            InitializeCollectable(behaviour, stickmans, stickmanTypes);
                        }

                        startPos.z += step;
                    }
                }
                else if (i == _platforms.Count - 1)
                {
                    var finish = Object.Instantiate(_finish, startPos, Quaternion.identity, collectableRoot.transform);
                    finish.Construct(windowService, behaviour, movement);
                }
            }
        }

        public void CleanUp() => 
            _platforms.Clear();

        private List<ICollectable> CreateCollectable<T>(LevelStaticData level, Vector3 startPos,
            GameObject collectableRoot, T prefab) where T : Object
        {
            var random = Random.Range(0, 4);
            var xDistance = level.Width / (random + 1);
            var list = new List<ICollectable>();
            for (var k = 0; k < random; k++)
            {
                var xPos = -level.Width / 2 + (k + 1) * xDistance;
                var pos = new Vector3(xPos, startPos.y, startPos.z);
                list.Add(Object.Instantiate(prefab, pos, Quaternion.identity, collectableRoot.transform) as ICollectable);
            }
            return list;
        }

        private void InitializeCollectable(HeroStickmanBehaviour behaviour, List<ICollectable> switchers, IEnumerable<StickmanType> stickmanTypes)
        {
            var temp = new List<StickmanType>();
            foreach (var switcher in switchers)
            {
                var uniqueList = stickmanTypes.Except(temp);
                var unique = uniqueList.ElementAt(Random.Range(0, uniqueList.Count()));
                switcher.Construct(behaviour, unique);
                temp.Add(unique);
            }
        }

        private async UniTask LoadPrefabs()
        {
            _stickman = (await _assetProvider.Load<GameObject>(AssetPath.Stickman)).GetComponent<CollectableStickman>();
            _colorSwitcher = (await _assetProvider.Load<GameObject>(AssetPath.ColorSwitcher)).GetComponent<ColorSwitcher>();
            _finish = (await _assetProvider.Load<GameObject>(AssetPath.Finish)).GetComponent<Finish>();
        }

        private async UniTask<GameObject> CreateLand(Vector3 position) => 
            await _assetProvider.InstantiateAsync(AssetPath.Land, position);
    }
}