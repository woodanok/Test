using CustomersFinder.Models.Domain;
using CustomrsFinder.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomrsFinder.Models.Repositories
{
    public struct ManagerRepository
    {
        private IocContainer dependency;
        public ManagerRepository(IocContainer dependency) => this.dependency = dependency;

        public async Task<List<Manager>> GetAll() => await dependency.searchDB.manager.ToListAsync();

        public async Task<Manager> GetById(Int32 id) => await dependency.searchDB.manager.FirstOrDefaultAsync(s => s.id == id);
    }
}
