using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PruebaLogan.CONTEXT.Context;
using PruebaLogan.IOC.Dependecies;
using PruebaLogan.MODEL.Mapper;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

Dependencies.RegistrarDependencias(builder.Services);
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
#region RegisterAddCors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("*")
                          .AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});
#endregion



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new Automaping());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<TestLoganContext>(options =>
      options
      .UseLazyLoadingProxies()
      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
      .UseSqlServer(builder.Configuration.GetConnectionString("loganDatabase"))
      );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(MyAllowSpecificOrigins);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
