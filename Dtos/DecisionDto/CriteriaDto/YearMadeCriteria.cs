namespace CarSee.Dtos
{
    public class YearMadeCriteria : Criteria<int>
    {
        /*
        <= 2005 1
        >= 2005 <= 2010 2
        >= 2010 <= 2015 3
        >= 2015 <= 2020 4
        >= 2020 5
        */
        public YearMadeCriteria(int value) : base(value)
        {
            this.Value = value;
        }

        public override int CalculateGap(int val)
        {
            this.Gap = val-this.MapCriteria();
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
    }
}