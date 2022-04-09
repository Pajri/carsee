namespace CarSee.Dtos
{
    public interface ICriteria<T>
    {
        int MapCriteria();
        int CalculateGap(int val);
        public abstract T GetValue();
    }
}