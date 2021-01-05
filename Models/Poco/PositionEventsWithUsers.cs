using CustomrsFinder.Models.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersFinder.Models.Poco
{
    public class PositionEventsWithUsersName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int PosId { get; set; }
        public string UserName { get; set; }
    }
}
