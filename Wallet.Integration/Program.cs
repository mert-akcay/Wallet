using MassTransit;
using Wallet.Integration.Consumers;
using Wallet.Integration.Helpers;
using Wallet.Integration.Services.CardService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//builder.Services.AddScoped(ICardService)
//builder.Services.AddHttpClient<IHttpHelper, HttpHelper>();    
builder.Services.AddScoped<IHttpHelper, HttpHelper>();
builder.Services.AddHttpClient<ICardService, CardService>(opt =>
{
    opt.BaseAddress = new Uri("http://localhost:5020/api/card/");
});
builder.Services.AddMassTransit(x =>
{
    // Projemizdeki bu consumer'� MassTransit'e tan�t�yoruz.
    x.AddConsumer<WalletCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", 5672, "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // Bir 'receive endpoint' (kuyruk) tan�ml�yoruz.
        // MassTransit, bu kuyru�u OrderCreatedConsumer'�n dinledi�i
        // t�m olaylar i�in otomatik olarak subscribe edecektir.
        //cfg.ReceiveEndpoint("wallet-queue", e =>
        //{
        //    e.ConfigureConsumer<WalletCreatedConsumer>(context);
        //});
        cfg.ConfigureEndpoints(context);
        cfg.UseRawJsonSerializer();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
