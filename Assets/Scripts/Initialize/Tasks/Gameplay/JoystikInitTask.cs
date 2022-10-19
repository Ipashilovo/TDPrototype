using UI.Core;
using Zenject;

namespace Initialize.Tasks.Gameplay
{
    [RequireTask(typeof(GameplayUiBindTask))]
    public class JoystikInitTask : SyncInitTask
    {
        private readonly DiContainer _container;

        public JoystikInitTask(DiContainer container)
        {
            _container = container;
        }

        protected override void OnExecute()
        {
            var uiManager = _container.Resolve<IUiManager>();
        }
    }

    public class GameplayUiBindTask : SyncInitTask
    {
        private readonly DiContainer _container;

        public GameplayUiBindTask(DiContainer container)
        {
            _container = container;
        }
        
        protected override void OnExecute()
        {
            throw new System.NotImplementedException();
        }
    }
}