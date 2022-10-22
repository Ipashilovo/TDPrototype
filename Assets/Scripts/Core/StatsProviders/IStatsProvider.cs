using System.Collections.Generic;
using UniRx;

namespace Core.StatsProviders
{
    public interface IStatsProvider<TTarget, TStats> where TStats : struct
    {
        public void PatchStats<T>(T source, TTarget target, TStats stats);
        public IReadOnlyReactiveProperty<TStats> GetStats(TTarget target);

        public void SetBaseStats(TTarget target, TStats stats);

        public void Dispatch<T>(T source, TTarget target);

        public void AddTarget<TTargetData>(TTarget target, TTargetData targetData);
        public void RemoveTarget(TTarget target);
    }

    public abstract class StatsProvider<TTarget, TStats> : IStatsProvider<TTarget, TStats> where TStats : struct
    {
        protected Dictionary<TTarget, IStatsContainer<TStats>> _stats =
            new Dictionary<TTarget, IStatsContainer<TStats>>();
        
        public void PatchStats<T>(T source, TTarget target, TStats stats)
        {
            _stats[target].PatchStats(source, stats);
        }
        

        public IReadOnlyReactiveProperty<TStats> GetStats(TTarget target)
        {
            return _stats[target].Get();
        }

        public void SetBaseStats(TTarget target, TStats stats)
        {
            _stats[target].SetBaseStat(stats);
        }

        public void Dispatch<T>(T source, TTarget target)
        {
            _stats[target].Dispatch(source);
        }

        public virtual void AddTarget<TTargetData>(TTarget target, TTargetData targetData)
        {
            
        }

        public virtual void RemoveTarget(TTarget target)
        {
        }
    }

    public interface IStatsContainer<TStats> where TStats : struct
    {
        public void SetBaseStat(TStats stats);

        public void PatchStats<TSource>(TSource source, TStats stats);

        public IReadOnlyReactiveProperty<TStats> Get();
        void Dispatch<TSource>(TSource source);
    }

    public abstract class StatsContainer<TStats> : IStatsContainer<TStats> where TStats : struct
    {
        protected Dictionary<object, TStats> _statsMap = new Dictionary<object, TStats>();
        protected TStats _baseStats;
        protected ReactiveProperty<TStats> _stat = new ReactiveProperty<TStats>();

        public StatsContainer(TStats stats)
        {
            SetBaseStat(stats);
        }
        
        public void SetBaseStat(TStats stats)
        {
            _baseStats = stats;
            PatchAll();
        }

        public void PatchStats<TSource>(TSource source, TStats stats)
        {
            _statsMap[source] = stats;
            PatchAll();
        }

        public IReadOnlyReactiveProperty<TStats> Get()
        {
            return _stat;
        }

        public void Dispatch<TSource>(TSource source)
        {
            _statsMap.Remove(source);
        }

        private void PatchAll()
        {
            var result = _baseStats;
            foreach (var stat in _statsMap)
            {
                result = Patch(result, stat.Value);
            }

            _stat.Value = result;
        }

        protected abstract TStats Patch(TStats baseStat, TStats stat);
    }
}