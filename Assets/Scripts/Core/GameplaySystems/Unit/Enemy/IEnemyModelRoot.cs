using System;
using Core.Environment;
using Core.GameplaySystems.Unit.Common;

namespace Core.GameplaySystems.Unit.Enemy
{
    public interface IEnemyModelRoot : IUnitModel, IDisposable
    {
        public IUnitModel Model { get; }
        public event Action<IUnitModel> FindedTarget;
        public event Action<IUnitModel> LostedTarget;
        public void Move(PathPoint position);
        public void Attack(Action<IUnitModel> callback);
    }
}