using _Project.CodeBase.StaticData;
using _Project.CodeBase.StaticData.UI;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        UniTask LoadLandConfig();
        UniTask LoadUIWindowConfig();
        
        LevelStaticData ForLevel();
        WindowConfig ForWindow(WindowId windowId);
    }
}