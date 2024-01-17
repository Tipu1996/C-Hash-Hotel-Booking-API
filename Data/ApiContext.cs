using Microsoft.EntityFrameworkCore;
using HotelBookingAPI.Models;
using MongoDB.Driver;

namespace HotelBookingAPI.Data
{
    public class ApiContext
    {
        private readonly IConfiguration _configuration;
        public IMongoCollection<HotelBooking> Bookings { get; set; }
        public ApiContext(IConfiguration configuration)
        {
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("MongoDB");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("HotelBooking");
            Bookings = database.GetCollection<HotelBooking>("Bookings");
        }

    }
}