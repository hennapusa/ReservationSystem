using Microsoft.EntityFrameworkCore;
using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ReservationContext _context;
        public ItemRepository(ReservationContext context)
        {
            _context = context;
        }
        public async Task<Item> AddItemAsync(Item item)
        {
            _context.Items.Add(item);
            try
            {
               await _context.SaveChangesAsync();
            }
            catch(Exception)
            {

            }
            return item;
        }

        public async Task<Item> GetItemAsync(long id)
        {
            //return await _context.Items.FindAsync(newItem);
            return await _context.Items.FindAsync(id);
        }

        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _context.Items.Include(i => i.Owner).ToListAsync();
        }

        public async Task<User> GetUserAsync(long id)
        {
            return await _context.Users.FindAsync(id);
        }

       /* internal async Task<Item> CreateItemAsync(Item item)
        {
            _context.Items.(item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
            }
            return item;
        }*/

       
        public async Task<IEnumerable<Item>> GetItemsOfUser(User user)
        {
            return await _context.Items.Include(i => i.Owner).Where(x => x.Owner == user).ToListAsync();
        }

        public async Task<IEnumerable<Item>> QueryItems(string query)
        {
            return await _context.Items.Include(i => i.Owner).Where(x => x.Name.Contains(query)).ToListAsync();
        }

        public Task<bool> DeleteItemAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Item> UpdateItem(Item item)
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}
