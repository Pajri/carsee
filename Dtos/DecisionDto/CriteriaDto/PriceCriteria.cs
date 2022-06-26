using CarSee.Utility.Common;

namespace CarSee.Dtos
{
    public class PriceCriteria : Criteria<double>
    {
        private string[] _priceWeights;
        public PriceCriteria(double value, string[] priceWeights) : base(value)
        {
            this.Value = value;
            _priceWeights = priceWeights;
        }

        public PriceCriteria(int criteriaWeight) : base(0)
        {
            this.Value = 0;
            this.CriteriaWeight = criteriaWeight;
        }


        public override int CalculateGap(int val)
        {
            this.Gap = val-this.CriteriaWeight;
            return  this.Gap;
        }

        public override double GetValue()
        {
            return this.Value;
        }

        public override int MapCriteria()
        {
            if (this.Value >= 200000000) return 1;
            if (this.Value > 150000000) return 2;
            if (this.Value > 125000000) return 3;
            if (this.Value > 100000000) return 4;
            return 5;
        }

        public int GetWeightValue()
        {
            for(int i = 0; i<_priceWeights.Length;i++)
            {
                var current = _priceWeights[i];
                if (WeightUtil.IsValueBetweenDouble(current, Value)) return i + 1;
            }
            return _priceWeights.Length;
        }
    }
}