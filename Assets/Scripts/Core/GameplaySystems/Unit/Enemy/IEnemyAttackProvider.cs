using System;
using System.Collections.Generic;
using System.Linq;
using Balance.Data;
using Core.GameplaySystems.Unit.Common;
using Entity;
using UniRx;
using UnityEngine;

namespace Core.GameplaySystems.Unit.Enemy
{
    public interface IEnemyAttackProvider : IDisposable
    {
        void Attack();
        IReadOnlyReactiveProperty<IUnitModel> Target { get;  }
    }

    public class EnemyAttackProvider : IEnemyAttackProvider
    {
        private HashSet<IUnitModel> _targets = new HashSet<IUnitModel>();
        private ReactiveProperty<IUnitModel> _target = new ReactiveProperty<IUnitModel>();
        private IEnemyModelRoot _enemyModelRoot;
        private AttackData _attackData;
        private readonly IEnemyMovable _enemyMovable;
        private readonly ITimeProvider _timeProvider;
        private readonly IReadOnlyReactiveProperty<EnemyAttackStats> _stats;
        private Time? _attackDataTime;
        public IReadOnlyReactiveProperty<IUnitModel> Target => _target;
        

        public EnemyAttackProvider(IEnemyModelRoot enemyModelRoot, AttackData attackData, IEnemyMovable enemyMovable, ITimeProvider timeProvider, IReadOnlyReactiveProperty<EnemyAttackStats> stats)
        {
            _attackData = attackData;
            _enemyMovable = enemyMovable;
            _timeProvider = timeProvider;
            _stats = stats;
            _enemyModelRoot = enemyModelRoot;
            enemyModelRoot.FindedTarget += OnFindTarget;
            enemyModelRoot.LostedTarget += OnTargetLost;
        }

        private void OnTargetLost(IUnitModel obj)
        {
            _targets.Remove(obj);
            if (_target.Value == obj)
            {
                _target.Value = null;
                _target.Value = _targets.FirstOrDefault();
            }
        }

        private void OnFindTarget(IUnitModel obj)
        {
            _targets.Add(obj);
            if (_target.Value == null)
            {
                _target.Value = obj;
            }
        }

        public void Attack()
        {
            if (Vector3.Distance(_enemyModelRoot.Position, _target.Value.Position) <= _stats.Value.Distance)
            {
                _enemyMovable.LookAt(_target.Value.Position);
                if (_attackDataTime.HasValue == false || _timeProvider.WorldTime > _attackDataTime.Value)
                {
                    _enemyModelRoot.Model.AnimatorProvider.Play();
                    _attackDataTime = _timeProvider.WorldTime + _stats.Value.FireRate;
                    _target.Value.TakeDamage(_stats.Value.Damage);
                }
            }
            else
            {
                _enemyMovable.Move(_target.Value.Position);
            }
        }

        public void Dispose()
        {
            _enemyModelRoot.FindedTarget -= OnFindTarget;
            _enemyModelRoot.LostedTarget -= OnTargetLost;
            _target?.Dispose();
        }
    }
}