using Microsoft.EntityFrameworkCore;
using NLog;
using Repositories.Abstract;
using Repositories.EFCore;
using Services.Abstract;
using WebApi.Extensions;
using WebApi.Utilities;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
     policy =>
     {
         policy.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
     });
});

// Add services to the container.
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly); //reflection


//builder.Services.AddCors(); 


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddAuthentication(); // kullanýcý adý þifre middle veriyi aktifleþtirmek
builder.Services.ConfigureIdentity(); //serviceextensions dosyasýndaki method 
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureLoggerService();


var app = builder.Build();

var logger=app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

//app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

/*
app.UseCors(builder => builder.WithOrigins("https://localhost:4200")
                                .AllowAnyMethod()
                                .AllowAnyHeader());
*/
/*
app.UseCors(builder => builder.WithOrigins("https://localhost:4200")
                              .AllowAnyMethod()
                              .WithHeaders("authorization", "accept", "content-type", "origin"));
*/


app.UseHttpsRedirection();

app.UseRouting();
//app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication(); //kullanýcý adý ve þifre ile oturum açma iþlemleri gerçekleþecek
app.UseAuthorization();//oturum açtýktan sonra yetkilendirme

app.MapControllers();

app.Run();
