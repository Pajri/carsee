using CarSee.Utility.Common;

namespace CarSee.Dtos
{
    public class ConditionCriteria : Criteria<float>
    {
        private string[] _conditionWeights;
        public ConditionCriteria(float value, string[] conditionWeights) : base(value)
        {
            this.Value = value;
            _conditionWeights = conditionWeights;
        }

        public ConditionCriteria(int criteriaWeight) : base(0)
        {
            this.Value = 0;
            this.CriteriaWeight = criteriaWeight;
        }

        public override int CalculateGap(int val)
        {
            this.Gap = val-this.CriteriaWeight;
            return  this.Gap;
        }

        public override float GetValue()
        {
            return this.Value;
        }

        public override int MapCriteria()
        {
            if(this.Value > 0.9) return 5;
            if(this.Value > 0.8) return 4;
            if(this.Value > 0.7) return 3;
            if(this.Value > 0.6) return 2;
            if(this.Value > 0.5) return 1;
            return 0;
        }

        public int GetWeightValue()
        {
            for (int i = 0; i < _conditionWeights.Length; i++)
            {
                var current = _conditionWeights[i];
                if (WeightUtil.IsValueBetweenFloat(current, Value)) return i + 1;
            }
            return _conditionWeights.Length;
        }
    }
}