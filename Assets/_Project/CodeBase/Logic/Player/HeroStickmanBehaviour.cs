using System;
using _Project.CodeBase.Logic.Stickman;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.CodeBase.Logic.Player
{
    public class HeroStickmanBehaviour : MonoBehaviour
    {
        public event Action<int> OnLevelChanged;
        
        [SerializeField] private Material _green;
        [SerializeField] private Material _red;
        [SerializeField] private Material _yellow;
        
        [SerializeField] private SkinnedMeshRenderer _mesh;
        [SerializeField] private Transform _player;
        [SerializeField] private Vector3 _scaleMultiplier;
        [SerializeField] private int _maxLevel;
        [SerializeField] private int _minLevel;
        
        private StickmanType _currentType;
        private int _level;

        public StickmanType CurrentType => _currentType;
        public int Level => _level;
        public int MaxLevel => _maxLevel;

        private void Start()
        {
            _currentType = (StickmanType) Random.Range(0, 3);
            ChangeColor(_currentType);
        }

        public void OnCollectSame()
        {
            if (_level >= _maxLevel) return;
            _level += 1;
            _player.localScale += _scaleMultiplier;
            OnLevelChanged?.Invoke(_level);
        }

        public void OnCollectDifferent()
        {
            if (_level <= _minLevel) return;
            _level -= 1;
            _player.localScale -= _scaleMultiplier;
            OnLevelChanged?.Invoke(_level);
        }

        public void ChangeType(StickmanType type)
        {
            if (CurrentType == type) return;
            _currentType = type;
            ChangeColor(type);
        }

        private void ChangeColor(StickmanType type)
        {
            var material = type switch
            {
                StickmanType.Green => _green,
                StickmanType.Yellow => _yellow,
                StickmanType.Red => _red
            };
            _mesh.material = material;
        }
    }
}