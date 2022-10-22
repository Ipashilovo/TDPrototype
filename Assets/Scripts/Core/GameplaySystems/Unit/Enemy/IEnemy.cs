using System;
using Entity;

namespace Core.GameplaySystems.Unit.Enemy
{
    public interface IEnemy : IUpdatable, IDisposable
    {
        public UnitId Id { get; }
        public event Action<IEnemy> Dead;
    }
}