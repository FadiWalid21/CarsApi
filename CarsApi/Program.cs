
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Core.Interfaces;
// using Infrastructure.Service;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json;
using Infrastructure.Service;
using Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// builder.Services.AddControllers(options =>{
//     // options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
//     // options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions(JsonSerializerDefaults.Web)
//     // {
//     //     ReferenceHandler = ReferenceHandler.Preserve,
//     // }));
// });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<ICarServices, CarService>();
builder.Services.AddScoped<ICategoryServices, CategoryService>();
builder.Services.AddScoped<IBrandServices, BrandService>();
builder.Services.AddScoped<IFavService,FavService>();

builder.Services.AddScoped<IModelServices, ModelService>();
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, int>>()
.AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, int>>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

