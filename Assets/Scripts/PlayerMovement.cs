using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Joystick _joystick;

    void Update()
    {
        if (_joystick.IsDrag == false)
        {
            return;
        }
        var joystickDirection = _joystick.Direction;
        Vector3 direction = new Vector3(joystickDirection.x, 0, joystickDirection.y);
        _characterController.Move(direction * (Time.deltaTime * _speed));
        var endRotation = Quaternion.LookRotation(direction);
        _characterController.transform.rotation = Quaternion.RotateTowards(_characterController.transform.rotation, endRotation, _rotateSpeed * Time.deltaTime);
    }
}
