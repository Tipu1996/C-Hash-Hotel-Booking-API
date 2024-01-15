using Microsoft.EntityFrameworkCore;
using HotelBookingAPI.Models;
using MongoDB.Driver;

namespace HotelBookingAPI.Data
{
    public class ApiContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public IMongoCollection<HotelBooking> Bookings { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("MongoDB");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("HotelBooking");
            Bookings = database.GetCollection<HotelBooking>("Bookings");
        }

    }
}