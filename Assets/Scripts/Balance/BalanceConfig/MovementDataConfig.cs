using Balance.Data.Player;
using Entity;
using UnityEngine;

namespace Balance.BalanceConfig
{
    [CreateAssetMenu(fileName = "MovementDataConfig", menuName = "ScriptableObject/Balance/MovementDataConfig", order = 0)]
    public class MovementDataConfig : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotateSpeed;

        public MovementData Get()
        {
            return new MovementData()
            {
                MovementStats = new MovementStats(_speed, _rotateSpeed)
            };
        }
    }
}