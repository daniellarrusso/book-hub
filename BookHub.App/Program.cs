using BookHub.Core.Repositories;
using BookHub.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext (InMemory for now, swap for SQL Server/MySQL later)
builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repository
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed data for demo
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookContext>();
    if (!context.Books.Any())
    {
        context.Books.AddRange(
        new Book { Id = 1, Title = "1984", Author = "George Orwell" },
        new Book { Id = 2, Title = "Clean Code", Author = "Robert C. Martin" },
        new Book { Id = 3, Title = "Brave New World", Author = "Aldous Huxley" }
        );
        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
