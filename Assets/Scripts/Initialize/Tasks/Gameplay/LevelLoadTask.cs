using Balance.Data;
using Cysharp.Threading.Tasks;
using Initialize.Core;
using PlayerState;
using SceneCore.Gameplay;
using UnityEngine;
using Zenject;

namespace Initialize.Tasks.Gameplay
{
    public class LevelLoadTask : AsyncInitTask
    {
        private DiContainer _container;

        public LevelLoadTask(DiContainer container)
        {
            _container = container;
        }
        
        protected override async UniTask OnExecute()
        {
            var state = _container.Resolve<State>();
            var balance = _container.Resolve<BalanceData>();
            var asset = await AdressableUtils.GetAsset<GameObject>($"Assets/SceneView/Level/Level{state.Level.Value}.prefab");
            _container.BindInterfacesAndSelfTo<IAddressableHandler<GameObject>>().FromInstance(asset).AsCached();
            var level = Object.Instantiate(asset.Result);
            var levelEnvironment = level.GetComponent<LevelEnvironment>();
            _container.BindInterfacesAndSelfTo<LevelEnvironment>().FromInstance(levelEnvironment).AsSingle();
        }
    }
}