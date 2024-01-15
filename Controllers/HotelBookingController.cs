using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Models;
using HotelBookingAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;

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
            if (booking.Id == 0)
            {
                _context.Bookings.Add(booking);
            }
            else
            {
                var bookingInDb = _context.Bookings.Find(booking.Id);
                if (bookingInDb == null)
                {
                    return new JsonResult(NotFound());
                }
                bookingInDb.RoomNumber = booking.RoomNumber;
                bookingInDb.ClientName = booking.ClientName;
            }
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