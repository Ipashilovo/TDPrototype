using System;
using UnityEngine;
using Zenject;

namespace Initialize
{
    public class GameplaySceneEntry : MonoBehaviour
    {
        private DiContainer _diContainer;
        private void Start()
        {
            enabled = false;
            var context = SceneContext.Create();
            _diContainer = context.Container;
            
        }
    }
}