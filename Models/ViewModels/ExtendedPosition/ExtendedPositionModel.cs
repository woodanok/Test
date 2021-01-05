using CustomersFinder.Models.Domain;
using CustomersFinder.Models.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersFinder.Models.ViewModels
{
    public class ExtendedPositionModel
    {
        public CustomerInfo CustomerInfo { get; set; }
        public List<PositionEventsWithUsersName> Events { get; set; }
        public List<CustomerPhoneNumbers> Phones { get; set; }
    }
}
