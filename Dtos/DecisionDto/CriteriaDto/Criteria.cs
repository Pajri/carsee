using System.Collections.Generic;

namespace CarSee.Dtos
{
    public abstract class Criteria<T> : ICriteria<T>
    {
        public T Value { get; set; }
        public int Gap { get; set; }
        public float MappedGap { get; set;}
        protected int CriteriaWeight { get; set; }
        
        
        
        public Dictionary<int, float> GapMapping = new Dictionary<int, float>
        {
            {0,5},{1,4.5f},{-1,4},{2,3.5f},{-2,3},{3,2.5f},{-3,2},{4,1.5f},{-4,1},
        };
        
        public Criteria(T value)
        {
            Value = value;
        }

        public int CalculateGap(int val, int valCriteria)
        {
            this.Gap = val - valCriteria;
            return this.Gap;
        }

        public abstract int MapCriteria();
        public abstract int CalculateGap(int val);
        public abstract T GetValue();

        public void ConvertGap()
        {
            this.MappedGap = this.GapMapping[this.Gap];
        }
    }
}