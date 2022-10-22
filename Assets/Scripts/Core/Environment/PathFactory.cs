namespace Core.Environment
{
    public class PathPointFactory : IPathPointFactory
    {
        private Path _path;

        public PathPointFactory(Path path)
        {
            _path = path;
        }
        public PathPoint Get(PathPoint? previousPoint = null)
        {
            if (previousPoint.HasValue == false)
            {
                return _path[0];
            }

            var index = _path.IndexOf(previousPoint.Value);
            if (index < _path.Count - 1)
            {
                index++;
            }

            return _path[index];
        }
    }

    public interface IPathPointFactory
    {
        public PathPoint Get(PathPoint? previousPoint = null);
    }
}