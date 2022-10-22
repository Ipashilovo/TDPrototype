using Balance.Data;
using Core.GameplaySystems.Unit.Player;
using Entity;
using Initialize.Core;
using SceneCore.Gameplay;
using UnityEngine;
using Zenject;

namespace Initialize.Tasks.Gameplay
{
    [RequireTask(typeof(JoystikLoadTask))]
    [RequireTask(typeof(TimeProviderInitTask))]
    [RequireTask(typeof(LevelLoadTask))]
    public class PlayerLoadTask : SyncInitTask
    {
        private DiContainer _container;

        public PlayerLoadTask(DiContainer container)
        {
            _container = container;
        }
        
        protected override void OnExecute()
        {
            var modelPrefab = Resources.Load<PlayerModel>("Player/Player");
            var level = _container.Resolve<LevelEnvironment>();
            var model = Object.Instantiate(modelPrefab, level.GetPlayerPosition(), Quaternion.identity);
            
            var balance = _container.Resolve<PlayerData>();
            _container.BindInterfacesAndSelfTo<PlayerModel>().FromInstance(model).AsSingle();
            var movement = new PlayerMovement(balance.Id);
            _container.Inject(movement);
            _container.BindInterfacesAndSelfTo<PlayerMovement>().FromInstance(movement).AsSingle();
        }
    }
}