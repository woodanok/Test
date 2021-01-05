using CustomersFinder.Models.Domain;
using CustomrsFinder.Models;
using CustomrsFinder.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomrsFinder.Models.Repositories
{
    public struct CustomerPhoneNumbersRepository
    {
        private IocContainer dependency;
        public CustomerPhoneNumbersRepository(IocContainer dependency) => this.dependency = dependency;

        public async Task InsertRange(List<CustomerPhoneNumbers> numbers)
        {
            await dependency.searchDB.customer_phone_numbers.AddRangeAsync(numbers);
            await dependency.SaveChangesDBAsunc();
        }

        public async Task<List<CustomerPhoneNumbers>> GetByCustomersId(List<Int32> customersId) => await dependency.searchDB.customer_phone_numbers.Where(s => customersId.Contains(s.customer_info_id)).ToListAsync();

        public async Task<List<CustomerPhoneNumbers>> GetByCustomerId(Int32 customersId) => await dependency.searchDB.customer_phone_numbers.Where(s =>s.customer_info_id == customersId).ToListAsync();

        public async Task<CustomerPhoneNumbers> GetById(Int32 id) => await dependency.searchDB.customer_phone_numbers.FirstOrDefaultAsync(s => s.id == id);

        public async Task Update(CustomerPhoneNumbers numbers)
        {
            dependency.searchDB.customer_phone_numbers.Update(numbers);
            await dependency.searchDB.SaveChangesAsync();
        }
    }
}
