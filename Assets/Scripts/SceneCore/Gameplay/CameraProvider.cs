using System;
using Cinemachine;
using UnityEngine;

namespace SceneCore.Gameplay
{
    public class CameraProvider : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        [SerializeField] private Camera _camera;

        public void Init(Transform target)
        {
            _cinemachineVirtualCamera.Follow = target;
            _cinemachineVirtualCamera.LookAt = target;
            _camera.gameObject.SetActive(true);
        }

        private void Reset()
        {
            _camera = GetComponentInChildren<Camera>();
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }
    }
}