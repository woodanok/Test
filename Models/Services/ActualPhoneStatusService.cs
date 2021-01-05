using CustomersFinder.Models.Domain;
using CustomrsFinder.Models;
using CustomrsFinder.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomrsFinder.Models.Service
{
    public struct ActualPhoneStatusService
    {
        private IocContainer dependency;
        public ActualPhoneStatusService(IocContainer dependency) => this.dependency = dependency;

        public async Task<List<ActualPhoneStatus>> GetAll() => await dependency.GetActualPhoneStatusRepository().GetAll();
    }
}
