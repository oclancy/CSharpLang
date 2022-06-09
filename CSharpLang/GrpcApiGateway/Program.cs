using GrpcApiGateway;

using Monitor = GrpcDependencies.Monitor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddSingleton<IGrpcClient>(p => new GrpcClient(builder.Configuration["ServerUrl"]));
builder.Services.AddGrpcClient<Monitor.MonitorClient>(o =>
{
    o.Address = new Uri(builder.Configuration["ServerUrl"]);
});

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
