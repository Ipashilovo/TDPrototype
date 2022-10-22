using Initialize.Core;
using UI;
using UI.Core;
using UI.Screens;
using Zenject;

namespace Initialize.Tasks.Gameplay
{
    [RequireTask(typeof(UiBindTask))]
    public class JoystikLoadTask : SyncInitTask
    {
        private readonly DiContainer _container;

        public JoystikLoadTask(DiContainer container)
        {
            _container = container;
        }

        protected override void OnExecute()
        {
            var uiManager = _container.Resolve<IUiManager>();
            uiManager.ShowStaticScreen(new JoystickScreenModel());
            uiManager.TryGet<JoystickScreenModel>(out var screen);
            var joystickScreen = screen as JoystickScreen;
            _container.BindInterfacesAndSelfTo<Joystick>().FromInstance(joystickScreen.Joystick).AsSingle();
        }
    }
}