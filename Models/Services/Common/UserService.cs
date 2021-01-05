using CustomrsFinder.Models.Domain.Common;
using CustomrsFinder.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomrsFinder.Models.Services.Common
{
    public struct UserService
    {
        private IocContainer dependency;
        public UserService(IocContainer dependency) => this.dependency = dependency;

        public async Task<User> GetUserByIdAsync(Int32? id, String token) => await dependency.GetUserRepository().GetById(id, token);

        public async Task<User> GetUserByNameAndPassAsync(LoginModel login) => await dependency.GetUserRepository().GetUserByNameAndPassAsync(login);
    }
}
