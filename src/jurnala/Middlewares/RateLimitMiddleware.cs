namespace Presentation.Web.REST.Middlewares
{
    public class RateLimitMiddleware : IMiddleware
    {
        private static int requestCount = 0;
        private static DateTime lastRequestTime = DateTime.MinValue;
        private readonly int LIMIT = 5;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            DateTime currentTime = DateTime.UtcNow;

            if ((currentTime - lastRequestTime).TotalSeconds > 60)
            {
                // Reset request count if more than a minute has passed since last request
                requestCount = 0;
                lastRequestTime = currentTime;
            }

            if (requestCount >= LIMIT)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too many requests. Please try again later.");
                return;
            }

            requestCount++;
            await next(context);
        }
    }
}
