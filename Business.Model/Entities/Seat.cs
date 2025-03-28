namespace Business.Model.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public int? AreaId { get; set; }
        public Area? Area { get; set; }
        public int? TicketId { get; set; }
        public Ticket? Ticket { get; set; }
    }
}
