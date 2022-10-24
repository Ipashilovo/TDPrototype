using System;
using Core.GameplaySystems.Unit.Common;
using UnityEngine;

namespace SceneCore.Gameplay.Enemy
{
    public class EnemyAttackRadius : MonoBehaviour
    {
        [SerializeField] private Action<IUnitModel> _findCallback;
        [SerializeField] private Action<IUnitModel> _lostCallback;
        public void Init(Action<IUnitModel> action, Action<IUnitModel> action1)
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IUnitModel unitModel))
            {
                _findCallback?.Invoke(unitModel);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IUnitModel unitModel))
            {
                _lostCallback?.Invoke(unitModel);
            }
        }
    }
}