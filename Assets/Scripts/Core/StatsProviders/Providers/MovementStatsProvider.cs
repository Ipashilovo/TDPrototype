using System.Collections.Generic;
using Balance.Data.Player;
using Entity;

namespace Core.StatsProviders.Providers
{
    public class MovementStatsProvider : StatsProvider<UnitId, MovementStats>
    {
        public MovementStatsProvider(Dictionary<UnitId, MovementStats> datas)
        {
            foreach (var data in datas)
            {
                _stats.Add(data.Key, new MovementStatsContainer(data.Value));
            }
        }
    }

    public class MovementStatsContainer : StatsContainer<MovementStats>
    {

        public MovementStatsContainer(MovementStats stats) : base(stats)
        {
            SetBaseStat(stats);
        }

        protected override MovementStats Patch(MovementStats baseStat, MovementStats stat)
        {
            return new(baseStat.Speed + stat.Speed, baseStat.RotateSpeed + stat.RotateSpeed);
        }
    }
}