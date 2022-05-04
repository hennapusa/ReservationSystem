using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Services
{
    public interface IReservationService
    {
        public Task <ReservationDTO>CreateReservationAsync(ReservationDTO res);
        public Task <ReservationDTO>CreateReservationAsync(long id);
        //public Task<ReservationDTO> GetReservation(long id);
        public Task<IEnumerable<ReservationDTO>> GetAllReservations();
        public Task<IEnumerable<ReservationDTO>> GetAllReservationsForUser(String Username);
        public Task<ReservationDTO> UpdateReservation(ReservationDTO reservation);

        public Task<IEnumerable<ReservationDTO>> GetReservation(long id);




    }
}
