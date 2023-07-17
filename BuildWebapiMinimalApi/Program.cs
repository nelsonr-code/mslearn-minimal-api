using BuildWebapiMinimalApi.Data;
using Microsoft.EntityFrameworkCore;
using BuildWebapiMinimalApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=Pizzas.db";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<PizzaContext>(connectionString);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "BuildWebapiMinimalApi", 
        Version = "v1", 
        Description = "This is my first Minimal API with help of mslearn training"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BuildWebapiMinimalApi v1");
});

app.MapGet("/", () => "Hello World!");
app.MapGet("/pizzas", async (PizzaContext db) => await db.Pizzas.ToListAsync());
app.MapPost("/pizzas", async (PizzaContext db, Pizza pizza) =>
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    
    return Results.Created($"/pizzas/{pizza.Id}", pizza);
});
app.MapGet("/pizza/{id:int}", async (PizzaContext db, int id) => await db.Pizzas.FindAsync(id));
app.MapPut("/pizza/{id:int}", async (PizzaContext db, Pizza updatepizza, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();
    pizza.Name = updatepizza.Name;
    pizza.Description = updatepizza.Description;
    await db.SaveChangesAsync();
    
    return Results.Ok(pizza);
});
app.MapDelete("/pizzas/{id:int}", async (PizzaContext db, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();
    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    
    return Results.Ok();
});

app.Run();