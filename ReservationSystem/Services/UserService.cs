using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using ReservationSystem.Models;
using ReservationSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ReservationSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<UserDTO> CreateUserAsync(User user)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng=RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(password: user.Password, salt: salt, prf: KeyDerivationPrf.HMACSHA256, iterationCount: 10000, numBytesRequested: 256 / 8));

            User newUser = new User
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                salt = salt,
                Password = hashedPassword,
                IsAdmin= user.IsAdmin
            };

            newUser = await _repository.AddUserAsync(newUser);
            return UserToDTO(newUser);
        }

        public async Task<bool> DeleteUserAsync(long id)
        {
            User user = _repository.GetUserAsync(id).Result;
            if (user != null)
            {
                return await _repository.DeleteUserAsync(user);
            }
            return false;
        }

        public async Task<UserDTO> GetUserAsync(long id)
        {
            User user = (await _repository.GetUserAsync(id));
            return UserToDTO(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            List<User> list = (await _repository.GetAllUsersAsync()).ToList();
            List<UserDTO> dtoList = new List<UserDTO>();
            foreach(User u in list){
                dtoList.Add(UserToDTO(u));

            }
            return dtoList;
        }


        public async Task<UserDTO> UptadeUserAsync(UserDTO user)
        {
            User dbUser = await _repository.GetUserAsync(user.UserName);
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;

            User updatedUser = await _repository.UptadeUserAsync(dbUser);
            if (updatedUser == null)
            {
                return null;
            }
            return UserToDTO(updatedUser);
        }

        private User DTOToUser(UserDTO user, String password)
        {
            User newUser = new User();
            newUser.UserName = user.UserName;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.Password = password;

            return newUser;
        }

        private UserDTO UserToDTO(User user)
        {
            UserDTO dto = new UserDTO();
            dto.Id = user.Id;
            dto.UserName = user.UserName;
            dto.FirstName = user.FirstName;
            dto.LastName = user.LastName;
            dto.IsAdmin = user.IsAdmin;

            return dto;
        }

        public async Task<UserDTO> GetUserAsync(string username)
        {
            User user = (await _repository.GetUserAsync(username));
            if (user == null)
            {
                return null;
            }
            return UserToDTO(user);
        }
    }
}
