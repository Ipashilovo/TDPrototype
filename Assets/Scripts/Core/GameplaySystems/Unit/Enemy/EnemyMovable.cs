using System;
using Balance.Data.Enemy;
using Core.Environment;
using Entity;
using UniRx;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Core.GameplaySystems.Unit.Enemy
{
    public interface IEnemyMovable
    {
        void Move();
        void MoveUnit(Vector3 target);
        void LookAt(Vector3 target);
        public IReadOnlyReactiveProperty<PathPoint?> Target { get; }
    }

    public class EnemyMovable : IEnemyMovable
    {
        private readonly IPathPointFactory _pathPointFactory;
        private readonly IReadOnlyReactiveProperty<MovementStats> _stats;
        private readonly IEnemyModelRoot _enemyModelRoot;
        private readonly ITimeProvider _timeProvider;
        private readonly EnemyMovementData _enemyMovementData;
        private ReactiveProperty<PathPoint?> _pathPoint = new ReactiveProperty<PathPoint?>(null);

        public IReadOnlyReactiveProperty<PathPoint?> Target => _pathPoint;

        public EnemyMovable(IPathPointFactory pathPointFactory, IReadOnlyReactiveProperty<MovementStats> stats, IEnemyModelRoot enemyModelRoot, ITimeProvider timeProvider, EnemyMovementData enemyMovementData)
        {
            _pathPointFactory = pathPointFactory;
            _stats = stats;
            _enemyModelRoot = enemyModelRoot;
            _timeProvider = timeProvider;
            _enemyMovementData = enemyMovementData;
            if (_pathPointFactory.TryGet(out var nextPoint))
            {
                _pathPoint.Value = nextPoint;
            }
        }

        public void Move()
        {
            if (_pathPoint.Value == null)
            {
                return;
            }

            var pathPoint = _pathPoint.Value.Value;
            var resultPosition = ResultPosition(pathPoint.Position, _timeProvider.DeltaTime.Value * _stats.Value.Speed);
            _enemyModelRoot.Move(resultPosition);
            MoveUnit(resultPosition);
            if (Vector3.Distance(_enemyModelRoot.Position, pathPoint.Position) < _stats.Value.Speed * _timeProvider.DeltaTime.Value)
            {
                if (_pathPointFactory.TryGet(out var nextPoint, pathPoint))
                {
                    _pathPoint.Value = nextPoint;
                }

                _pathPoint.Value = null;
            }
        }

        private Vector3 ResultPosition(Vector3 target, float speed)
        {
            var resultPosition = Vector3.MoveTowards(_enemyModelRoot.Position, target,
                speed);
            return resultPosition;
        }

        public void MoveUnit(Vector3 target)
        {
            _enemyModelRoot.MoveUnit(ResultPosition(target, _stats.Value.Speed * _timeProvider.DeltaTime.Value * _enemyMovementData.UnitSpeedScale));
            LookAt(target);
        }
        
        public void LookAt(Vector3 target)
        {
            var endRotation = Quaternion.LookRotation(target);
            Quaternion result = Quaternion.RotateTowards(_enemyModelRoot.Rotation,
                endRotation, _stats.Value.RotateSpeed * _timeProvider.DeltaTime.Value);
            _enemyModelRoot.LookAt(result);
        }
    }
}