using CustomersFinder.Models.Domain;
using CustomrsFinder.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomrsFinder.Models.Repositories
{
    public struct ActualPhoneStatusRepository
    {
        private IocContainer dependency;
        public ActualPhoneStatusRepository(IocContainer dependency) => this.dependency = dependency;

        public async Task<List<ActualPhoneStatus>> GetAll() => await dependency.searchDB.actual_phone_status.ToListAsync();
    }
}
