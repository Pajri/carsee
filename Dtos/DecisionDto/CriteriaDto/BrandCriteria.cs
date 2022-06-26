using System.Collections.Generic;

namespace CarSee.Dtos
{
    public class BrandCriteria : Criteria<string>
    {
        private Dictionary<string, int> BrandDict = new Dictionary<string, int>
        {
            {"Daihatsu",1},
            {"Wuling",2},
            {"Suzuki",3},
            {"Honda",4},
            {"Toyota",5}
        };

        private string[] _brandWeights;
        public BrandCriteria(string value, string[] brandWeights) : base(value)
        {
            this.Value = value;
            _brandWeights = brandWeights;
        }

        public BrandCriteria(int criteriaWeight) : base("")
        {
            this.Value = "";
            this.CriteriaWeight = criteriaWeight;
        }

        public override int CalculateGap(int val)
        {
            this.Gap = val-this.CriteriaWeight;
            return  this.Gap;
        }
        public override string GetValue()
        {
            return this.Value;
        }

        public override int MapCriteria()
        {
            return BrandDict[this.Value];
            // return this.CriteriaWeight;
        }

        public int GetWeightValue()
        {
            for (int i = 0; i < _brandWeights.Length; i++)
            {
                var current = _brandWeights[i];
                if (current == Value) return i + 1;
            }
            return _brandWeights.Length;
        }
    }
}