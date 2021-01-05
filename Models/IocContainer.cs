using CustomersFinder.Models.Repositories;
using CustomersFinder.Models.Services;
using CustomersFinder.Models.Services.CustomerFinder;
using CustomersFinder.Models.Services.ExtendedPosition;
using CustomersFinder.Models.Utils;
using CustomrsFinder.Models.Repositories;
using CustomrsFinder.Models.Service;
using CustomrsFinder.Models.Services.Common;
using CustomrsFinder.Models.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Receiver;
using Receiver.Models.Repositories;
using Receiver.Models.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomrsFinder.Models
{
    public class IocContainer
    {
        private readonly CustomerFinderDB customerSearchDB;
        public CustomerFinderDB searchDB => customerSearchDB;

        public IocContainer(CustomerFinderDB customerSearchDB)
        {
            this.customerSearchDB = customerSearchDB;
        }

        public async Task SaveChangesDBAsunc() => await customerSearchDB.SaveChangesAsync();
        public void SaveChangesDB() => customerSearchDB.SaveChanges();

        public UserService GetUserService() => new(this);

        public CustomerInfoService GetCustomerInfoService() => new(this);
        public ActualPhoneStatusService GetActualPhoneStatusService() => new(this);
        public CustomerPhoneNumbersService GetCustomerPhoneNumbersService() => new(this);
        public ManagerService GetManagerService() => new(this);
        public PositionStatusService GetPositionStatusService() => new(this);
        public CustomerFinderModelService GetCustomerFinderModelService() => new(this);
        public PositionEventsService GetPositionEventsService() => new(this);
        public ExtendedPositionModelService GetExtendedPositionModelService() => new(this);

        public WorkWithExcel GetWorkWithExcel() => new(this);
        public Coockies GetCoockiesService() => new(this);



        public UserRepository GetUserRepository() => new(this);

        public CustomerInfoRepository GetCustomerInfoRepository() => new(this);
        public ActualPhoneStatusRepository GetActualPhoneStatusRepository() => new(this);
        public CustomerPhoneNumbersRepository GetCustomerPhoneNumbersRepository() => new(this);
        public ManagerRepository GetManagerRepository() => new(this);
        public PositionStatusRepository GetPositionStatusRepository() => new(this);
        public PositionEventsRepository GetPositionEventsRepository() => new(this);

    }
}
