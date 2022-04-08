using _Project.CodeBase.Logic.Player;
using UnityEngine;

namespace _Project.CodeBase.Logic.Stickman
{
    public class CollectableStickman : MonoBehaviour, ICollectable
    {
        [SerializeField] private MeshRenderer _mesh;
        private StickmanType _type;
        private HeroStickmanBehaviour _stickmanBehaviour;
        
        public void Construct(HeroStickmanBehaviour stickmanBehaviour, StickmanType type)
        {
            _type = type;
            _stickmanBehaviour = stickmanBehaviour;
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
                if (_stickmanBehaviour.CurrentType == _type)
                    _stickmanBehaviour.OnCollectSame();
                else
                    _stickmanBehaviour.OnCollectDifferent();
                Destroy(gameObject);
            }
        }
    }
}