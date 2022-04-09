namespace CarSee.Dtos
{
    public class PriceCriteria : Criteria<double>
    {
        public PriceCriteria(double value) : base(value)
        {
            this.Value = value;
        }

        public override int CalculateGap(int val)
        {
            this.Gap = val-this.MapCriteria();
            return  this.Gap;
        }

        public override double GetValue()
        {
            return this.Value;
        }

        public override int MapCriteria()
        {
           if(this.Value >= 200000000 ) return 1;
           if(this.Value > 150000000) return 2;
           if(this.Value > 125000000) return 3;
           if(this.Value > 100000000) return 4;
           return 5;

        }
    }
}