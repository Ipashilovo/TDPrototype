using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Environment
{
    public struct PathPoint : IEquatable<PathPoint>
    {
        public readonly Vector3 Position;

        public PathPoint(Vector3 position)
        {
            Position = position;
        }

        public static bool operator ==(PathPoint a, PathPoint b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(PathPoint a, PathPoint b)
        {
            return !(a == b);
        }


        public bool Equals(PathPoint other)
        {
            return Position.Equals(other.Position);
        }

        public override bool Equals(object obj)
        {
            return obj is PathPoint other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }
}