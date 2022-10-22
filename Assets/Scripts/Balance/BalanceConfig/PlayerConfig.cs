using Balance.Data;
using Balance.Data.Player;
using Entity;
using UnityEngine;

namespace Balance.BalanceConfig
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObject/Balance/PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private MovementDataConfig _movementDataConfig;
        [SerializeField] private string _id;
        public PlayerData Get()
        {
            return new PlayerData()
            {
                MovementData = _movementDataConfig.Get(),
                Id = new UnitId(_id)
            };
        }
    }
}