using Auth.Consumers;
using Auth.Data;
using Auth.Interfaces.Repositories;
using Auth.Interfaces.Services;
using Auth.Repositories;
using Auth.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<RegistConsumer>();
    x.AddConsumer<LoginConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ReceiveEndpoint("AuthRegistQueue", e =>
        {
            e.ConfigureConsumer<RegistConsumer>(context);
        });
        cfg.ReceiveEndpoint("AuthLoginQueue", e =>
        {
            e.ConfigureConsumer<LoginConsumer>(context);
        });

        cfg.ClearSerialization();
        cfg.UseRawJsonSerializer();
        cfg.ConfigureEndpoints(context);
    });
});

ConfigurationHelper.Initialize(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

app.Run();
