using Initialize.Core;
using SceneCore.Gameplay;
using UI.Core;
using UI.Screens;
using UnityEngine;
using Zenject;

namespace Initialize.Tasks.Gameplay
{
    [RequireTask(typeof(PlayerLoadTask))]
    public class CameraInitTask : SyncInitTask
    {
        private readonly DiContainer _container;

        public CameraInitTask(DiContainer container)
        {
            _container = container;
        }
        
        protected override void OnExecute()
        {
            var cameraPrefab = Resources.Load<CameraProvider>("Camera");
            var camera = Object.Instantiate(cameraPrefab);
            var player = _container.Resolve<PlayerModel>();
            camera.Init(player.transform);
            _container.BindInterfacesAndSelfTo<CameraProvider>().FromInstance(camera).AsSingle();
        }
    }
}