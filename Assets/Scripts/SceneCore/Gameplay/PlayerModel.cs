using System;
using Core.GameplaySystems.Unit.Player;
using UnityEngine;

namespace SceneCore.Gameplay
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerModel : MonoBehaviour, IPlayerModel
    {
        [SerializeField] private CharacterController _characterController;

        public Vector3 Position { get; }
        public CharacterController CharacterController => _characterController;
        
        public event Action<Amount> TakedDamage;
        
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