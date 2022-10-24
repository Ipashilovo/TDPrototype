using Core.GameplaySystems.Unit.Common;
using UnityEngine;
using UnityEngine.Animations;

namespace Core.GameplaySystems.Unit.Player
{
    public class PlayerAnimation : MonoBehaviour, IAnimatorProvider
    {
        [SerializeField] private Animator _animator;
        private static string AttackName = "Attack";
        public void Attack()
        {
            _animator.Play("Attack");
        }
    }
}