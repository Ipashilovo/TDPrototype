using Core.Environment;
using Entity;
using UniRx;
using UnityEngine;

namespace Core.GameplaySystems.Unit.Enemy
{
    public interface IEnemyMovable
    {
        void Move();
        void Move(Vector3 target);
        void LookAt(Vector3 valuePosition);
    }

    public class EnemyMovable : IEnemyMovable
    {
        public EnemyMovable(IPathPointFactory pathPointFactory, IReadOnlyReactiveProperty<MovementStats> stats)
        {
            
        }
        
        public void Move()
        {
            
        }

        public void Move(Vector3 target)
        {
            
        }

        public void LookAt(Vector3 valuePosition)
        {
            throw new System.NotImplementedException();
        }
    }
}