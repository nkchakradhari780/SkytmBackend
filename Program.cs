using SkytmBackend.Data;
using Microsoft.EntityFrameworkCore; // Add this using directive to resolve 'UseSqlServer'

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddCors(c => c.AddPolicy("CorsPolicy", build =>
{
    build
         .WithOrigins(
            "http://localhost:4200",
            "http://localhost:4292",
            "http://localhost:8100",
            "http://localhost:8400"

        )
         .AllowAnyMethod()
         .AllowAnyHeader()
         .AllowCredentials();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "innotm v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
