namespace Middleware
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder LoggingExtension(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                Console.WriteLine("LoggingExtension");
                Console.WriteLine(context.Request.Path);
                await next.Invoke(context);
            });
            return app;
        }
    }
}
