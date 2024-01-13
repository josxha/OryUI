using KratosSelfServiceBlazor;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables().Build(); // use environment variables

var startup = new Startup(builder.Configuration, builder.Environment);
startup.ConfigureServices(builder.Services);
var app = builder.Build();
startup.Configure(app);

app.Run();