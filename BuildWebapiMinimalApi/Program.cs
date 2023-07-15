using BuildWebapiMinimalApi.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
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
app.MapGet("/pizzas/{id:int}", PizzaDb.GetPizza);
app.MapGet("/pizzas", PizzaDb.GetPizzas);
app.MapPost("/pizzas", (Pizza pizza) => PizzaDb.CreatePizza(pizza));
app.MapPut("/pizzas", (Pizza pizza) => PizzaDb.UpdatePizza(pizza));
app.MapDelete("/pizzas/{id:int}", (int id) => PizzaDb.RemovePizza(id));

app.Run();