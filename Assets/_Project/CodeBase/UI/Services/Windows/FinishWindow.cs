using _Project.CodeBase.Infrastructure;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.States;
using _Project.CodeBase.Logic.Player;
using _Project.CodeBase.UI.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.CodeBase.UI.Services.Windows
{
    public class FinishWindow : WindowBase
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Button _restartButton;

        private LazyInject<IGameStateMachine> _stateMachine;
        private HeroStickmanBehaviour _behaviour;
        private IGameFactory _gameFactory;

        public void Construct(LazyInject<IGameStateMachine> stateMachine, IGameFactory gameFactory, HeroStickmanBehaviour behaviour)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _behaviour = behaviour;
        }

        protected override void Initialize()
        {
            if (_behaviour != null)
                _scoreText.text = _behaviour.Level.ToString();
        }

        protected override void OnAwake() { }

        protected override void SubscribeUpdates() =>
            _restartButton.onClick.AddListener(Restart);

        protected override void UnSubscribeUpdates() =>
            _restartButton.onClick.RemoveAllListeners();

        private void Restart()
        {
            _gameFactory.CleanUp();
            _stateMachine.Value.Enter<BootstrapState>();
        }
    }
}