using System;
using Balance.Data;
using PlayerState;
using UnityEngine;
using Zenject;

namespace Initialize.Tasks.Core
{
    [RequireTask(typeof(LoadBalanceTask))]
    public class LoadStateTask : SyncInitTask
    {
        private DiContainer _container;

        public LoadStateTask(DiContainer container)
        {
            _container = container;
        }

        protected override void OnExecute()
        {
            State savedState = null;
            var stateSaver = new StateSaver();
            try
            {
                savedState = stateSaver.Read();
            }
            catch (Exception ex)
            {
                Debug.LogException(new Exception("[SAVE] load state exception", ex));
            }

            if (savedState == null)
            {
                savedState = new DefaultStateCreator().Create(_container.Resolve<BalanceData>());
            }
            stateSaver.SetState(savedState);
            _container.Bind<State>()
                .FromInstance(savedState).AsSingle();
            _container.BindInterfacesAndSelfTo<StateSaver>().FromInstance(stateSaver).AsSingle();
            savedState.BindFields(_container);
            savedState.BindProperties(_container);
        }
    }
}