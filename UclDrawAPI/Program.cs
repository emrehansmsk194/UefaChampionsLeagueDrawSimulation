using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UclDrawAPI;
using UclDrawAPI.Data;
using AutoMapper; // Ensure AutoMapper namespace is included  
using Microsoft.OpenApi.Models;
using UclDrawAPI.Services.IServices;
using UclDrawAPI.Services; // Add this namespace for Swagger support  

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  

builder.Services.AddControllers();
builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingConfig>());
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default Connection")));
builder.Services.AddEndpointsApiExplorer(); // ← Bu eksikti  
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "UclDrawAPI", Version = "v1" });
}); // Fix: Ensure SwaggerGen is properly configured  
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.AllowAnyOrigin() 
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});

var app = builder.Build();

// Pipeline  
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
