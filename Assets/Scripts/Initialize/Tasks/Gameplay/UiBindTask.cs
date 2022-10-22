using System.Collections.Generic;
using Initialize.Core;
using UI.Core;
using UnityEngine;
using Zenject;

namespace Initialize.Tasks.Gameplay
{
    public class UiBindTask : SyncInitTask
    {
        private readonly DiContainer _container;
        private readonly UiManager _uiManager;

        public UiBindTask(DiContainer container, UiManager uiManager)
        {
            _container = container;
            _uiManager = uiManager;
        }
        
        protected override void OnExecute()
        {
            var screensPrefabs = Resources.LoadAll<AbstractViewBase>("Screens");
            List<AbstractViewBase> screens = new List<AbstractViewBase>();
            foreach (var screen in screensPrefabs)
            {
                screens.Add(Object.Instantiate(screen, _uiManager.transform as RectTransform));
            }
            
            _uiManager.Init(screens);
        }
    }
}