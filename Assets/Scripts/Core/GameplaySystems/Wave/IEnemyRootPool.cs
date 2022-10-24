using System.Collections.Generic;
using Core.GameplaySystems.Unit.Enemy;
using UnityEngine;

namespace Core.GameplaySystems.Wave
{
    public class IEnemyRootPool<TEnemy> : IPool<TEnemy> where TEnemy : MonoBehaviour, IEnemyModelRoot
    {
        private TEnemy _prefab;
        private Queue<TEnemy> _unactive = new Queue<TEnemy>();

        public IEnemyRootPool(TEnemy prefab)
        {
            _prefab = prefab;
        }
        
        public TEnemy Get()
        {
            if (_unactive.TryDequeue(out var enemy) == false)
            {
                return CreateEnemy();
            }

            return enemy;
        }

        private TEnemy CreateEnemy()
        {
            return Object.Instantiate(_prefab);
        }

        public void Add(TEnemy obj)
        {
            _unactive.Enqueue(obj);
        }
    }
}