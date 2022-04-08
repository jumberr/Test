namespace _Project.CodeBase.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory) => 
            _uiFactory = uiFactory;

        public void Open(WindowId id)
        {
            switch (id)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.Win:
                    _uiFactory.OpenWinWindow();
                    break;
                case WindowId.Lose:
                    _uiFactory.OpenLoseWindow();
                    break;
            }
        }
    }
}