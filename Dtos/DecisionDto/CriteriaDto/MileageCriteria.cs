namespace CarSee.Dtos
{
    public class MileageCriteria : Criteria<long>
    {
        /*
        >= 100.000 km 1
        <= 100.000 km 2
        <= 80.000 km 3
        <= 50.000 km 4
        <= 20.000 km 5
        */
        public MileageCriteria(long value) : base(value)
        {
            this.Value = value;
            this.Standard = 3;
            this.Gap = this.CalculateGap();
        }

        public override int CalculateGap()
        {
            return this.MapCriteria() - this.Standard;
        }

        public override int MapCriteria()
        {
            if(this.Value > 100000) return 1;
            if(this.Value > 80000) return 2;
            if(this.Value > 50000) return 3;
            if(this.Value > 20000) return 4;
            return 5;

        }
    }
}