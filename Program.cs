using test.ITSTK.producto.api.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices();
var app = builder.Build();

app.RegisterMiddlewares();
app.Run();
