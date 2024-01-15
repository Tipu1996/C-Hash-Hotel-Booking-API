using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Models;
using HotelBookingAPI.Data;
using MongoDB.Driver;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HotelBookingController : ControllerBase
    {
        private readonly ApiContext _context;
        public HotelBookingController(ApiContext context)
        {
            _context = context;
        }


        [HttpPost("/bookings")]
        public JsonResult Create(HotelBooking booking)
        {
            _context.Bookings.InsertOne(booking);
            return new JsonResult(Ok(booking));
        }

        [HttpPut("/booking/{id}")]
        public IActionResult UpdateById(string id, HotelBooking update)
        {
            var filter = Builders<HotelBooking>.Filter.Eq(x => x.Id, id);
            var updated = Builders<HotelBooking>.Update
            .Set(x => x.RoomNumber, update.RoomNumber)
            .Set(x => x.ClientName, update.ClientName);
            var result = _context.Bookings.FindOneAndUpdate(filter, updated);

            if (result == null)
            { return new JsonResult(NotFound()); }
            return new JsonResult(Ok(result));
        }

        [HttpGet("/bookings")]
        public IActionResult GetAll()
        {
            var result = _context.Bookings.Find(_ => true).ToList();
            if (result == null || result.Count == 0) { return new JsonResult(NotFound()); }
            else { return Ok(result); }
        }

        [HttpGet("/bookings/{id}")]
        public IActionResult GetById(string id)
        {
            var booking = _context.Bookings.Find(x => x.Id == id).FirstOrDefault();
            if (booking == null)
            { return new JsonResult(NotFound()); }
            else
            { return new JsonResult(Ok(booking)); }
        }

        [HttpDelete("/bookings/{id}")]
        public IActionResult Delete(string id)
        {
            var booking = _context.Bookings.Find(x => x.Id == id).FirstOrDefault();
            if (booking == null) { return new JsonResult(NotFound()); }
            else { _context.Bookings.DeleteOne(x => x.Id == id); }
            return new JsonResult(NoContent());
        }
    }
}