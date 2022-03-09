﻿using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Services
{
   public interface IUserService
    {
        public Task<UserDTO> CreateUserAsync(User user);
        public Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    }
}