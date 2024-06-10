using System.ComponentModel.DataAnnotations;

namespace RestaurantApp_Backend.Models
{
    public class Dish
    {
        public int DishId { get; set; }
        public string Dish_Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Rest_Id { get; set; }
    }
}
public class Restaurant
{
    [Key]
    public int Rest_Id { get; set; }
    public string Rest_Name { get; set; }
    public string Contact { get; set; }
}
