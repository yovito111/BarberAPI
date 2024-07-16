﻿namespace BarberAPI.Models
{
    public class BarberModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        public ICollection<ReservationModel> Reservations { get; set; }
    }
}
