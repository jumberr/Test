using _Project.CodeBase.Logic.Player;
using _Project.CodeBase.UI.Services.Windows;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic
{
    public class Finish : MonoBehaviour
    {
        private IWindowService _windowService;
        private HeroStickmanBehaviour _behaviour;
        private HeroMovement _movement;

        public void Construct(IWindowService windowService, HeroStickmanBehaviour behaviour, HeroMovement movement)
        {
            _windowService = windowService;
            _behaviour = behaviour;
            _movement = movement;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) 
                GameEnd();
        }

        private void GameEnd()
        {
            _movement.enabled = false;
            _windowService.Open(_behaviour.Level >= _behaviour.MaxLevel ? WindowId.Win : WindowId.Lose);
        }
    }
}