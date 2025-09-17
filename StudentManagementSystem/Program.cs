using StudentManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();

		//builder.Services.AddSingleton<IStudentRepository, FakeDbRepository>();

		builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

		var app = builder.Build();

		using (var scope = app.Services.CreateScope())
		{
			var services = scope.ServiceProvider;
			var context = services.GetRequiredService<AppDbContext>();
			context.Database.EnsureCreated();
			DbInitializer.Seed(context);
		}

		// Configure the HTTP request pipeline.
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
			pattern: "{controller=Students}/{action=Index}/{id?}");

		app.Run();
	}
}

/* We Use This Code To Run Program While In Development Mode in Mobile or Other Device 
 
 using StudentManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Kestrel configuration: allow access from mobile (same Wi-Fi)
builder.WebHost.ConfigureKestrel(options =>
{
	// HTTP (http://192.168.100.50:7038)
	options.ListenAnyIP(7038);

	// Agar tumhe HTTPS chahiye to certificate add karna padega
	// options.ListenAnyIP(7039, listenOptions =>
	// {
	//     listenOptions.UseHttps("certificate.pfx", "password");
	// });
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>
	(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

var app = builder.Build();

// ✅ Database initialize & seed
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<AppDbContext>
		();
	context.Database.EnsureCreated();
	DbInitializer.Seed(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

// (temp kia he isko wapas hatana he) app.UseHttpsRedirection(); // yeh rahega, lekin mobile par tum http://IP:7038 use karoge
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
name: "default",
pattern: "{controller=Students}/{action=Index}/{id?}");

app.Run();
*/