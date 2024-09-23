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
            var user = await _repositoryWrapper.User.FindByCondition(x => x.ClientId == id);
            return user.First();
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
            await _repositoryWrapper.User.Create(model);

            _repositoryWrapper.Save();
        }

        public async Task Update(User model)
        {
            _repositoryWrapper.User.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(string id)
        {
            var user = await _repositoryWrapper.User
                .FindByCondition(x => x.ClientId == id);
            _repositoryWrapper.User.Delete(user.First());
            _repositoryWrapper.Save();
        }
    }
}