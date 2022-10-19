using Entity;
using UniRx;
using UnityEngine;

namespace Core.GameplaySystems.Player
{
    public class PlayerMovement : IUpdatable, IPlayerMovement
    {
        private CharacterController _characterController;
        private ReactiveProperty<bool> _isMovement = new ReactiveProperty<bool>();
        private Joystick _joystick;
        private Transform _target;

        public IReadOnlyReactiveProperty<PlayerMovementStats> Stats { get; }
        public IReadOnlyReactiveProperty<bool> IsMovement => _isMovement;

        public PlayerMovement(Joystick joystick, CharacterController characterController, IReadOnlyReactiveProperty<PlayerMovementStats> playerMovementStats)
        {
            _joystick = joystick;
            _characterController = characterController;
            Stats = playerMovementStats;
        }

        public void Update()
        {
            Vector3 direction;
            if (_joystick.IsDrag == false)
            {
                _isMovement.Value = false;
                if (_target != null)
                {
                    direction = _target.position - _characterController.transform.position;
                    RotateToTarget(direction);
                }
                return;
            }

            _isMovement.Value = true;
            var joystickDirection = _joystick.Direction;
            direction = new Vector3(joystickDirection.x, 0, joystickDirection.y);
            _characterController.Move(direction * (Time.deltaTime * Stats.Value.Speed));
            RotateToTarget(direction);
        }

        private void RotateToTarget(Vector3 direction)
        {
            var endRotation = Quaternion.LookRotation(direction);
            _characterController.transform.rotation = Quaternion.RotateTowards(_characterController.transform.rotation,
                endRotation, Stats.Value.RotateSpeed * Time.deltaTime);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }

    public interface IPlayerMovement
    {
        public void SetTarget(Transform target);
        public IReadOnlyReactiveProperty<bool> IsMovement { get; }
    }
}
