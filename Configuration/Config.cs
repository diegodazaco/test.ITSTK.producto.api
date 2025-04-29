using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Runtime.CompilerServices;
using test.ITSTK.producto.api.AppServices;
using test.ITSTK.producto.api.Data;

namespace test.ITSTK.producto.api.Configuration
{
    public static class Config
    {
        private const string MessageInitialLog = "Start App";
        private const string DefaultConnection = "Server=DIEGO_DAZA\\LOCALDATABASE;Database=Test;User Id=test;Password=test321*;TrustServerCertificate=false;MultipleActiveResultSets=true;TrustServerCertificate=true;";


        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            //Logger Configuration Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Information()
                .MinimumLevel.Error()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(
                    connectionString: DefaultConnection,
                    sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                    {
                        TableName = "Logger_test_ITSTK",
                        SchemaName = "dbo",
                        AutoCreateSqlTable = true,
                        BatchPostingLimit = 1,
                    })
                .CreateLogger();

            Log.Information(MessageInitialLog);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(DefaultConnection));
            builder.Services.AddScoped<IProductoAppServices, ProductoAppServices>();
        }

        public static void RegisterMiddlewares(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
