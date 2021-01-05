using CustomersFinder.Models.Domain;
using CustomersFinder.Models.Poco;
using CustomrsFinder.Models.Emuns;
using CustomrsFinder.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomrsFinder.Models.Service
{
    public struct CustomerInfoService
    {
        private IocContainer dependency;
        public CustomerInfoService(IocContainer dependency) => this.dependency = dependency;

        public async Task Insert(List<CustomerInfoFromExcel> parseExcel)
        {
            foreach (var pos in parseExcel)
            {
                var custInfo = new CustomerInfo
                {
                    address = pos.address,
                    date = Convert.ToDateTime(pos.date), // i changed string date to datetime date  !!!beware
                    another_work_type = pos.another_work_type,
                    arbitrage = pos.arbitrage,
                    balance = pos.balance,
                    boss = pos.boss,
                    boss_position = pos.boss_position,
                    got_licenses = pos.got_licenses,
                    INN = pos.INN,
                    INNFL = pos.INNFL,
                    KPP = pos.KPP,
                    leasing = pos.leasing,
                    mail = pos.mail,
                    MSP = pos.MSP,
                    name = pos.name,
                    net_proceeds = pos.net_proceeds,
                    OGRN = pos.OGRN,
                    proceeds = pos.proceeds,
                    procurement_subject = pos.procurement_subject,
                    quantity = pos.quantity,
                    registration_region = pos.registration_region,
                    site_url = pos.site_url,
                    status = pos.status,
                    works_type = pos.works_type,
                    position_status_id = 1
                };

                var custExist = await this.GetCustomerByINN(custInfo.INN);

                Func<List<CustomerPhoneNumbers>> getPhones = () =>
                {
                    var phones = new List<CustomerPhoneNumbers>();
                    if (!String.IsNullOrEmpty(pos.phone_number))
                    {
                        foreach (var phone in pos.phone_number.Split("\n").ToList())
                        {
                            var cleanPhone = phone.Replace(" ", String.Empty).Replace("-", String.Empty).Replace("(", String.Empty).Replace(")", String.Empty).Replace("+7", "8").Trim();
                            if (!String.IsNullOrWhiteSpace(cleanPhone))
                            {
                                phones.Add(new CustomerPhoneNumbers
                                {
                                    customer_info_id = custExist == null ? custInfo.id : custExist.id,
                                    is_actual_id = (Int32)PhoneStatus.Status.Default,
                                    number = cleanPhone
                                });
                            }
                        }
                    }
                    return phones;
                };

                if (custExist == null)
                {
                    if (custInfo.name.ToLower().Contains("ип ")) custInfo.customer_type_id = 1;
                    else custInfo.customer_type_id = 2;

                    await this.InsertNewCustomerInfo(custInfo);

                    var phones = getPhones();


                    if (phones.Count != 0) await dependency.GetCustomerPhoneNumbersService().InsertRange(phones);
                }
                else
                {
                    var phones = getPhones();

                    if (phones.Count != 0)
                    {
                        var phonesExist = await dependency.GetCustomerPhoneNumbersService().GetByCustomerId(custExist.id);
                        var newPhones = phones.Except(phonesExist, new GenericCompare<CustomerPhoneNumbers>(x => x.number)).ToList();
                        if (newPhones.Count != 0) await dependency.GetCustomerPhoneNumbersService().InsertRange(newPhones);
                    }
                }
            }
        }

        public async Task InsertNewCustomerInfos(List<CustomerInfo> customerInfos) => await dependency.GetCustomerInfoRepository().InsertNewCustomerInfos(customerInfos);

        public async Task<List<CustomerInfo>> GetByPageNumber(Int32 pageNumber) => await dependency.GetCustomerInfoRepository().GetByPageNumber(pageNumber);

        public async Task<List<CustomerInfo>> GetByFilters(Int32 pageNumber, Filters filters) => await dependency.GetCustomerInfoRepository().GetByFilters(pageNumber, filters);

        public async Task<Int32> GetAllCount() => await dependency.GetCustomerInfoRepository().GetAllCount();

        public async Task InsertNewCustomerInfo(CustomerInfo customerInfo) => await dependency.GetCustomerInfoRepository().InsertNewCustomerInfo(customerInfo);

        public async Task<CustomerInfo> GetCustomerByINN(String INN) => await dependency.GetCustomerInfoRepository().GetCustomerByINN(INN);

        public async Task<CustomerInfo> GetById(int id) => await dependency.GetCustomerInfoRepository().GetById(id);

        public async Task UpdateCommentsById(String comments, Int32 id)
        {
            var pos = await this.GetById(id);
            pos.comments = comments;
            await dependency.SaveChangesDBAsunc();
        }

        public async Task ChangeStatusById(int posId, int statusId)
        {
            var pos = await this.GetById(posId);
            pos.position_status_id = statusId;
            await dependency.SaveChangesDBAsunc();
        }

        public async Task<string> GetMailBodyByPosId(int posId)
        {
            var customerInfo = await this.GetById(posId);
            return $"<p><b>Наименование:</b> {customerInfo.name}</p>" +
                $"<p><b>Руководитель:</b> {customerInfo.boss_position} - {customerInfo.boss}</p>" +
                $"<p><b>Почта:</b> {customerInfo.mail}</p>" +
                $"<p><b>Адрес:</b> {customerInfo.address}</p>" +
                $"<p><b>Последний комментарий:</b> {customerInfo.comments}</p>";
        }

        public async Task<Int32> GetCount() => await dependency.GetCustomerInfoRepository().GetCount();
    }
}
