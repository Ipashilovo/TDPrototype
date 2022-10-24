using System;
using Core.GameplaySystems.Unit.Common;
using Core.GameplaySystems.Unit.Player;
using UnityEngine;

namespace SceneCore.Gameplay
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerModel : MonoBehaviour, IPlayerModel
    {
        [SerializeField] private PlayerAnimation _playerAnimator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField, Range(0f, 1f)] private float _health;
        [SerializeField] private Renderer _renderer;
        private Material _material;

        public IAnimatorProvider AnimatorProvider => _playerAnimator;

        public Vector3 Position => transform.position;

        public CharacterController CharacterController => _characterController;

        public event Action<Amount> TakedDamage;

        private void Start()
        {
            _material = _renderer.material;
        }

        private void Update()
        {
            _material.SetFloat("_CurrentValue", _health);
        }

        public void TakeDamage(Amount damage)
        {
            TakedDamage?.Invoke(damage);
        }

        private void Reset()
        {
            _characterController = GetComponent<CharacterController>();
        }
    }
}