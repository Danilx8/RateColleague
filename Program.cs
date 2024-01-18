using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quartz;
using RateColleague.Data;
using RateColleague.Models;
using RateColleague.Services.EmailSenderService;
using RateColleague.Services.RatingCollectionService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDatabase"))
);
builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
    });
builder.Services.AddQuartz(options =>
    options.UsePersistentStore(s =>
        {
            s.UseProperties = true;
            s.UsePostgres(builder.Configuration.GetConnectionString("PostgresDatabase")
                ?? throw new JobPersistenceException("Couldn't connect to database"));
            s.UseNewtonsoftJsonSerializer();
        }
    ));
builder.Services.AddQuartzHostedService(options =>
    options.WaitForJobsToComplete = true);
builder.Services.AddSingleton<IRatingCollectionService, RatingCollectionService>();
builder.Services.AddSingleton<IEmailSenderService, EmailSenderService>();
builder.Services.AddIdentityCore<Employee>(
    options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
