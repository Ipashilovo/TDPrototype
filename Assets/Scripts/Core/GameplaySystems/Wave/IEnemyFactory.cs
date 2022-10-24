using System.Collections.Generic;
using Core.GameplaySystems.Unit.Enemy;
using Entity;

namespace Core.GameplaySystems.Wave
{
    public interface IEnemyFactory
    {
        IEnemy GetEnemy(IEnemyModelRoot modelRoot, PathId? pathId, UnitId id, Amount count);
    }
}