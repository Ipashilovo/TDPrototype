using System;
using Core.Environment;
using Core.GameplaySystems.Unit.Common;
using UnityEngine;

namespace Core.GameplaySystems.Unit.Enemy
{
    public interface IEnemyModelRoot : IUnitModel, IDisposable
    {
        public IUnitModel Model { get; }
        public Quaternion Rotation { get; }

        public void Init(IHealthProvider healthProvider);
        public event Action<IUnitModel> FindedTarget;
        public event Action<IUnitModel> LostedTarget;
        public void Move(Vector3 position);
        public void LookAt(Quaternion rotation);
        public void MoveUnit(Vector3 target);

        public void Enable();
        public void Disable();
    }
}