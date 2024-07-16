using System.Threading;

namespace BarberAPI.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BarberId { get; set; }
        public DateTime ReservationTime { get; set; }
        public decimal TotalPrice { get; set; }

        public UserModel User { get; set; }
        public ServiceModel Barber { get; set; }
        public ICollection<ReservationServiceModel> ReservationServices { get; set; }
    }
}
