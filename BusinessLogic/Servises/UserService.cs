using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Servises
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<User>> GetAll()
        {
            return await _repositoryWrapper.User.FindAll();
        }

        public async Task<User> GetById(string id)
        {
            var model = await _repositoryWrapper.User
                .FindByCondition(x => x.ClientId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            return model.First();
        }

        public async Task<User> GetByLogin(string login, string password)
        {
            var model = await _repositoryWrapper.User
                .FindByCondition(x => x.Login == login);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("User not found");
            }
            if (model.First().Password != password)
            {
                throw new ArgumentException("Password uncorrect");
            }
            return model.First();
        }

        public async Task<User> GetByEmail(string email)
        {
            var model = await _repositoryWrapper.User
                .FindByCondition(x => x.Email == email);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Email not registered");
            }
            return model.First();
        }

        public async Task Create(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));

            }
            if (string.IsNullOrEmpty(model.ClientId))
            {
                throw new ArgumentException(nameof(model.ClientId));
            }
            if (string.IsNullOrEmpty(model.FirstName))
            {
                throw new ArgumentException(nameof(model.FirstName));
            }
            if (string.IsNullOrEmpty(model.LastName))
            {
                throw new ArgumentException(nameof(model.LastName));
            }
            if (string.IsNullOrEmpty(model.Email))
            {
                throw new ArgumentException(nameof(model.Email));
            }
            if (string.IsNullOrEmpty(model.Login))
            {
                throw new ArgumentException(nameof(model.Login));
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                throw new ArgumentException(nameof(model.Password));
            }
            if (model.DateOfBirth > DateTime.Now.AddYears(-14))
            {
                throw new ArgumentException(nameof(model.DateOfBirth));
            }
            var users = await _repositoryWrapper.User.FindByCondition(u => true);
            if (users == null)
            {
                users = new List<User>();
            }

            var existingUser = users.FirstOrDefault(u =>
                u.SeriesPasport == model.SeriesPasport &&
                u.NumberPasport == model.NumberPasport);

            if (existingUser != null)
            {
                throw new ArgumentException("A passport with this series and number already exists.");
            }

            existingUser = users.FirstOrDefault(u => u.Login == model.Login);

            if (existingUser != null)
            {
                throw new ArgumentException("Login is taken by another user");
            }
            existingUser = users.FirstOrDefault(u => u.Email == model.Email);

            if (existingUser != null)
            {
                throw new ArgumentException("Email is taken by another user");
            }
            if (model.DeletedAt < model.CreatedAt)
            {
                throw new ArgumentException(nameof(model.CreatedAt));
            }
            await _repositoryWrapper.User.Create(model);

            _repositoryWrapper.Save();
        }

        public async Task Update(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));

            }
            if (string.IsNullOrEmpty(model.ClientId))
            {
                throw new ArgumentException(nameof(model.ClientId));
            }
            if (string.IsNullOrEmpty(model.FirstName))
            {
                throw new ArgumentException(nameof(model.FirstName));
            }
            if (string.IsNullOrEmpty(model.LastName))
            {
                throw new ArgumentException(nameof(model.LastName));
            }
            if (string.IsNullOrEmpty(model.Email))
            {
                throw new ArgumentException(nameof(model.Email));
            }
            if (string.IsNullOrEmpty(model.Login))
            {
                throw new ArgumentException(nameof(model.Login));
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                throw new ArgumentException(nameof(model.Password));
            }
            if (model.DateOfBirth > DateTime.Now.AddYears(-14))
            {
                throw new ArgumentException(nameof(model.DateOfBirth));
            }
            if (model.SeriesPasport < 0)
            {
                throw new ArgumentException(nameof(model.SeriesPasport));
            }
            if (model.NumberPasport < 0)
            {
                throw new ArgumentException(nameof(model.NumberPasport));
            }
            var users = await _repositoryWrapper.User.FindByCondition(u => true);
            if (users == null)
            {
                users = new List<User>();
            }

            var existingUser = users.FirstOrDefault(u =>
                u.SeriesPasport == model.SeriesPasport &&
                u.NumberPasport == model.NumberPasport &&
                u.ClientId != model.ClientId); // currentUserId - идентификатор текущего пользователя

            if (existingUser != null)
            {
                throw new ArgumentException("A passport with this series and number already exists.");
            }

            existingUser = users.FirstOrDefault(u => u.Login == model.Login);

            if (existingUser != null)
            {
                throw new ArgumentException("Login is taken by another user");
            }
            existingUser = users.FirstOrDefault(u => u.Email == model.Email);

            if (existingUser != null)
            {
                throw new ArgumentException("Email is taken by another user");
            }
            _repositoryWrapper.User.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(string id)
        {
            var model = await _repositoryWrapper.User
                .FindByCondition(x => x.ClientId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            _repositoryWrapper.User.Delete(model.First());
            _repositoryWrapper.Save();
        }
    }
}