using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Wallet.API.ExceptionHandler;
using Wallet.API.Logging;
using Wallet.API.Middlewares;
using Wallet.Application.Behaviuors;
using Wallet.Application.Commands;
using Wallet.Application.Consumers;
using Wallet.Application.Mappings;
using Wallet.Application.Validation;
using Wallet.Infrastructure.DbContext;
using Wallet.Infrastructure.Repositories;
using Wallet.Infrastructure.Repositories.Interface;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddMassTransit(x =>
{
    // Projemizdeki bu consumer'ý MassTransit'e tanýtýyoruz.
    x.AddConsumer<CardCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", 5672, "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // Bir 'receive endpoint' (kuyruk) tanýmlýyoruz.
        // MassTransit, bu kuyruðu OrderCreatedConsumer'ýn dinlediði
        // tüm olaylar için otomatik olarak subscribe edecektir.
        //cfg.ReceiveEndpoint("wallet-queue", e =>
        //{
        //    e.ConfigureConsumer<WalletCreatedConsumer>(context);
        //});
        cfg.ConfigureEndpoints(context);
        cfg.UseRawJsonSerializer();
    });
});


builder.Host.UseSerilog();
builder.Services.AddSerilogLogging(builder.Configuration);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly).AddOpenBehavior(typeof(UnitOfWorkBehavior<,>)));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(WalletCreateCommand).Assembly));

builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

builder.Services.AddScoped<IRepository<Wallet.Domain.Entities.Wallet>, Repository<Wallet.Domain.Entities.Wallet>>();
builder.Services.AddScoped<IRepository<Wallet.Domain.Entities.Parameter>, Repository<Wallet.Domain.Entities.Parameter>>();

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

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
