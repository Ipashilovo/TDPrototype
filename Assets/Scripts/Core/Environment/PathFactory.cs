namespace Core.Environment
{
    public class PathPointFactory : IPathPointFactory
    {
        private Path _path;

        public PathPointFactory(Path path)
        {
            _path = path;
        }
        
        public bool TryGet(out PathPoint nextPoint, PathPoint? previousPoint = null)
        {
            if (previousPoint.HasValue == false)
            {
                nextPoint = _path[0];
                return true;
            }

            var index = _path.IndexOf(previousPoint.Value);
            if (index < _path.Count - 1)
            {
                nextPoint = _path[index + 1];
                return true;
            }

            nextPoint = default;
            return false;
        }
    }

    public interface IPathPointFactory
    {
        public bool TryGet(out PathPoint nextPoint, PathPoint? previousPoint = null);
    }
}