using System.Collections.Generic;

namespace CarSee.Dtos
{
    public abstract class Criteria<T> : ICriteria
    {
        public T Value { get; set; }
        protected int Standard { get; set; }
        public int Gap { get; set; }
        public int MappedGap { get; set; }
        
        public Dictionary<int, float> GapMapping = new Dictionary<int, float>
        {
            {0,5},{1,4.5f},{-1,4},{2,3.5f},{-2,3},{3,2.5f},{-3,2},{4,1.5f},{-4,1},
        };
        
        public Criteria(T value)
        {
            Value = value;
        }

        public abstract int MapCriteria();
        public abstract int CalculateGap();
    }
}