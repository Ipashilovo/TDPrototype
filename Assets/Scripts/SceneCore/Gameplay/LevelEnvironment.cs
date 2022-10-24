using System;
using System.Collections.Generic;
using System.Linq;
using Core.Environment;
using UnityEngine;

namespace SceneCore.Gameplay
{
    public class LevelEnvironment : MonoBehaviour
    {
        [SerializeField] private Transform _playerStartPosition;
        [SerializeField] private PathProvider[] _pathProviders;
        private Dictionary<PathId, PathProvider> _pathProvidersById;
        private Path _path;

        [Serializable]
        private class PathPoint
        {
            public PathId PathId;
            public Transform[] Points;
        }

        private void Awake()
        {
            _pathProvidersById = _pathProviders.ToDictionary(k => k.PathId);
        }

        public Path GetPath(PathId? id)
        {
            if (id.HasValue == false)
            {
                return _pathProviders[0].GetPath();
            }

            return _pathProvidersById[id.Value].GetPath();
        }

        public Vector3 GetPlayerPosition() => _playerStartPosition.position;
    }
}