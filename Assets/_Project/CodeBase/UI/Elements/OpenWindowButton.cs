using _Project.CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Elements
{
    public class OpenWindowButton : MonoBehaviour
    {
        public Button Button;
        public WindowId WindowId;
        private IWindowService _windowService;

        public void Construct(IWindowService windowService) => 
            _windowService = windowService;

        private void OnEnable() =>
            Button.onClick.AddListener(Open);

        private void OnDisable() => 
            Button.onClick.RemoveListener(Open);

        private void Open() => 
            _windowService.Open(WindowId);
    }
}