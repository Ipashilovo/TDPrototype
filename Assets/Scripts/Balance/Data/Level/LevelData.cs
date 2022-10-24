using System.Collections;
using System.Collections.Generic;
using Entity;

namespace Balance.Data.Level
{
    public class LevelData
    {
        public Entity.Level Level;
        public Time TimeToStartWave;
        public WaveData[] WaveDatas;
        public UnitId[] UnitsType;
    }

    public class WaveData
    {
        public bool HaveBoss;
        public WaveStep[] Steps;
        public Time TimeToNextWave;
    }

    public class WaveStep
    {
        public Time StartDelay;
        public WavePathData[] WavePathDatas;
    }

    public class WavePathData
    {
        public WaveUnitData[] UnitCounts;
        public PathId? PathId;
    }

    public class WaveUnitData
    {
        public Time Delay;
        public Dictionary<UnitId, Amount> SpawnCount;
    }
}