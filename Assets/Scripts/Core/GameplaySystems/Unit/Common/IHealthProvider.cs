using UniRx;

namespace Core.GameplaySystems.Unit.Common
{
    public interface IHealthProvider
    {
        public IReadOnlyReactiveProperty<Amount> Health { get; }
    }
}