using Core.Interfaces;
using ECommerce.Errors;
using ECommerce.Middleware;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var GetAssemblies = AppDomain.CurrentDomain.GetAssemblies()
//                                    .Where(x => x.FullName.Contains("Online.AutoTender.API", StringComparison.OrdinalIgnoreCase));

//builder.Services.AddAutoMapper(GetAssemblies);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHealthChecks();
builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.InvalidModelStateResponseFactory = actionContext =>
     {
         var errors = actionContext.ModelState.Where(d => d.Value?.Errors.Count > 0)
                                             .SelectMany(d => d.Value?.Errors)
                                             .Select(d => d.ErrorMessage).ToArray();

         var errorResponse = new ApiValidationErrorResponse
         {
             Errors = errors
         };

         return new BadRequestObjectResult(errorResponse);
     };
});

builder.Services.AddDbContext<StoreContext>(opt =>
  {
      opt.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceConnection"));
  });

builder.Services.AddInfrastructureServices();

var app = builder.Build();
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

//try
//{
//    var context = app.Services.GetRequiredService<StoreContext>();
//    await context.Database.MigrateAsync();
//    await StoreContextSeed.SeedAsync(context);
//}
//catch (Exception ex)
//{
//    var logger = loggerFactory.CreateLogger<Program>();
//    logger.LogError(ex, "An error occured during migration");
//}

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePagesWithRedirects("/errors/{0}");

app.UseHttpsRedirection();

app.UseHealthChecks("/healthcheck");

app.UseAuthorization();

app.MapControllers();

app.Run();
