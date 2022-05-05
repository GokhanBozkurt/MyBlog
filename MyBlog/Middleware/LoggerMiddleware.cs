namespace Middleware
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate next;

        public LoggerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context )
        {
            Console.WriteLine("LoggerMiddleware");
            Console.WriteLine(context.Request.Path);

            await next.Invoke(context);
        }
    }
}
