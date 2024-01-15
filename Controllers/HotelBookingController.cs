using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Models;
using HotelBookingAPI.Data;

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
        public JsonResult CreateEdit(HotelBooking booking)
        {

            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return new JsonResult(Ok(booking));
        }

        [HttpGet("/bookings")]
        public IActionResult GetAll()
        {
            var result = _context.Bookings.ToList();
            if (result == null) { return new JsonResult(NotFound()); }
            else { return Ok(result); }
        }

        [HttpPut("/bookings/{id}")]
        public IActionResult EditById(int id, HotelBooking booking)
        {
            var bookingInDb = _context.Bookings.Find(id);
            if (bookingInDb == null)
            { return new JsonResult(NotFound()); }
            else
            {
                bookingInDb.RoomNumber = booking.RoomNumber;
                bookingInDb.ClientName = booking.ClientName;
                _context.SaveChanges();
                return new JsonResult(Ok(booking));
            }
        }

        [HttpGet("/bookings/{id}")]
        public IActionResult GetById(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null)
            { return new JsonResult(NotFound()); }
            else
            { return new JsonResult(Ok(booking)); }
        }

        [HttpDelete("/bookings/{id}")]
        public IActionResult Delete(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null)
            { return new JsonResult(NotFound()); }
            else
            {
                _context.Bookings.Remove(booking);
            }
            _context.SaveChanges();
            return new JsonResult(NoContent());
        }
    }
}