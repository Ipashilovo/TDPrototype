using System;
using System.Collections.Generic;
using System.Linq;
using Core.GameplaySystems.Unit.Enemy;
using Entity;
using UniRx;

namespace Core.GameplaySystems.Wave
{
    public interface IEnemyModule : IUpdatable
    {
        public IReadOnlyReactiveProperty<Amount> Count { get; }
        public void AddEnemy(PathId? pathId, Dictionary<UnitId, Amount> units);
    }

    public class EnemyModule : IEnemyModule
    {
        private ReactiveProperty<Amount> _count = new ReactiveProperty<Amount>();
        private IEnemyPool _enemyPool;
        private Dictionary<IEnemy, IDisposable> _disposables = new Dictionary<IEnemy, IDisposable>();
        private List<IEnemy> _enemies = new List<IEnemy>();
        private readonly IEnemyFactory _enemyFactory;

        public IReadOnlyReactiveProperty<Amount> Count => _count;

        public EnemyModule(IEnemyPool enemyPool, IEnemyFactory enemyFactory)
        {
            _enemyPool = enemyPool;
            _enemyFactory = enemyFactory;
        }

        public void Update()
        {
            Span<IEnemy> enemys = new Span<IEnemy>(_enemies.ToArray());
            foreach (var enemy in enemys)
            {
                enemy.Update();
            }
        }

        public void AddEnemy(PathId? pathId, Dictionary<UnitId, Amount> units)
        {
            foreach (var unitData in units)
            {
                var id = unitData.Key;
                var modelRoot = _enemyPool.GetEnemy(id);
                var enemy = _enemyFactory.GetEnemy(modelRoot, pathId, id, unitData.Value);
                _disposables[enemy] = enemy.HealthProvider.Health.Subscribe(v =>
                {
                    if (v <= Amount.Zero)
                    {
                        OnEnemyDead(enemy);
                    }
                });
            }
        }

        private void OnEnemyDead(IEnemy enemy)
        {
            _disposables[enemy].Dispose();
            _disposables.Remove(enemy);
            _enemyPool.Add(enemy.Id, enemy.EnemyModelRoot);
        }
    }
}