using CarSee.Utility.Common;

namespace CarSee.Dtos
{
    public class MileageCriteria : Criteria<long>
    {
        private string[] _mileageWeights;
        public MileageCriteria(long value, string[] mileageWeights) : base(value)
        {
            this.Value = value;
            _mileageWeights = mileageWeights;
        }

        public MileageCriteria(int criteriaWeight) : base(0)
        {
            this.Value = 0;
            this.CriteriaWeight = criteriaWeight;
        }

        public override int CalculateGap(int val)
        {
            this.Gap = val-this.CriteriaWeight;
            return  this.Gap;
        }

        public override long GetValue()
        {
            return this.Value;
        }

        public override int MapCriteria()
        {
            if(this.Value > 100000) return 1;
            if(this.Value > 80000) return 2;
            if(this.Value > 50000) return 3;
            if(this.Value > 20000) return 4;
            return 5;
        }

        public int GetWeightValue()
        {
            for (int i = 0; i < _mileageWeights.Length; i++)
            {
                var current = _mileageWeights[i];
                if (WeightUtil.IsValueBetweenLong(current, Value))  return i + 1;
            }
            return _mileageWeights.Length;
        }
    }
}