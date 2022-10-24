using Balance.Data;
using Core.GameplaySystems.Unit.Enemy;
using Core.GameplaySystems.Wave;
using Cysharp.Threading.Tasks;
using Initialize.Core;
using PlayerState;
using SceneCore.Gameplay;
using SceneCore.Gameplay.Enemy;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Initialize.Tasks.Gameplay
{
    [RequireTask(typeof(LevelLoadTask))]
    public class EnemyInitTask : AsyncInitTask
    {
        private DiContainer _container;

        public EnemyInitTask(DiContainer container)
        {
            _container = container;
        }
        
        protected override async UniTask OnExecute()
        {
            var asset = await AdressableUtils.GetAsset<GameObject>("Assets/SceneView/Enemy/EnemyRoot.prefab");
            _container.BindInterfacesAndSelfTo<IAddressableHandler<GameObject>>().FromInstance(asset).AsCached();
            var level = _container.Resolve<LevelEnvironment>();
            var balance = _container.Resolve<BalanceData>();
            var state = _container.Resolve<State>();
            var levelData = balance.LevelData[state.Level.Value];
            foreach (var unit in levelData.UnitsType)
            {
                
            }
            IEnemyPool enemyPool = new EnemyPool()
            IEnemyModule enemyModule = new EnemyModule()
            IWaveModule waveModule = new WaveModule(levelData, )
        }
    }
}