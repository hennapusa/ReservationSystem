using ReservationSystem.Models;
using ReservationSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IItemRepository _itemRepository;
        public ReservationService(IReservationRepository repository, IUserRepository userRepository, IItemRepository itemRepository) 
        {
            _repository = repository;
            _userRepository = userRepository;
            _itemRepository = itemRepository;

        }

        public async Task<ReservationDTO> CreateReservationAsync(ReservationDTO res)
        {
            if (res.Start >= res.End)
            {
                return null;
            }
            //Lisää varaus
            //Tarkista ensin onko kohde vapaa halutulla ajalla
            Item target = await _itemRepository.GetItemAsync(res.Target);
            if (target == null)
            {
                return null;
            }
            IEnumerable<Reservation> reservations = await _repository.GetReservationAsync(target, res.Start, res.End);
            if (reservations.Count() > 0)
            {
                return null;
            }
            Reservation newReservation = await DTOToReservationAsync(res);

            await _repository.AddReservationAsync(newReservation);
            return ReservationToDTO(newReservation);

        }

        public Task<ReservationDTO> CreateReservationAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ReservationDTO>> GetAllReservations()
        {
            IEnumerable<Reservation> reservations = await _repository.GetReservationAsync();
            List<ReservationDTO> reservationDTOs = new List<ReservationDTO>();
            foreach (Reservation r in reservations)
            {
                reservationDTOs.Add(ReservationToDTO(r));
            }
            return reservationDTOs;
        }
// lisää, jos muistat! GetReservationForItem(long id)
        public async Task<ReservationDTO> GetReservation(long id)
        {
            return ReservationToDTO(await _repository.GetReservationAsync(id));
        }

        public async Task<IEnumerable<ReservationDTO>> GetReservationForUser(string username)
        {
            User owner = await _userRepository.GetUserAsync(username);
            if (owner == null)
            {
                return null;
            }
            IEnumerable<Reservation> reservations = await _repository.GetReservationsAsync(owner);
            List<ReservationDTO> dTOs = new List<ReservationDTO>();
            foreach (Reservation r in reservations)
            {
                dTOs.Add(ReservationToDTO(r));
            }
            return dTOs;
        }

        //turha? vai mikä lie koodin murunen jäänyt ;D
        /*
        User owner = await _userRepository.GetUserAsync(dto.Owner);
            if(owner == null)
            {
                return null;
            }
        */
        private ReservationDTO ReservationToDTO(Reservation res)
        {
            ReservationDTO reservationDTO = new ReservationDTO();
            reservationDTO.Id = res.Id;
            reservationDTO.Target = res.Target.Id;
            reservationDTO.Owner = res.Owner.UserName;
            reservationDTO.Start = res.Start;
            reservationDTO.End = res.End;

            return reservationDTO;
        }

        private async Task<Reservation> DTOToReservationAsync(ReservationDTO dto)
        {
            Reservation reservation = new Reservation();
            User owner = await _userRepository.GetUserAsync(dto.Owner);

            if (owner == null)
            {
                return null;
            }

            Item target = await _itemRepository.GetItemAsync(dto.Target);
            reservation.Target = target;
            reservation.Id = dto.Id;
            reservation.Start = dto.Start;
            reservation.End = dto.End;
            reservation.Owner = owner;

            return reservation;
        }

        public async Task<IEnumerable<ReservationDTO>> GetAllReservationsForUser(string username)
        {
            User owner = await _userRepository.GetUserAsync(username);
            if (owner == null)
            {
                return null;
            }
            IEnumerable<Reservation> reservations = await _repository.GetReservationsAsync(owner);
            List<ReservationDTO> dtos = new List<ReservationDTO>();
            foreach(Reservation r in reservations)
            {
                dtos.Add(ReservationToDTO(r));
            }
            return dtos;
        }
    }

}
