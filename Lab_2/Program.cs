using Lab_2.Services.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services to the container.


builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddControllersWithViews();
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddEndpointsApiExplorer();



var app = builder.Build();

// Continue with the rest of your configuration...

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSerilogRequestLogging();

app.UseAuthorization();
app.Use((context, next) =>
{
    var request = context.Request;
    var ipAddress = context.Connection.RemoteIpAddress;
    var requestTime = DateTime.Now;

    var logMessage = $"Request: {request.Scheme}://{request.Host}{request.Path}{request.QueryString}, " +
                     $"Time: {requestTime}, " +
                     $"IP: {ipAddress}";

    // ЋогуЇмо дан≥ про запит
    context.RequestServices.GetRequiredService<ILogger<Program>>().LogInformation(logMessage);

    return next();
});
app.UseRouting();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
