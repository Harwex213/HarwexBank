using System;

namespace HarwexBank.CreditCalculator
{
    public class CreditCalculator
    {
        public CreditCalculator(decimal creditAmount, decimal creditRate, int creditTerm)
        {
            _creditAmount = creditAmount;
            _creditRate = creditRate / 100 / 12;
            _creditTerm = creditTerm;
        }

        private readonly decimal _creditAmount;
        private readonly decimal _creditRate;
        private readonly int _creditTerm;

        public decimal GetAnnuityPlan()
        {
            return _creditAmount * (decimal) Math.Pow((double) (1 + _creditRate), _creditTerm) /
                   (decimal) (Math.Pow((double) (1 + _creditRate), _creditTerm) - 1);
        }
    }
}