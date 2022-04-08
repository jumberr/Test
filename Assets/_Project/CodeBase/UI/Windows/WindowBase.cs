using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public Button CloseButton;

        private void Awake() => 
            OnAwake();

        private void OnEnable()
        {
            Initialize();
            SubscribeUpdates();
        }

        protected virtual void OnDisable()
        {
            UnSubscribeUpdates();
            Cleanup();
        }

        private void OnDestroy() => 
            Cleanup();

        protected virtual void OnAwake() => 
            CloseButton.onClick.AddListener(() => gameObject.SetActive(false));

        protected virtual void Initialize() { }
        protected virtual void SubscribeUpdates() { }
        protected virtual void UnSubscribeUpdates() { }
        protected virtual void Cleanup() { }
    }
}