using CustomersFinder.Models.Domain;
using CustomersFinder.Models.Poco;
using CustomrsFinder.Models;
using CustomrsFinder.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomrsFinder.Models.Repositories
{
    public struct CustomerInfoRepository
    {
        private IocContainer dependency;
        public CustomerInfoRepository(IocContainer dependency) => this.dependency = dependency;

        public async Task InsertNewCustomerInfos(List<CustomerInfo> customerInfos)
        {
            await dependency.searchDB.customer_info.AddRangeAsync(customerInfos);
            await dependency.searchDB.SaveChangesAsync();
        }

        public async Task InsertNewCustomerInfo(CustomerInfo customerInfo)
        {
            //return await dependency.db.SelectOneValueAsync<int>(query: $"INSERT INTO `customer_info`(`name`, `INN`, `KPP`, `OGRN`, `boss`, `INNFL`, `boss_position`, `mail`, `address`, `site_url`, `status`, `date`, `MSP`, `proceeds`, `balance`, `arbitrage`, `net_proceeds`, `works_type`, `another_work_type`, `procurement_subject`, `registration_region`, `got_licenses`, `leasing`, `quantity`, `comments`, `customer_type_id`, `position_status_id`) VALUES " +
            //    $"('{customerInfo.name}', '{customerInfo.INN}', '{customerInfo.KPP}', '{customerInfo.OGRN}', '{customerInfo.boss}', '{customerInfo.INNFL}', '{customerInfo.boss_position}', '{customerInfo.mail}', '{customerInfo.address}', '{customerInfo.site_url}', '{customerInfo.status}', '{customerInfo.date:yyyy-MM-dd}', '{customerInfo.MSP}', '{customerInfo.proceeds}', '{customerInfo.balance}', '{customerInfo.arbitrage}', '{customerInfo.net_proceeds}', '{customerInfo.works_type}', '{customerInfo.another_work_type}', '{customerInfo.procurement_subject}', '{customerInfo.registration_region}', '{customerInfo.got_licenses}', '{customerInfo.leasing}', '{customerInfo.quantity}', '{customerInfo.comments}', '{customerInfo.customer_type_id}', '{customerInfo.position_status_id}'); SELECT id FROM `customer_info` WHERE id=LAST_INSERT_ID();");
            await dependency.searchDB.customer_info.AddAsync(customerInfo);
            await dependency.searchDB.SaveChangesAsync();
        }

        public async Task<Int32> GetCount() => await dependency.searchDB.customer_info.CountAsync();

        public async Task<CustomerInfo> GetCustomerByINN(String INN) => await dependency.searchDB.customer_info.FirstOrDefaultAsync(s => s.INN.ToLower() == INN.ToLower());

        public async Task<List<CustomerInfo>> GetByPageNumber(Int32 pageNumber) => await dependency.searchDB.customer_info.Skip(25 * (pageNumber - 1)).Take(25).ToListAsync();

        public async Task<List<CustomerInfo>> GetByFilters(Int32 pageNumber, Filters filters)
        {
            var result = new List<CustomerInfo>();
            if (filters.CustomerTypes != 0)
            {
                result = await dependency.searchDB.customer_info.Where(s=>s.customer_type_id == filters.CustomerTypes).Skip(25 * (pageNumber - 1)).Take(25).ToListAsync();
            }
            else result = await GetByPageNumber(pageNumber);
            return result;
        }

        public async Task<Int32> GetAllCount() => await dependency.searchDB.customer_info.CountAsync();

        public async Task<CustomerInfo> GetById(Int32 id) => await dependency.searchDB.customer_info.FirstOrDefaultAsync(s => s.id == id);
    }
}
