using CustomersFinder.Models.Domain;
using CustomrsFinder.Models;
using CustomrsFinder.Models.Domain;
using CustomrsFinder.Models.Emuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomrsFinder.Models.Service
{
    public struct CustomerPhoneNumbersService
    {
        private IocContainer dependency;
        public CustomerPhoneNumbersService(IocContainer dependency) => this.dependency = dependency;

        public async Task InsertRange(List<CustomerPhoneNumbers> numbers) => await dependency.GetCustomerPhoneNumbersRepository().InsertRange(numbers);

        //public async Task InsertRange(List<CustomerPhoneNumbers> numbers) 
        //{
        //    await dependency.db.InsertAsync(query: $"INSERT INTO `customer_phone_numbers`(`number`, `is_actual_id`, `customer_info_id`) VALUES {string.Join(", ", numbers.Select(s => $"({s.number}, {s.is_actual_id}, {s.customer_info_id})"))}");
        //}

        public async Task<List<CustomerPhoneNumbers>> GetByCustomersId(List<Int32> customersId) => await dependency.GetCustomerPhoneNumbersRepository().GetByCustomersId(customersId);

        public async Task<List<CustomerPhoneNumbers>> GetByCustomerId(Int32 customersId) => await dependency.GetCustomerPhoneNumbersRepository().GetByCustomerId(customersId);

        public async Task UpdateStatusById(Int32 id, String status)
        {
            var result = await dependency.GetCustomerPhoneNumbersRepository().GetById(id);
            result.is_actual_id = String.Compare(status, "success") == 0 ? (Int32)PhoneStatus.Status.Actual : (Int32)PhoneStatus.Status.Not;
            await dependency.SaveChangesDBAsunc();
        }
    }
}
