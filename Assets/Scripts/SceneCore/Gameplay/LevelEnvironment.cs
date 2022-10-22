using UnityEngine;

namespace SceneCore.Gameplay
{
    public class LevelEnvironment : MonoBehaviour
    {
        [SerializeField] private Transform _playerStartPosition;

        public Vector3 GetPlayerPosition() => _playerStartPosition.position;
    }
}