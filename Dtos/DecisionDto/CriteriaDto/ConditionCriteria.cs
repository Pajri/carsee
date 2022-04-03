namespace CarSee.Dtos
{
    public class ConditionCriteria : Criteria<float>
    {
        /*
            >= 50% 1
            >= 60% 2
            >= 70% 3
            >= 80% 4
            >= 90% 5
        */
        public ConditionCriteria(float value) : base(value)
        {
            this.Value = value;
            this.Standard = 4;
            this.Gap = this.MapCriteria() - this.CalculateGap();
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
            if(this.Value > 0.9) return 5;
            if(this.Value > 0.8) return 4;
            if(this.Value > 0.7) return 3;
            if(this.Value > 0.6) return 2;
            if(this.Value > 0.5) return 1;
            return 0;
        }
    }
}