using System;
using System.Collections.Generic;
using Balance.Data.Enemy;
using Core.Environment;
using Core.GameplaySystems.Unit.Common;
using Entity;
using UniRx;

namespace Core.GameplaySystems.Unit.Enemy
{
    public class EnemyUnit : IEnemy
    {
        private enum EnemyBattleState
        {
            Walk,
            Attack
        }

        private DisposableList _disposableList = new DisposableList();
        private readonly EnemyData _enemyData;
        private readonly IEnemyModelRoot _enemyModelRoot;
        private readonly IEnemyMovable _enemyMovable;
        private readonly IEnemyAttackProvider _attackProvider;
        private EnemyBattleState _currentState;
        private Dictionary<EnemyBattleState, Action> _state;

        public IHealthProvider HealthProvider { get; }
        public IEnemyModelRoot EnemyModelRoot => _enemyModelRoot;
        public UnitId Id => _enemyData.Id;

        public EnemyUnit(EnemyData enemyData, IEnemyModelRoot enemyModelRoot, IEnemyMovable enemyMovable, IEnemyAttackProvider attackProvider, IHealthProvider healthProvider)
        {
            _enemyData = enemyData;
            _enemyModelRoot = enemyModelRoot;
            _enemyMovable = enemyMovable;
            _attackProvider = attackProvider;
            HealthProvider = healthProvider;
            _state = new Dictionary<EnemyBattleState, Action>()
            {
                {EnemyBattleState.Attack, Attack},
                {EnemyBattleState.Walk, Walk}
            };
            _attackProvider.Target.Subscribe(v =>
            {
                _currentState = v == null ? EnemyBattleState.Walk : EnemyBattleState.Attack;
            }).AddTo(_disposableList);
        }

        public void Update()
        {
            _state[_currentState].Invoke();
        }

        private void Walk()
        {
            _enemyMovable.Move();
        }

        private void Attack()
        {
            _attackProvider.Attack();
        }

        public void Dispose()
        {
            _disposableList?.Dispose();
            _enemyModelRoot.Dispose();
        }
    }
}