using CustomersFinder.Models.Domain;
using CustomersFinder.Models.Poco;
using CustomrsFinder.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersFinder.Models.Repositories
{
    public struct PositionEventsRepository
    {
        private IocContainer dependency;
        public PositionEventsRepository(IocContainer dependency) => this.dependency = dependency;

        public async Task Insert(PositionEvents events)
        {
            await dependency.searchDB.position_events.AddAsync(events);
            await dependency.searchDB.SaveChangesAsync();
        }

        public async Task<List<PositionEvents>> GetForPositionByPosId(int id) => await dependency.searchDB.position_events.Where(s => s.customer_info_id == id).ToListAsync();

        public async Task<List<PositionEventsWithUsersName>> GetForPositionWithUserByPosId(int id) => await dependency.searchDB.PositionEventsWithUsers.FromSqlRaw($"SELECT position_events.id as Id, position_events.name as Name, position_events.customer_info_id as PosId, position_events.date as Date, user.short_name as UserName FROM `position_events`, u0145014_metroscope.user as user WHERE user.id = position_events.user_id AND customer_info_id={id}").ToListAsync();
    }
}
