using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Repositories
{
   public interface IReservationRepository
    {
        public Task<Reservation> AddReservationAsync(Reservation res);
        public Task<Reservation> GetReservationAsync(long id);
        Task<IEnumerable<Reservation>> GetReservationAsync();
        Task<IEnumerable<Reservation>> GetReservationAsync(Item target, DateTime start, DateTime end);
        Task<IEnumerable<Reservation>> GetReservationsAsync(User user);
        Task<IEnumerable<Reservation>> GetReservationsAsync(Item item);
    }
}
