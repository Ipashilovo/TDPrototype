namespace UI.Core
{
    public interface IUiManager
    {
        public void ShowStaticScreen<T>(T type) where T : IViewModel;
        public void BindAndShow<T>(T type) where T : IViewModel;
        public void Hide<T>(T type) where T : IViewModel;
        public bool TryGet<T>(out IView screen);
    }
}