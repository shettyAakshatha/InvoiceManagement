using InvoiceManagement;
using InvoiceManagement.AutoMapper;
using InvoiceManagement.InvoiceRepository;
using InvoiceManagement.InvoiceService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddSingleton<IInvoiceRepository, InvoiceRepository>();

builder.Services.AddScoped<IInvoiceService,InvoiceService>();
builder.Services.AddControllers(Options => { Options.Filters.Add<ValidateAtrribute>(); });
builder.Services.AddAutoMapper(typeof(InvoiceProfile).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
