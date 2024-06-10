using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApp_Backend.Data;
using RestaurantApp_Backend.Models;

namespace RestaurantApp_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly RestaurantApp_BackendContext _context;

        public DishesController(RestaurantApp_BackendContext context)
        {
            _context = context;
        }

        // GET: api/Dishes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dish>>> GetDish()
        {
          if (_context.Dish == null)
          {
              return NotFound();
          }
            return await _context.Dish.ToListAsync();
        }

        // GET: api/Dishes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dish>> GetDish(int id)
        {
          if (_context.Dish == null)
          {
              return NotFound();
          }
            var dish = await _context.Dish.FindAsync(id);

            if (dish == null)
            {
                return NotFound();
            }

            return dish;
        }
        // GET: api/Dishes by Name
        [HttpGet("ByName")]
        public async Task<ActionResult<Dish>> GetDishByName(string name)
        {
            // Find the dish by name in the database
            var dish = await _context.Dish.FirstOrDefaultAsync(d => d.Dish_Name == name);

            if (dish == null)
            {
                // If dish with the specified name is not found, return 404 Not Found
                return NotFound();
            }

            // If dish is found, return it
            return dish;
        }


        // PUT: api/Dishes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDish(int id, Dish dish)
        {
            if (id != dish.DishId)
            {
                return BadRequest();
            }

            _context.Entry(dish).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Dishes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dish>> PostDish(Dish dish)
        {
          if (_context.Dish == null)
          {
              return Problem("Entity set 'RestaurantApp_BackendContext.Dish'  is null.");
          }
            _context.Dish.Add(dish);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDish", new { id = dish.DishId }, dish);
        }

        // DELETE: api/Dishes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            if (_context.Dish == null)
            {
                return NotFound();
            }
            var dish = await _context.Dish.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }

            _context.Dish.Remove(dish);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DishExists(int id)
        {
            return (_context.Dish?.Any(e => e.DishId == id)).GetValueOrDefault();
        }
    }
}
