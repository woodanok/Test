using CustomersFinder.Models.Domain;
using CustomrsFinder.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersFinder.Models.Repositories
{
    public struct PositionStatusRepository
    {
        private IocContainer dependency;
        public PositionStatusRepository(IocContainer dependency) => this.dependency = dependency;

        public async Task<List<PositionStatus>> GetAll() => await dependency.searchDB.position_status.ToListAsync();

        public async Task<PositionStatus> GetById(int id) => await dependency.searchDB.position_status.FirstOrDefaultAsync(s => s.Id == id);
    }
}
