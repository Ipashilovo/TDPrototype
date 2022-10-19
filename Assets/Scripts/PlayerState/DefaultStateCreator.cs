using Balance;
using Balance.Data;

namespace PlayerState
{
    public class DefaultStateCreator
    {
        public State Create(BalanceData resolve)
        {
            return new State();
        }
    }
}