using UnityEngine;

namespace Balance.BalanceConfig
{
    [CreateAssetMenu(fileName = "BalanceConfig", menuName = "Balance/BalanceConfig", order = 0)]
    public class BalanceConfig : ScriptableObject
    {
        public BalanceData Get()
        {
            return new BalanceData();
        }
    }
}