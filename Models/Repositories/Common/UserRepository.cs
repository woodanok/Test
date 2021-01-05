using CustomrsFinder.Models;
using CustomrsFinder.Models.Domain.Common;
using CustomrsFinder.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receiver.Models.Repositories.Common
{
    public struct UserRepository
    {
        public IocContainer IocContainer;
        public UserRepository(IocContainer iocContainer) => this.IocContainer = iocContainer;

        public async Task<User> GetById(Int32? id, String token)
        {
            var dict = new Dictionary<String, String>(1);
            dict.Add("id", id.ToString());
            return await ApiCommonV2.GetAsync<User>("Account/GetById", token, dict);
        }

        public async Task<User> GetUserByNameAndPassAsync(LoginModel model) => await ApiCommonV2.PostAsync<User>("/Account/LogIn6", model);


    }
}
