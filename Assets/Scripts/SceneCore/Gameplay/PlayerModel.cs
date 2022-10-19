using System;
using UnityEngine;

namespace SceneCore.Gameplay
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;

        public CharacterController CharacterController => _characterController;

        private void Reset()
        {
            _characterController = GetComponent<CharacterController>();
        }
    }
}