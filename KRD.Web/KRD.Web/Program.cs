using KRD.Data;
using KRD.Data.Enum;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

NpgsqlConnection.GlobalTypeMapper.MapEnum<Color>("color");
NpgsqlConnection.GlobalTypeMapper.MapEnum<OptionType>("option_type");
NpgsqlConnection.GlobalTypeMapper.MapEnum<Status>("status");

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection, npgsqlOptions =>
{
    npgsqlOptions.MapEnum<Color>("color");
    npgsqlOptions.MapEnum<OptionType>("option_type");
    npgsqlOptions.MapEnum<Status>("status");
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

/*app.UseRouting();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
*/
app.MapControllers();

app.Run();