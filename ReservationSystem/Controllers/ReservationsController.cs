using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Middleware;
using ReservationSystem.Models;
using ReservationSystem.Services;

namespace ReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationContext _context;
        private readonly IReservationService _service;
        private readonly IUserAuthenticationService _authenticationService;


        public ReservationsController(IReservationService service, IUserAuthenticationService authenticationService)
        {
            _service = service;
            _authenticationService = authenticationService;
            
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
        {
            return Ok(await _service.GetAllReservations());
        }
        /// <summary>
        /// Get reservations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<ReservationDTO>> GetReservations(long id)
        {
            // return await _context.Reservations.ToListAsync();
            ReservationDTO dto = await _service.GetReservation(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(await _service.GetReservation(id));
        }

        /// <summary>
        /// Get reservations via id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservation(long id)
        {
            //var reservation = await _context.Reservations.FindAsync(id);

            var reservation = await _service.GetReservation(id);
            if (reservation == null)
         {
             return NotFound();
         }
         return Ok(reservation);
            //return Ok(await _service.GetAllReservations());
        }
        /// <summary>
        /// Get reservations via username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        // GET: api/Reservation/user/username
        [HttpGet("user/{username}")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations(String username)
        {
            return Ok(await _service.GetAllReservationsForUser(username));

        }
        /// <summary>
        /// Edit reservation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reservation"></param>
        /// <returns></returns>
        // PUT: api/Reservations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(long id, ReservationDTO reservation)
        {
            if (id != reservation.Id)
            {
               return BadRequest();
            }

             _context.Entry(reservation).State = EntityState.Modified;

            try
             {
                await _context.SaveChangesAsync();
             }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            /*    if (!ReservationExists(id))
            //    {
                    return NotFound();
               }
                else
               {
                  throw;
               }*/
            }

            return NoContent();
        }
        /// <summary>
        /// Make reservation
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        // POST: api/Reservations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservation)
        {
            // oikeus lisätä varaus
            bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, reservation);
            if (!isAllowed)
            { return Unauthorized(); }
            reservation = await _service.CreateReservationAsync(reservation);
            if (reservation == null)
            {
                return StatusCode(500);
            }

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
            
        }
        /// <summary>
        /// Delete reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(long id)
        {
             var reservation = await _context.Reservations.FindAsync(id);
             if (reservation == null)
             {
                 return NotFound();
             }

              _context.Reservations.Remove(reservation);
             await _context.SaveChangesAsync();

            return reservation;
            //return null;
        }
    }
}
