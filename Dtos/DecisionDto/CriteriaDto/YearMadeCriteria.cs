using CarSee.Utility.Common;

namespace CarSee.Dtos
{
    public class YearMadeCriteria : Criteria<int>
    {
        private string[] _yearMadeWeights;
        public YearMadeCriteria(int value, string[] yearMadeWeights) : base(value)
        {
            this.Value = value;
            _yearMadeWeights = yearMadeWeights;
        }

        public YearMadeCriteria(int criteriaWeight) : base(0)
        {
            this.Value = 0;
            this.CriteriaWeight = criteriaWeight;
        }

        public override int CalculateGap(int val)
        {
            this.Gap = val-this.CriteriaWeight;
            return  this.Gap;
        }

        public override int GetValue()
        {
            return this.Value;
        }

        public override int MapCriteria()
        {
            if(this.Value >= 2020) return 5;
            if(this.Value >= 2015) return 4;
            if(this.Value > 2010) return 3;
            if(this.Value > 2005) return 2;
            return 1;
        }

        public int GetWeightValue()
        {
            for (int i = 0; i < _yearMadeWeights.Length; i++)
            {
                var current = _yearMadeWeights[i];
                if (WeightUtil.IsValueBetweenDouble(current, Value)) return i + 1;
            }
            return _yearMadeWeights.Length;
        }
    }
}