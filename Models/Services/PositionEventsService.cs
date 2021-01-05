using CustomersFinder.Models.Domain;
using CustomersFinder.Models.Poco;
using CustomrsFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersFinder.Models.Repositories
{
    public struct PositionEventsService
    {
        private IocContainer dependency;
        public PositionEventsService(IocContainer dependency) => this.dependency = dependency;

        public async Task Insert(PositionEvents events) => await dependency.GetPositionEventsRepository().Insert(events);

        public async Task<List<PositionEvents>> GetAllByPosId(int id) => await dependency.GetPositionEventsRepository().GetForPositionByPosId(id);

        public async Task<List<PositionEventsWithUsersName>> GetForPositionWithUserByPosId(int id) => await dependency.GetPositionEventsRepository().GetForPositionWithUserByPosId(id);
    }
}
