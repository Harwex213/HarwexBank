using System;

namespace HarwexBank.CreditCalculator
{
    public class CreditCalculator
    {
        public CreditCalculator(decimal creditAmount, decimal creditRate, long creditTerm)
        {
            _creditAmount = creditAmount;
            _creditRate = creditRate / 12;
            _creditTerm = creditTerm;
        }

        private  decimal _creditAmount;
        private  decimal _creditRate;
        private  long _creditTerm;

        public decimal GetAnnuityPlan()
        {
            if (_creditAmount == 0 || _creditRate == 0 || _creditTerm == 0)
            {
                return 0;
            }

            var numerator = _creditRate * (decimal) Math.Pow((double) (1 + _creditRate), _creditTerm);
            var denominator = (decimal) (Math.Pow((double) (1 + _creditRate), _creditTerm) - 1);
            return _creditAmount * numerator / denominator;
        }
    }
}