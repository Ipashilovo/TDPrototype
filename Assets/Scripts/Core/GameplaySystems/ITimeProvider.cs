using UnityEngine;

namespace Core.GameplaySystems
{
    public interface ITimeProvider
    {
        public Time DeltaTime { get; }
        public Time WorldTime { get; }
    }

    public class TimeProvider : ITimeProvider
    {
        public Time DeltaTime { get; private set; }
        public Time WorldTime { get; private set; }

        
        public TimeProvider()
        {
            WorldTime = new Time(0);
            DeltaTime = new Time(0);
        }

        public void Update()
        {
            DeltaTime = new Time(UnityEngine.Time.deltaTime);
            WorldTime += DeltaTime;
        }
    }
}