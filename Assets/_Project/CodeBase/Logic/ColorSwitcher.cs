using _Project.CodeBase.Logic.Player;
using _Project.CodeBase.Logic.Stickman;
using UnityEngine;

namespace _Project.CodeBase.Logic
{
    public class ColorSwitcher : MonoBehaviour, ICollectable
    {
        [SerializeField] private MeshRenderer _mesh;
        private StickmanType _type;
        private HeroStickmanBehaviour _stickmanBehaviour;
        
        public void Construct(HeroStickmanBehaviour stickmanBehaviour, StickmanType type)
        {
            _stickmanBehaviour = stickmanBehaviour;
            _type = type;
            ChangeColor();
        }

        public void ChangeColor()
        {
            var color = _type switch
            {
                StickmanType.Red => Color.red,
                StickmanType.Yellow => Color.yellow,
                StickmanType.Green => Color.green
            };
            _mesh.material.color = color;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_stickmanBehaviour.CurrentType != _type)
                    _stickmanBehaviour.ChangeType(_type);
            }
        }
    }
}