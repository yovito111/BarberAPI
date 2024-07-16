namespace BarberAPI.Dtos
{
    public class ReservationDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long BarberId { get; set; }
        public DateTime ReservationTime { get; set; }
        public decimal TotalAmount { get; set; }
        public List<ServiceDTO> Services { get; set; }
    }
}
