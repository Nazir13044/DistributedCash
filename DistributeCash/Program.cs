using DistributedCash.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var appSettings = builder.Configuration.GetSection("AppSetings").Get<AppSetings>();
ConfigureServices(builder.Services, builder.Configuration, appSettings);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration, AppSetings appSettings)
{
    //if (appSettings.IsValid())
    //{
        services.Configure<AppSetings>(configuration.GetSection("AppSetings"));
    //}
}
