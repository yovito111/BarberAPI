namespace BarberAPI.Dtos
{
    public class ErrorDto
    {
        public string Message { get; set; }
        public string Details { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
