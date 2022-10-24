using Balance.Data.Player;
using Entity;

namespace Balance.Data.Enemy
{
    public class EnemyData
    {
        public EnemyMovementData EnemyMovementData;
        public AttackData AttackData;
        public UnitId Id;
    }

    public class EnemyMovementData
    {
        public float UnitSpeedScale;
        public MovementData AttackData;
    }
}