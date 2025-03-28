namespace Business.Model.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public int? ScheduleItemId { get; set; }
        public ScheduleItem? ScheduleItem { get; set; }
        public int? AreaId { get; set; }
        public Area? Area { get; set; }
        public int? SeatId { get; set; }
        public Seat? Seat { get; set; }
    }
}
