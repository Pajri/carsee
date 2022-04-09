namespace CarSee.Dtos
{
    public class CriteriaRequestDto
    {
        public long Mileage { get; set; }
        
        public float Condition { get; set; }
        public int YearMade { get; set; }
        
        public string Brand { get; set; }
        
        public double Price { get; set; }
    }
}