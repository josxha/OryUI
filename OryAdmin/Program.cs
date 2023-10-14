using OryAdmin;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables().Build(); // use environment variables

var startup = new Startup(builder.Configuration, builder.Environment);
startup.ConfigureServices(builder.Services);
var app = builder.Build();
await startup.Configure(app);

app.Run();