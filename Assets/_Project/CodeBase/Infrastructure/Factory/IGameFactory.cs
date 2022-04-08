using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        UniTask GenerateLand();
        UniTask<GameObject> CreatePlayer();
        UniTask GenerateCollectable(IWindowService windowService, GameObject player);
        void CleanUp();
    }
}