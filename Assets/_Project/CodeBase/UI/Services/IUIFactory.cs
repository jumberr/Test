using _Project.CodeBase.Infrastructure;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI.Services
{
    public interface IUIFactory
    {
        UniTask CreateUIRoot();
        UniTask CreateHud(GameObject player);
        void CreateWinWindow(LazyInject<IGameStateMachine> stateMachine, GameObject player);
        void CreateLoseWindow(LazyInject<IGameStateMachine> stateMachine, GameObject player);
        
        void OpenWinWindow();
        void OpenLoseWindow();
    }
}