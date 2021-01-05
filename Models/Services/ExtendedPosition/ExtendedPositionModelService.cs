using CustomersFinder.Models.Poco;
using CustomersFinder.Models.ViewModels;
using CustomrsFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersFinder.Models.Services.ExtendedPosition
{
    public struct ExtendedPositionModelService
    {
        private IocContainer dependency;
        public ExtendedPositionModelService(IocContainer dependency) => this.dependency = dependency;

        public async Task<ExtendedPositionModel> GetByPosId(int id)
        {
            var events = await dependency.GetPositionEventsRepository().GetForPositionWithUserByPosId(id);
            var pos = await dependency.GetCustomerInfoService().GetById(id);
            var phones = await dependency.GetCustomerPhoneNumbersService().GetByCustomerId(id);
            return new ExtendedPositionModel { CustomerInfo = pos, Events = events, Phones = phones };
        }
    }
}
