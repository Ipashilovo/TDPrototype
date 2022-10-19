using Core.GameplaySystems.Player;
using SceneCore.Gameplay;
using UnityEngine;
using Zenject;

namespace Initialize.Tasks.Gameplay
{
    [RequireTask(typeof(JoystikInitTask))]
    public class LoadPlayerTask : SyncInitTask
    {
        private DiContainer _container;

        public LoadPlayerTask(DiContainer container)
        {
            _container = container;
        }
        
        protected override void OnExecute()
        {
            var modelPrefab = Resources.Load<PlayerModel>("PlayerModel");
            var model = Object.Instantiate(modelPrefab);
            _container.BindInterfacesTo<PlayerModel>().FromInstance(model).AsSingle();
            PlayerMovement playerMovement = new PlayerMovement();
        }
    }
}