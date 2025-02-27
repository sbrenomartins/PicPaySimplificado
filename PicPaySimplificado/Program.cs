using Microsoft.EntityFrameworkCore;
using PicPaySimplificado.Infra.Database;
using PicPaySimplificado.Infra.Repositories.Transfer;
using PicPaySimplificado.Infra.Repositories.Wallet;
using PicPaySimplificado.Services.Authorization;
using PicPaySimplificado.Services.Notification;
using PicPaySimplificado.Services.Transfer;
using PicPaySimplificado.Services.Wallet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var serverVersion = new MySqlServerVersion(new Version(9, 1, 0));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion));

builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<ITransferRepository, TransferRepository>();

builder.Services.AddScoped<ITransferService, TransferService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddHttpClient<IAuthorizationService, AuthorizationService>();

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