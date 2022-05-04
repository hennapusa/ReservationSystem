using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Services
{
    public interface IItemService
    {
        public Task<ItemDTO> CreateItemAsync(ItemDTO item);
        public Task<IEnumerable<ItemDTO>> GetAllItems();
        public Task<IEnumerable<ItemDTO>> GetItems(String username);
        public Task<IEnumerable<ItemDTO>> QueryItems(String query);
        public Task<ItemDTO> UpdateItem(ItemDTO item);
    }
}
