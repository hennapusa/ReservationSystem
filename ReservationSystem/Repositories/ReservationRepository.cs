using Microsoft.EntityFrameworkCore;
using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationContext _context;
        public ReservationRepository(ReservationContext context) 
        {
            _context = context;
        }
        public async Task<Reservation> AddReservationAsync(Reservation res)
        {
            //Tallenna uusi varaus
            _context.Reservations.Add(res);
            try { 
            await _context.SaveChangesAsync();
            }
            catch(Exception)
            {

            }
            return res;


        }

        public async Task<Reservation> GetReservationAsync(long id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        public async Task<IEnumerable<Reservation>> GetReservationAsync()
        {
            return await _context.Reservations.Include(i => i.Owner).Include(i => i.Target).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationAsync(Item target, DateTime start, DateTime end)
        {
            return await _context.Reservations.Include(i => i.Owner).Include(i => i.Target).Where(x => x.Target == target && ((x.Start >= start && x.Start < end) || (x.End > start && x.End < end) || (x.Start <= start && x.End > end))).ToListAsync(); //tästä puuttuu kellon aika rajaukset
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync(User user)
        {
            return await _context.Reservations.Include(i => i.Owner).Include(i => i.Target).Where(x => x.Owner == user).ToListAsync();
        }

    }
}
