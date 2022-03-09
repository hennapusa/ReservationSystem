﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Models;
using ReservationSystem.Services;

namespace ReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //private readonly ReservationContext _context;
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        // GET: api/Users
        /// <summary>
        /// Gets a list of all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _service.GetAllUsersAsync());
            //return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        /// <summary>
        /// Gets one user
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>User information as json</returns>
        /// <responce code="200">User found</responce>
        /// <responce code="404">User not found</responce>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            //var user = await _context.Users.FindAsync(id);

            //if (user == null)
            //{
            //  return NotFound();
            // }

            // return user;
            return null;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Edit user
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <param name="user">User´s new information</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            //if (id != user.Id)
           // {
           //     return BadRequest();
           // }

           // _context.Entry(user).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
           // }
           // catch (DbUpdateConcurrencyException)
           // {
            //    if (!UserExists(id))
            //    {
              //      return NotFound();
               // }
               // else
               // {
               //     throw;
              //  }
           // }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Add a new user to system
        /// </summary>
        /// <param name="user">User information</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<UserDTO>> PostUser(User user)
        {
            UserDTO newUser = await _service.CreateUserAsync(user);

            return CreatedAtAction("GetUser", new { id = newUser.Id }, newUser);
        }

        // DELETE: api/Users/5
        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">Id of user to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> DeleteUser(long id)
        {
            //var user = await _context.Users.FindAsync(id);
            //if (user == null)
            // {
            //    return NotFound();
            //}

            // _context.Users.Remove(user);
            // await _context.SaveChangesAsync();

            //return user;
            return null;
        }

    }
}