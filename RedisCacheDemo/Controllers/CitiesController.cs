using CacheManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedisCacheDemo.Context;
using RedisCacheDemo.Model;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisCacheDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly ICacheService _cache;

        public CitiesController(AppDbContext dbContext, ICacheService cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }
        [HttpGet(template: "GetiCitiesWithCache")]
        public async Task<IEnumerable<Ciudades>> Get()
        {
            var data = _cache.GetCacheValueAsync<Ciudades>("Cities");
            if (data.Result is null)
            {
                var cities = await GetCities();
                await _cache.SetCacheValueAsync("Cities", JsonSerializer.Serialize(cities));
                return _cache.GetCacheValueAsync<Ciudades>("Cities").Result;
            }
            return data.Result;
        }

        [HttpGet(template: "GetCitiesNoCache")]
        public  async Task<IEnumerable<Ciudades>> GetCitiesNoCache()
        {
            return await GetCities();
        }

        // GET api/<CitiesController>/5
        [HttpGet(template: "{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CitiesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CitiesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CitiesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private async Task<List<Ciudades>> GetCities()
        {
            return await _dbContext.tbCiudades.ToListAsync();
        }
    }
}
