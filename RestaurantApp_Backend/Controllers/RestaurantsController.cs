﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
    public class RestaurantsController : ControllerBase
    {
        private readonly RestaurantApp_BackendContext _context;

        public RestaurantsController(RestaurantApp_BackendContext context)
        {
            _context = context;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurant()
        {
          if (_context.Restaurant == null)
          {
              return NotFound();
          }
            return await _context.Restaurant.ToListAsync();
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Dish>>>GetRestaurant(int id)
        {
          if (_context.Restaurant == null)
          {
              return NotFound();
          }
            var restaurant = await _context.Dish.Where(x => x.Rest_Id == id).ToListAsync();

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }

        // PUT: api/Restaurants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, Restaurant restaurant)
        {
            if (id != restaurant.Rest_Id)
            {
                return BadRequest();
            }

            _context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
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

        // POST: api/Restaurants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
          if (_context.Restaurant == null)
          {
              return Problem("Entity set 'RestaurantApp_BackendContext.Restaurant'  is null.");
          }
            _context.Restaurant.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Rest_Id }, restaurant);
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            if (_context.Restaurant == null)
            {
                return NotFound();
            }
            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurant.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantExists(int id)
        {
            return (_context.Restaurant?.Any(e => e.Rest_Id == id)).GetValueOrDefault();
        }
    }
}
