using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ReservationSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ReservationContext _context;
        public UserRepository(ReservationContext context)
        {
            _context = context;
        }
        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return user;
        }

        public async Task <Boolean> DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(string userName)
        {
            return await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();

        }

        public async Task<User> GetUserAsync(long id)
        {
            User user = await _context.Users.FindAsync(id);
            return user;
        }

        public async Task<User> UptadeUserAsync(User user)
        {
            _context.Users.Update(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
            return user;
        }


        public async Task<User> UptadeUserAsync(UserDTO user)
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return null;
            }
            return null;
        }



        /* Task<bool> IUserRepository.DeleteUserAsync(User user)
         {
             _context.Users.Delete(user);
             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (Exception)
             {
                 return null;
             }
             return user;
         }*/
    }
}
