using Microsoft.EntityFrameworkCore;
using BuildWebapiMinimalApi.Models;
namespace BuildWebapiMinimalApi.Data;

class PizzaContext : DbContext
{
    public PizzaContext(DbContextOptions options) : base(options) { }
    public DbSet<Pizza> Pizzas => Set<Pizza>();
}