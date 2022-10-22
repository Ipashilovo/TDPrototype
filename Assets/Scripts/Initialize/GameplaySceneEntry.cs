using System.Collections.Generic;
using Core;
using Core.GameplaySystems;
using Initialize.Core;
using Initialize.Tasks.Gameplay;
using UI.Core;
using UnityEngine;

namespace Initialize
{
    public class GameplaySceneEntry : EntryPointBase
    {
        [SerializeField] private UiManager _uiManager;
        private List<IUpdatable> _updatables = new List<IUpdatable>();
        private TimeProvider _timeProvider;
        
#if UNITY_EDITOR
        protected override void Start()
        {
            if (MainEntryPoint.IsLoaded == false)
            {
                enabled = false;
                MainEntryPoint.Load();
                return;
            }
            base.Start();
        }
#endif
        
        protected override void Bind()
        {
            _container.BindInterfacesAndSelfTo<UiManager>().FromInstance(_uiManager).AsSingle();
            _initializer.RegisterTask(new PlayerLoadTask(_container));
            _initializer.RegisterTask(new TimeProviderInitTask(_container));
            _initializer.RegisterTask(new JoystikLoadTask(_container));
            _initializer.RegisterTask(new CameraInitTask(_container));
            _initializer.RegisterTask(new LevelLoadTask(_container));
            _initializer.RegisterTask(new UiBindTask(_container, _uiManager));
            _initializer.OnAllCompleted += OnAllTaskCompleted;
            _initializer.Run();
        }

        private void OnAllTaskCompleted()
        {
            _initializer.OnAllCompleted -= OnAllTaskCompleted;
            _timeProvider = _container.Resolve<TimeProvider>();
            _updatables = _container.ResolveAll<IUpdatable>();
            enabled = true;
        }

        private void Update()
        {
            _timeProvider.Update();
            foreach (var updatable in _updatables)
            {
                updatable.Update();
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}