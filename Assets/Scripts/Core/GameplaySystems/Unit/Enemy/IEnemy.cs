using System;
using Core.GameplaySystems.Unit.Common;
using Entity;
using UniRx;

namespace Core.GameplaySystems.Unit.Enemy
{
    public interface IEnemy : IUpdatable, IDisposable
    {
        public IHealthProvider HealthProvider { get; }
        public IEnemyModelRoot EnemyModelRoot { get; }
        public UnitId Id { get; }
    }
}