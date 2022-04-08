using UnityEngine;

namespace _Project.CodeBase.Logic.Player
{
    public class HeroAnimation : MonoBehaviour
    {
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        [SerializeField] private Animator _animator;

        public void Run(float value) => 
            _animator.SetFloat(Horizontal, value);
    }
}