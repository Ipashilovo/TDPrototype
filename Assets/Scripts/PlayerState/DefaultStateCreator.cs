using Balance;

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