using CustomersFinder.Models.Domain;
using CustomrsFinder.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomrsFinder.Models.Service
{
    public struct ManagerService
    {
        private IocContainer dependency;
        public ManagerService(IocContainer dependency) => this.dependency = dependency;

        public async Task<List<Manager>> GetAll() => await dependency.GetManagerRepository().GetAll();

        public async Task<Manager> GetById(Int32 id) => await dependency.GetManagerRepository().GetById(id);
    }
}
