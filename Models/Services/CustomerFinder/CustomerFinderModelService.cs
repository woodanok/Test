using CustomersFinder.Models.Domain;
using CustomersFinder.Models.Enums;
using CustomersFinder.Models.Poco;
using CustomrsFinder.Models;
using CustomrsFinder.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersFinder.Models.Services.CustomerFinder
{
    public struct CustomerFinderModelService
    {
        private IocContainer dependency;
        public CustomerFinderModelService(IocContainer dependency) => this.dependency = dependency;

        public async Task<CustomerFinderModel> GetCustomerFinderModel(Int32 pageNumer, Filters filters = null)
        {
            var posCount = await dependency.GetCustomerInfoService().GetAllCount();
            if (posCount == 0) return new();

            var customerInfos = new List<CustomerInfo>();
            if (filters != null) customerInfos = await dependency.GetCustomerInfoService().GetByFilters(pageNumer, filters);
            else customerInfos = await dependency.GetCustomerInfoService().GetByPageNumber(pageNumer);
            var phones = await dependency.GetCustomerPhoneNumbersService().GetByCustomersId(customerInfos.Select(s => s.id).ToList());
            var managers = await dependency.GetManagerService().GetAll();
            var positionStatuses = await dependency.GetPositionStatusService().GetAll();

            var custResult = customerInfos.GroupJoin(phones,
                c => c.id,
                p => p.customer_info_id,
                (c, p) => new CustomerFinderForPage
                {
                    CustomerPhones = p.ToList(),
                    Comments = c.comments,
                    Date = c.date,
                    Address = c.address,
                    AnotherWorkType = c.another_work_type,
                    Arbitrage = c.arbitrage,
                    Balance = c.balance,
                    Boss = c.boss,
                    BossPosition = c.boss_position,
                    GotLicenses = c.got_licenses,
                    Id = c.id,
                    INN = c.INN,
                    INNFL = c.INNFL,
                    KPP = c.KPP,
                    Leasing = c.leasing,
                    Mail = c.mail,
                    MSP = c.MSP,
                    Name = c.name,
                    NetPoceeds = c.net_proceeds,
                    OGRN = c.OGRN,
                    Proceeds = c.proceeds,
                    ProcurementSubject = c.procurement_subject,
                    Quantity = c.quantity,
                    RegistrationRegion = c.registration_region,
                    SiteUrl = c.site_url,
                    Status = c.status,
                    WorksType = c.works_type,
                    Managers = managers,
                    CustomerType = c.customer_type_id == 1 ? CompanyTypeEnum.Type.IP : CompanyTypeEnum.Type.Company,
                    PositionStatus = positionStatuses.FirstOrDefault(s => s.Id == c.position_status_id)
                }).ToList();

            var model = new CustomerFinderModel();
            model.Data = custResult;
            model.Pagination.CurrentPage = pageNumer;
            model.Pagination.PageCount = posCount / 25;

            return model;
        }
    }
}
