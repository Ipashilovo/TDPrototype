using System.Linq;
using Core.Environment;
using UnityEditor;
using UnityEngine;

namespace SceneCore.Gameplay
{
    public class PathProvider : MonoBehaviour
    {
        [SerializeField] private string _pathId;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform[] _pathPoints;
        private Path _path;
        
        public PathId PathId => new(_pathId);

        private void Awake()
        {
            CreatePath();
        }

        public Path GetPath()
        {
            if (_path == null)
            {
                CreatePath();
            }

            return _path;
        }

        private void CreatePath()
        {
            _lineRenderer.positionCount = _pathPoints.Length;
            for (int i = 0; i < _pathPoints.Length; i++)
            {
                _lineRenderer.SetPosition(i, _pathPoints[i].position);
            }

            _path = new Path(_pathPoints.Select(v => new PathPoint(v.position)).ToList());
        }
    }
}