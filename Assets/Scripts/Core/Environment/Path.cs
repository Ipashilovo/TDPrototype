using System.Collections.Generic;

namespace Core.Environment
{
    public class Path : List<PathPoint>
    {
        public Path(IList<PathPoint> pathPoints)
        {
            AddRange(pathPoints);
        }
    }
}