namespace BarberAPI.Middleware
{

    public static class LoggerResponseMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggerResponseHttp(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggerResponseMiddleware>();
        }
    }

    public class LoggerResponseMiddleware
    {

        private readonly RequestDelegate requestDelegate;
        private readonly ILogger<LoggerResponseMiddleware> logger;

        public LoggerResponseMiddleware(RequestDelegate requestDelegate,
            ILogger<LoggerResponseMiddleware> logger)
        {
            this.requestDelegate = requestDelegate;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var ms = new MemoryStream())
            {
                var originalResponseBody = context.Response.Body;
                context.Response.Body = ms;

                await requestDelegate(context);

                ms.Seek(0, SeekOrigin.Begin);
                string response = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(originalResponseBody);
                context.Response.Body = originalResponseBody;

                logger.LogInformation($"Response: {response}");

            }
        }
    }
}
