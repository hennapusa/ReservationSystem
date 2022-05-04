using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Repositories
{
    public interface IItemRepository
    {
        public Task<Item> GetItemAsync(long id);
        public Task<Item> AddItemAsync(Item item);
        public Task<IEnumerable<Item>> GetAllItems();
        public Task<IEnumerable<Item>> GetItemsOfUser(User user);
        public Task<IEnumerable<Item>> QueryItems(String query);
        public Task<User> GetUserAsync(long id);
        public Task<Boolean> DeleteItemAsync(long id);

        public Task<Item> UpdateItem(Item item);
    }
}
