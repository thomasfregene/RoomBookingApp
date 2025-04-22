using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Persistence;
using RoomBookingApp.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connString = "Filename=:memory:";
var conn = new SqliteConnection();
conn.Open();

builder.Services.AddDbContext<RoomBookingAppDbContext>(opt=>opt.UseSqlite(conn));
EnsureDatabaseCreated(conn);

static void EnsureDatabaseCreated(SqliteConnection conn)
{
    var build = new DbContextOptionsBuilder<RoomBookingAppDbContext>();
    build.UseSqlite(conn);

    using var context = new RoomBookingAppDbContext(build.Options);
    context.Database.EnsureCreated();
}

builder.Services.AddScoped<IRoomBookingService, RoomBookingService>();
builder.Services.AddScoped<IRoomBookingRequestProcessor, RoomBookingRequestProcessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
