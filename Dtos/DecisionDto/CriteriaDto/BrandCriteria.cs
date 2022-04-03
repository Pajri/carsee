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
            this.Standard = 5;
            this.Gap = this.CalculateGap();
        }

        public override int CalculateGap()
        {
            return this.MapCriteria() - this.Standard;
        }

        public override int CalculateGap(int val)
        {
            return  val-Standard;
        }

        public override int MapCriteria()
        {
            return BrandDict[this.Value];
        }
    }
}