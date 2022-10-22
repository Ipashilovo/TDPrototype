using Balance.Data;
using UnityEngine;

namespace Balance.BalanceConfig
{
    [CreateAssetMenu(fileName = "BalanceConfig", menuName = "Balance/BalanceConfig", order = 0)]
    public class BalanceConfig : ScriptableObject
    {
        [SerializeField] private PlayerConfig _playerConfig;
        public BalanceData Get()
        {
            return new BalanceData()
            {
                PlayerData = _playerConfig.Get()
            };
        }
    }
}