using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersFinder.Models.Domain
{
    public class CustomerInfo
    {
        public Int32 id { get; set; }
        public String name { get; set; } = string.Empty;
        public String INN { get; set; } = string.Empty;
        public String KPP { get; set; } = string.Empty;
        public String OGRN { get; set; } = string.Empty;
        public String boss { get; set; } = string.Empty;
        public String INNFL { get; set; } = string.Empty;
        public String boss_position { get; set; } = string.Empty;
        public String mail { get; set; } = string.Empty;
        public String address { get; set; } = string.Empty;
        public String site_url { get; set; } = string.Empty;
        public String status { get; set; } = string.Empty;
        public DateTime date { get; set; }

        public String MSP { get; set; } = string.Empty;
        public String proceeds { get; set; } = string.Empty;
        public String balance { get; set; } = string.Empty;
        public String arbitrage { get; set; } = string.Empty;
        public String net_proceeds { get; set; } = string.Empty;
        public String works_type { get; set; } = string.Empty;
        public String another_work_type { get; set; } = string.Empty;
        public String procurement_subject { get; set; } = string.Empty;
        public String registration_region { get; set; } = string.Empty;
        public String got_licenses { get; set; } = string.Empty;
        public String leasing { get; set; } = string.Empty;
        public String quantity { get; set; } = string.Empty;
        public String comments { get; set; } = string.Empty;
        public Int32 customer_type_id { get; set; }
        public Int32 position_status_id { get; set; }
    }

    public class CustomerInfoFromExcel
    {
        public String name { get; set; }
        public String INN { get; set; }
        public String KPP { get; set; }
        public String OGRN { get; set; }
        public String boss { get; set; }
        public String INNFL { get; set; }
        public String boss_position { get; set; }
        public String phone_number { get; set; }
        public String mail { get; set; }
        public String address { get; set; }
        public String site_url { get; set; }
        public String status { get; set; }

        private String dateTemp;
        public String date
        {
            get
            {
                var massData = dateTemp.Split('.');
                return String.Join("-", massData.Reverse());
            }
            set { dateTemp = value; }
        }

        public String MSP { get; set; }
        public String proceeds { get; set; }
        public String balance { get; set; }
        public String arbitrage { get; set; }
        public String net_proceeds { get; set; }
        public String works_type { get; set; }
        public String another_work_type { get; set; }
        public String procurement_subject { get; set; }
        public String registration_region { get; set; }
        public String got_licenses { get; set; }
        public String leasing { get; set; }
        public String quantity { get; set; }
    }
}
