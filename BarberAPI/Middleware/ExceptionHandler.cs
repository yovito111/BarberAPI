namespace BarberAPI.Middleware
{
    public interface IExceptionHandler
    {
        Task HandleExceptionAsync(Exception ex);
    }

    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleExceptionAsync(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing your request.");
            await Task.CompletedTask;
        }
    }

}
