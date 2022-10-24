using System;
using System.ComponentModel;
using Core.GameplaySystems.Unit.Common;
using Core.GameplaySystems.Unit.Enemy;
using Unity.Collections;
using UnityEngine;

namespace SceneCore.Gameplay.Enemy
{
    public class EnemyModelRoot : MonoBehaviour, IEnemyModelRoot
    {
        [SerializeField] private EnemyUnitModel _enemyUnitModel;
        [SerializeField] private EnemyAttackRadius enemyAttackRadius;
        
        public IAnimatorProvider AnimatorProvider => _enemyUnitModel.AnimatorProvider;
        public Vector3 Position => transform.position;
        public event Action<Amount> TakedDamage;
        public IUnitModel Model => _enemyUnitModel;
        public Quaternion Rotation => _enemyUnitModel.transform.rotation;

        public event Action<IUnitModel> FindedTarget;
        public event Action<IUnitModel> LostedTarget;

        public void Init(IHealthProvider healthProvider)
        {
            _enemyUnitModel.Init(healthProvider.HealthPercent);
            _enemyUnitModel.TakedDamage += TakeDamage;
            enemyAttackRadius.Init((target) => FindedTarget?.Invoke(target), (target) => LostedTarget?.Invoke(target));
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void TakeDamage(Amount damage)
        {
            TakedDamage?.Invoke(damage);
        }

        public void Move(Vector3 position)
        {
            enemyAttackRadius.transform.position = position;
        }

        public void LookAt(Quaternion rotation)
        {
            _enemyUnitModel.transform.rotation = rotation;
        }

        public void MoveUnit(Vector3 target)
        {
            _enemyUnitModel.Move(target);
        }

        public void Dispose()
        {
            _enemyUnitModel.TakedDamage -= TakeDamage;
            _enemyUnitModel.Dispose();
            _enemyUnitModel.transform.localPosition = Vector3.zero;
            enemyAttackRadius.transform.localPosition = Vector3.zero;
        }
    }
}