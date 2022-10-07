using SafetyEquipmentInspectionAPI.Controllers;
using SafetyEquipmentInspectionAPI.Interfaces;

var AllowedOrigins = "_allowedOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowedOrigins,
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:44451");
                    });
});

builder.Services.AddTransient<IEquipmentController, EquipmentController>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(AllowedOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
