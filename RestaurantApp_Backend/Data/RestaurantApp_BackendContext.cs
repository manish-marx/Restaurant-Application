using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantApp_Backend.Models;

namespace RestaurantApp_Backend.Data
{
    public class RestaurantApp_BackendContext : DbContext
    {
        public RestaurantApp_BackendContext (DbContextOptions<RestaurantApp_BackendContext> options)
            : base(options)
        {
        }

        public DbSet<RestaurantApp_Backend.Models.Dish> Dish { get; set; } = default!;

        public DbSet<Restaurant> Restaurant { get; set; } = default!;
    }
}
