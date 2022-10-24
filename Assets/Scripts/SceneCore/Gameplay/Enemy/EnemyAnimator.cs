using System;
using Core.GameplaySystems.Unit.Common;
using UnityEngine;

namespace SceneCore.Gameplay.Enemy
{
    [RequireComponent(typeof(AnimationCurve))]
    public class EnemyAnimator : MonoBehaviour, IAnimatorProvider
    {
        [SerializeField] private AnimationCurve _animationCurve;
        [SerializeField] private Vector3 _baseScale;
        private float _animationTime;
        
        public void Attack()
        {
            _animationTime = 0;
        }

        private void Update()
        {
            _animationTime += UnityEngine.Time.deltaTime;
            var scale = _animationCurve.Evaluate(_animationTime);
            transform.localScale = _baseScale * scale;
        }

        private void Reset()
        {
            _baseScale = transform.localScale;
        }

        [ContextMenu("SaveScale")]
        private void SaveScale()
        {
            _baseScale = transform.localScale;
        }
    }
}