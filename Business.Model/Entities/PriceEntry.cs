namespace Business.Model.Entities
{
    public class PriceEntry
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }

        public int? AreaId { get; set; }
        public Area? Area { get; set; } 
        public int? PriceListId { get; set; }
        public PriceList? PriceList { get; set; }
    }
}
