using CustomersFinder.Models.Domain;
using CustomersFinder.Models.Enums;
using CustomrsFinder.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomrsFinder.Models.ViewModels
{
    public class CustomerFinderModel
    {
        public List<CustomerFinderForPage> Data { get; set; } = new();
        public PaginationModel Pagination { get; set; } = new();
    }

    public class CustomerFinderForPage
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String INN { get; set; }
        public String KPP { get; set; }
        public String OGRN { get; set; }
        public String Boss { get; set; }
        public String INNFL { get; set; }
        public String BossPosition { get; set; }
        public String Mail { get; set; }
        public String Address { get; set; }
        public String SiteUrl { get; set; }
        public String Status { get; set; }
        public DateTime Date { get; set; }
        public String MSP { get; set; }
        public String Proceeds { get; set; }
        public String Balance { get; set; }
        public String Arbitrage { get; set; }
        public String NetPoceeds { get; set; }
        public String WorksType { get; set; }
        public String AnotherWorkType { get; set; }
        public String ProcurementSubject { get; set; }
        public String RegistrationRegion { get; set; }
        public String GotLicenses { get; set; }
        public String Leasing { get; set; }
        public String Quantity { get; set; }
        public String Comments { get; set; }
        public List<CustomerPhoneNumbers> CustomerPhones { get; set; }
        public List<Manager> Managers { get; set; }
        public CompanyTypeEnum.Type CustomerType { get; set; }
        public PositionStatus PositionStatus { get; set; }
    }
}
