using Core.StatsProviders;
using Entity;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.GameplaySystems.Unit.Player
{
    public class PlayerMovement : IUpdatable, IPlayerMovement
    {
        private readonly UnitId _unitId;
        private CharacterController _characterController;
        private ReactiveProperty<bool> _isMovement = new();
        private Joystick _joystick;
        private Transform _target;
        private ITimeProvider _timeProvider;

        public IReadOnlyReactiveProperty<MovementStats> Stats { get; private set; }
        public IReadOnlyReactiveProperty<bool> IsMovement => _isMovement;

        public PlayerMovement(UnitId unitId)
        {
            _unitId = unitId;
        }
        
        [Inject]
        private void Construct(Joystick joystick, IPlayerModel playerModel, IStatsProvider<UnitId, MovementStats> playerMovementStats, ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
            _joystick = joystick;
            _characterController = playerModel.CharacterController;
            Stats = playerMovementStats.GetStats(_unitId);
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
            _characterController.Move(direction * (_timeProvider.DeltaTime.Value * Stats.Value.Speed));
            RotateToTarget(direction);
        }

        private void RotateToTarget(Vector3 direction)
        {
            var endRotation = Quaternion.LookRotation(direction);
            _characterController.transform.rotation = Quaternion.RotateTowards(_characterController.transform.rotation,
                endRotation, Stats.Value.RotateSpeed * _timeProvider.DeltaTime.Value);
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
