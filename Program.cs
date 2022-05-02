using EscolaId.Models;

using EscolaId.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    }); 
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

app.MapControllers();


app.Run();
