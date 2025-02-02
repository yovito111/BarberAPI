﻿namespace BarberAPI.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<ReservationServiceModel> ReservationServices { get; set; }
    }
}
