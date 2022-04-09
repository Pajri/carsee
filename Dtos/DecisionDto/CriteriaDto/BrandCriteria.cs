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

        public BrandCriteria(string value) : base(value)
        {
            this.Value = value;
        }
        public override int CalculateGap(int val)
        {
            this.Gap = val-this.MapCriteria();
            return  this.Gap;
        }
        public override string GetValue()
        {
            return this.Value;
        }

        public override int MapCriteria()
        {
            return BrandDict[this.Value];
        }
    }
}