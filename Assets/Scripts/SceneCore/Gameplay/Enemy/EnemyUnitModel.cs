using System;
using Core.GameplaySystems.Unit.Common;
using UniRx;
using UnityEngine;

namespace SceneCore.Gameplay.Enemy
{
    public class EnemyUnitModel : MonoBehaviour, IUnitModel, IDisposable
    {
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Material _material;
        private DisposableList _disposableList = new DisposableList();
        
        public IAnimatorProvider AnimatorProvider => _enemyAnimator;
        public Vector3 Position => transform.position;
        
        public event Action<Amount> TakedDamage;
        

        public void Init(IReadOnlyReactiveProperty<float> HealthPercent)
        {
            HealthPercent.Subscribe(v =>
            {
                _material.SetFloat("_CurrentValue", v);
            }).AddTo(_disposableList);
        }
        
        public void TakeDamage(Amount damage)
        {
            TakedDamage?.Invoke(damage);
        }

        public void Move(Vector3 position)
        {
            _characterController.Move(position);
        }

        public void Dispose()
        {
            _disposableList.Dispose();
        }
    }
}