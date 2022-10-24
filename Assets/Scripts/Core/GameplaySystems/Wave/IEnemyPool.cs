using System.Collections.Generic;
using Core.GameplaySystems.Unit.Enemy;
using Entity;

namespace Core.GameplaySystems.Wave
{
    public interface IEnemyPool
    {
        public IEnemyModelRoot GetEnemy(UnitId unitId);
        public void Add(UnitId unitId, IEnemyModelRoot enemyEnemyModelRoot);
    }

    public class EnemyPool : IEnemyPool
    {
        public Dictionary<UnitId, IPool<IEnemyModelRoot>> _units;

        public EnemyPool(Dictionary<UnitId, IPool<IEnemyModelRoot>> unitsPool)
        {
            _units = unitsPool;
        }
        
        public IEnemyModelRoot GetEnemy(UnitId unitId)
        {
            return _units[unitId].Get();
        }

        public void Add(UnitId unitId, IEnemyModelRoot enemyEnemyModelRoot)
        {
            enemyEnemyModelRoot.Disable();
            _units[unitId].Add(enemyEnemyModelRoot);
        }
    }
}