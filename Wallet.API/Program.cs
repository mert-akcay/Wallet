using Microsoft.EntityFrameworkCore;
using Wallet.API.ExceptionHandler;
using Wallet.Application.Behaviuors;
using Wallet.Application.Commands;
using Wallet.Application.Mappings;
using Wallet.Application.Validation;
using Wallet.Infrastructure.DbContext;
using Wallet.Infrastructure.Repositories;
using Wallet.Infrastructure.Repositories.Interface;
using Wallet.Infrastructure.UnitOfWork;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly).AddOpenBehavior(typeof(UnitOfWorkBehavior<,>)));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(WalletCreateCommand).Assembly));


builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

builder.Services.AddScoped<IUnityOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepository<Wallet.Domain.Entities.Wallet>, Repository<Wallet.Domain.Entities.Wallet>>();
builder.Services.AddScoped<IValidator, Validator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler(opt =>
{

});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
