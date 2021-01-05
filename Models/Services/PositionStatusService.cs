using CustomersFinder.Models.Domain;
using CustomrsFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersFinder.Models.Services
{
    public struct PositionStatusService
    {
        private IocContainer dependency;
        public PositionStatusService(IocContainer dependency) => this.dependency = dependency;

        public async Task<List<PositionStatus>> GetAll() => await dependency.GetPositionStatusRepository().GetAll();

        public async Task<PositionStatus> GetById(int id) => await dependency.GetPositionStatusRepository().GetById(id);
    }
}
