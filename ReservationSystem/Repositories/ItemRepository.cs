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

        public async Task<Item> GetItemAsync(long id)//long id
        {
            //return await _context.Items.FindAsync(newItem);//id
            return await _context.Items.FindAsync(id);
        }

        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _context.Items.Include(i => i.Owner).ToListAsync();
        }

        public Task<User> GetUserAsync(long id)
        {
            throw new NotImplementedException();
        }

        internal Task<Item> CreateItemAsync(Item item)
        {
            throw new NotImplementedException();
        }

       
        public async Task<IEnumerable<Item>> GetItemsOfUser(User user)
        {
            return await _context.Items.Include(i => i.Owner).Where(x => x.Owner == user).ToListAsync();
        }

        public async Task<IEnumerable<Item>> QueryItems(string query)
        {
            return await _context.Items.Include(i => i.Owner).Where(x => x.Name.Contains(query)).ToListAsync();
        }
    }
}
