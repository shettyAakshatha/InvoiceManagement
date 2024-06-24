using InvoiceManagement;
using InvoiceManagement.AutoMapper;
using InvoiceManagement.InvoiceRepository;
using InvoiceManagement.InvoiceService;
using Serilog;
using System.Runtime;



var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment.EnvironmentName == "Development" ? "dev" : "prod";
builder.Configuration.AddJsonFile($"appsettings.{env}.json");
// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());

builder.Services.AddSingleton<IInvoiceRepository, InvoiceRepository>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IInvoiceService,InvoiceService>();
builder.Services.AddControllers(Options => { Options.Filters.Add<ValidateAtrribute>(); });
builder.Services.AddAutoMapper(typeof(InvoiceProfile).Assembly);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseExceptionMiddleware();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
